window.blazorCulture = {
    get: () => window.localStorage['BlazorCulture'],
    set: (value) => window.localStorage['BlazorCulture'] = value
};

var exitScriptObj = null;

/*$('a[href="?GoCTemplateCulture"]').click(function () {
    alert('Contains question mark');
});*/
function linkClick(e) {
    alert(e.target.href);
}
links = document.getElementsByTagName('a');
for (i = 0; i < links.length; i++)
    links[i].addEventListener('click', linkClick, false);
//nodocwrite function
/*function cdtsSetup(obj) {
    var script = document.createElement('script');
    script.setAttribute('type', 'text/javascript');
    script.setAttribute('src', 'https://cdts.service.canada.ca/app/cls/WET/gcweb/v4_1_0/cdts/compiled/wet-en.js');
    script.setAttribute('data-cdts-setup', obj);
    document.getElementsByTagName('head')[0].appendChild(script);
}*/
//end nodoc functions functions

/*function setTop(obj) {
	const y = JSON.parse(obj);
    var defTop = document.getElementById("def-top");
    defTop.insertAdjacentHTML("afterend", wet.builder.top(y));
}*/

function setTop(obj, isApp) {
    const y = JSON.parse(obj);
    const tmpElem = document.createElement('template'); //top's content must be directly in <body>, so use "template" element as temporary container
    //(can't use outerHTML on an orphan element)
    tmpElem.insertAdjacentHTML('afterbegin', isApp ? wet.builder.appTop(y) : wet.builder.top(y)); //eslint-disable-line
    //inject a "marker/tag" class
    for (let e of tmpElem.children) e.classList.add('cdtsreact-top-tag'); //using `children` and not `childNodes`, we only want elements
    
    //---[ Remove any elements from previous runs
    document.body.querySelectorAll('.cdtsreact-top-tag').forEach((e) => e.remove());

    //---[ Install right after body
    for (let i = tmpElem.children.length - 1; i >= 0; i--) {
        const e = tmpElem.children[i];
        document.body.insertAdjacentElement('afterbegin', e);
    }
}

function setPreFooter(obj) {
    const y = JSON.parse(obj);
    const tmpElem = document.createElement('div');
    //(can't use outerHTML on an orphan element)
    tmpElem.insertAdjacentHTML('afterbegin', wet.builder.preFooter(y));
    const element = document.getElementById('cdts-react-def-preFooter');
    //TODO: Check for null
    if (element.childNodes.length > 0) {
        element.replaceChild(tmpElem);
    }
    else {
        element.appendChild(tmpElem);
    }    
}

function setSectionMenu(obj) {
    $('#wb-sec, #cdts-main').wrapAll("<div class='container'><div class='row'></div></div>");
    const y = JSON.parse(obj);
    const tmpElem = document.createElement('div');
    //(can't use outerHTML on an orphan element)
    tmpElem.insertAdjacentHTML('afterbegin', wet.builder.secmenu(y));
    const element = document.getElementById('wb-sec');
    if (element.childNodes.length > 0) {
        element.replaceChild(tmpElem);
    }
    else {
        element.appendChild(tmpElem);
    }
}
/*function setPreFooter(obj) {
	const y = JSON.parse(obj);
	var defPreFooter = document.getElementById("def-preFooter");
	defPreFooter.outerHTML = wet.builder.preFooter(y);
}*/

/*function setFooter(obj) {
	const y = JSON.parse(obj);
	var defFooter = document.getElementById("def-footer");
	defFooter.outerHTML = wet.builder.footer(y );
}*/
function setFooter(obj, isApp) {
    const y = JSON.parse(obj);
    const tmpElem = document.createElement('template'); //footer's content must be directly in <body>, so use "template" element as temporary container
    //(can't use outerHTML on an orphan element)
    tmpElem.insertAdjacentHTML('afterbegin', isApp ? wet.builder.appFooter(y) : wet.builder.footer(y)); //eslint-disable-line
    //inject a "marker/tag" class
    for (let e of tmpElem.children) e.classList.add('cdtsreact-footer-tag'); //using `children` and not `childNodes`, we only want elements

    //---[ Remove any elements from previous runs
    document.body.querySelectorAll('.cdtsreact-footer-tag').forEach((e) => e.remove());

    //---[ Install right after body
    const children = Array.from(tmpElem.children); //don't want a live list for this, so convert to array
    for (let i = 0; i < children.length; i++) {
        const e = children[i];
        document.body.appendChild(e);
    }
    resetExitScript(exitScriptObj);
}


function setRefFooter(obj, exit) {
    exitScriptObj = exit;
    applyRefFooter(obj, onRefFooterCompleted);	
}

function setRefTop(obj) {
    applyRefTop(obj, onRefFooterCompleted);
}

function onRefFooterCompleted() {
    //blah
}

function FragmentLoader(targetElem, fragmentNodes, doneFunc) {
    this.targetElem = targetElem;
    this.fragmentNodes = fragmentNodes;
    this.doneFunc = doneFunc;
    this.cursorIndex = 0;

    this.nodeScriptClone = function nodeScriptClone(node) {
        const script = document.createElement("script");
        script.text = node.innerHTML;

        let i = -1;
        const attrs = node.attributes
        let attr;
        while (++i < attrs.length) script.setAttribute((attr = attrs[i]).name, attr.value);

        return script;
    };

    this.onScriptLoaded = function onScriptLoaded(/*ev*/) {
        this.run(); //resume processing
    };

    this.run = function run() {
        //---[ While we still have nodes to load...
        while (this.cursorIndex < this.fragmentNodes.length) {
            if (this.fragmentNodes[this.cursorIndex].tagName != null && this.fragmentNodes[this.cursorIndex].tagName.toUpperCase() === 'SCRIPT') {
                //---[ node is a SCRIPT, special treatment (document.importNode(nodes[i], true) does NOT work for scripts)
                const tmpScript = this.nodeScriptClone(this.fragmentNodes[this.cursorIndex]);
                const hasSrc = tmpScript.hasAttribute('src');
                if (hasSrc) tmpScript.onload = this.onScriptLoaded.bind(this); //(else, if there is no src, there'll be no load event so don't attach the handler)
                this.targetElem.appendChild(tmpScript);
                this.cursorIndex++;
                if (hasSrc) return; //GET OUT OF HERE, event handler will call us again to resume when script is finished loading
                //(if there is no event handler, consider the script loaded and just keep going)
            }
            else {
                //---[ node is "normal", just inject
                this.targetElem.appendChild(document.importNode(this.fragmentNodes[this.cursorIndex], true));
                this.cursorIndex++;
            }
        } //of while

        //(if we get here, we're done)
        if (this.doneFunc) this.doneFunc(this);
    }
}

function applyRefTop(obj, onCompletedFunc) {
    //CStoJSCall();
    const parser = new DOMParser();

    const y = JSON.parse(obj);

    //---[ Insert refTop at the end of HEAD
    const tmpDoc = parser.parseFromString('<html><head>' + wet.builder.refTop(y) + '</head></html>', 'text/html');
    const nodes = tmpDoc.head.childNodes; //NOTE: Must use `childNodes` and not `children` for comments to be inserted
    const loader = new FragmentLoader(document.head, nodes, onCompletedFunc);
    loader.run();
}

function applyRefFooter(obj, onCompletedFunc) {
    const parser = new DOMParser();

    const y = JSON.parse(obj);

    //---[ Insert refFooter at the end of BODY
    const tmpDoc = parser.parseFromString('<html><body>' + wet.builder.refFooter(y) + '</body></html>', 'text/html');
    const nodes = tmpDoc.body.childNodes; //NOTE: Must use `childNodes` and not `children` for comments to be inserted
    const loader = new FragmentLoader(document.body, nodes, onCompletedFunc);
    loader.run();
}

async function installCDTS(lang) {
    document.documentElement.setAttribute('lang', lang);
    const cssHref = findCDTSCssHref();
    if (cssHref) {
        var cdtsEnvironment = deriveCDTSEnv(cssHref);
    }//TODO: Add the else condition
    await appendScriptElement(document.head, `${cdtsEnvironment.baseUrl}cdts/compiled/wet-${lang}.js`, 'cdts-main-js', false); //change from false to sriHash
}
function appendScriptElement(parentElement, src, id, sriHash) {
    return new Promise(function cdase(resolve, reject) {
        const elem = document.createElement('script');
        if (id) elem.setAttribute('id', id);
        if (sriHash) {
            elem.setAttribute('integrity', sriHash);
            elem.setAttribute('crossorigin', 'anonymous');
        }
        elem.onload = resolve.bind(null);
        elem.onerror = reject;
        elem.setAttribute('src', src);

        parentElement.appendChild(elem);
    });
}
 
function deriveCDTSEnv(cssHref) {
    if (!cssHref) return null;
    try {
        const url = new URL(cssHref);
        const hostname = url.hostname.toLowerCase();
        const pathname = url.pathname.toLowerCase();
        const baseUrl = cssHref.substring(0, cssHref.lastIndexOf('/cdts/cdts-') + 1);

        //theme
        const theme = pathname.includes('/gcintranet/') ? 'gcintranet' : 'gcweb';

        //cdnEnv
        let cdnEnv = null;
        if (hostname === 'www.canada.ca' || hostname === 'canada.ca') {
            cdnEnv = 'prod';
        }
        else if (hostname === 'cdts.service.canada.ca') {
            cdnEnv = theme === 'gcweb' ? 'esdcprod' : 'prod'; //depends on theme
        }
        else if (hostname === 'templates.service.gc.ca') {
            cdnEnv = 'esdcprod';
        }
        else {
            cdnEnv = baseUrl; //anything else is taken as-is minux the stylesheet's location, including trailing slash
        }

        //version
        let version = null;
        if (pathname.includes('/rn/cls/') || pathname.includes('/rn/cdts/')) {
            version = 'rn';
        }
        else {
            version = (pathname.match(/\/(v[0-9]+_[0-9]+_[0-9]+)\//) || [null, null])[1];
        }

        return { cdnEnv, theme, version, baseUrl };
    }
    catch (error) {
        console.error('Unexpected error parsing CDTS stylesheet URL, unable to derive CDTS parameters, defaults will be used.', error);
        return null;
    }
}

function findCDTSCssHref() {
    return Array.from(document.head.querySelectorAll('link[rel="stylesheet"]')).map((e) => e.getAttribute('href')).find((href) => href?.includes('/cdts/cdts-')) || null;
}

function CStoJSCall() {
    // Invoke to call C# function from JavaScript.
    DotNet.invokeMethodAsync("Blazor", "TestMethod");
}

function resetExitScript(exitScriptObj) {
    var elems = document.getElementsByTagName('a');
    wet.utilities.wetExitScript(exitScriptObj.displayModal.toString(), exitScriptObj.exitURL, exitScriptObj.exitDomains, exitScriptObj.exitMsg, exitScriptObj.yesMsg, exitScriptObj.cancelMsg, exitScriptObj.msgBoxHeader, exitScriptObj.targetWarning, exitScriptObj.displayModalForNewWindow.toString(), elems);
}