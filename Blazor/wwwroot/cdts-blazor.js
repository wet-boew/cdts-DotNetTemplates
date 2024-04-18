//Get and set the culture name in the local storage
window.blazorCulture = {
    get: () => window.localStorage['BlazorCulture'],
    set: (value) => window.localStorage['BlazorCulture'] = value
};

var cdtsBlazor = {
    exitScriptObj: null,
    globalSerializedFooter: null,
    globalSerializedTop: null,
    globalSerializedPreFooter: null,
    globalSerializedSectionMenu: null,
    currentPage: null,
    cdnEnv: null, 
    defaultCSSHref: "https://www.canada.ca/etc/designs/canada/cdts/gcweb/v5_0_0/cdts/cdts-styles.css",

    //Functions
    setRefTop: function setRefTop(serializedSetupBase, isApp) {
        const parsedSetupBase = JSON.parse(serializedSetupBase);
        this.exitScriptObj = parsedSetupBase.exitSecureSite;
        wet.localConfig = { cdnEnv: this.cdnEnv, base: { ...parsedSetupBase, isApplication: isApp, sriEnabled: false, cdtsSetupExcludeCSS: true } };
        wet.utilities.applyRefTop(() => {
            wet.utilities.applyRefFooter(() => { 
                wet.utilities.onRefFooterCompleted();
            });
        });
    },

    setTop: function setTop(serializedTop, isApp) {
        if (this.globalSerializedTop != serializedTop) {
            const parsedTop = JSON.parse(serializedTop);
            const tmpElem = document.createElement('template'); //top's content must be directly in <body>, so use "template" element as temporary container
            //(can't use outerHTML on an orphan element)
            tmpElem.insertAdjacentHTML('afterbegin', isApp ? wet.builder.appTop(parsedTop) : wet.builder.top(parsedTop)); //eslint-disable-line
            //inject a "marker/tag" class
            for (let e of tmpElem.children) e.classList.add('cdtsblazor-top-tag'); //using `children` and not `childNodes`, we only want elements

            //---[ Remove any elements from previous runs
            document.body.querySelectorAll('.cdtsblazor-top-tag').forEach((e) => e.remove());

            //---[ Install right after body
            for (let i = tmpElem.children.length - 1; i >= 0; i--) {
                const e = tmpElem.children[i];
                document.body.insertAdjacentElement('afterbegin', e);
            }

            this.resetExitScriptForSection();
            this.globalSerializedTop = serializedTop;
        }
    },

    setPreFooter: function setPreFooter(serializedPreFooter) {
        if (this.globalSerializedPreFooter != serializedPreFooter) {
            const parsedPreFooter = JSON.parse(serializedPreFooter);
            const tmpElem = document.createElement('template'); //top's content must be directly in <body>, so use "template" element as temporary container
            //(can't use outerHTML on an orphan element)
            tmpElem.insertAdjacentHTML('afterbegin', wet.builder.preFooter(parsedPreFooter));
            //inject a "marker/tag" class
            for (let e of tmpElem.children) e.classList.add('cdtsblazor-preFooter-tag'); //using `children` and not `childNodes`, we only want elements

            //---[ Remove any elements from previous runs
            document.querySelector('main').querySelectorAll('.cdtsblazor-preFooter-tag').forEach((e) => e.remove());

            //---[ Install right after body
            for (let i = tmpElem.children.length - 1; i >= 0; i--) {
                const e = tmpElem.children[i];
                document.querySelector('main').insertAdjacentElement('beforeend', e);
            }

            this.resetExitScriptForSection();
            this.globalSerializedPreFooter = serializedPreFooter;
        }
    },

    setFooter: function setFooter(seriealizedFooter, isApp) {
        if (this.globalSerializedFooter != seriealizedFooter) {
            const parsedFooter = JSON.parse(seriealizedFooter);
            const tmpElem = document.createElement('template'); //footer's content must be directly in <body>, so use "template" element as temporary container
            //(can't use outerHTML on an orphan element)
            tmpElem.insertAdjacentHTML('afterbegin', isApp ? wet.builder.appFooter(parsedFooter) : wet.builder.footer(parsedFooter)); //eslint-disable-line
            //inject a "marker/tag" class
            for (let e of tmpElem.children) e.classList.add('cdtsfooter-footer-tag'); //using `children` and not `childNodes`, we only want elements

            //---[ Remove any elements from previous runs
            document.body.querySelectorAll('.cdtsfooter-footer-tag').forEach((e) => e.remove());

            //---[ Install right after body
            const children = Array.from(tmpElem.children); //don't want a live list for this, so convert to array
            for (let i = 0; i < children.length; i++) {
                const e = children[i];
                document.body.appendChild(e);
            }

            this.resetExitScriptForSection();
            this.globalSerializedFooter = seriealizedFooter;
        }
    },

    setSectionMenu: function setSectionMenu(serializedSectionMenu) {
        if (this.globalSerializedSectionMenu != serializedSectionMenu) {
            $('#wb-sec, #cdts-main').wrapAll("<div class='container'><div class='row'></div></div>");
            const parsedSectionMenu = JSON.parse(serializedSectionMenu);
            const tmpElem = document.createElement('div');
            //(can't use outerHTML on an orphan element)
            tmpElem.insertAdjacentHTML('afterbegin', wet.builder.secmenu(parsedSectionMenu));
            const element = document.getElementById('wb-sec');
            if (element.childNodes.length > 0) {
                element.replaceChild(tmpElem);
            }
            else {
                element.appendChild(tmpElem);
            }

            this.resetExitScriptForSection();
            this.globalSerializedSectionMenu = serializedSectionMenu;
        }
    },

    installCDTS: async function installCDTS(lang) {
        document.documentElement.setAttribute('lang', lang);
        const cssHref = this.findCDTSCssHref();
        if (cssHref) {
            var cdtsEnvironment = this.deriveCDTSEnv(cssHref);
        }
        else {
            var cdtsEnvironment = this.deriveCDTSEnv(this.defaultCSSHref);
        }
        await this.appendScriptElement(document.head, `${cdtsEnvironment.baseUrl}cdts/compiled/wet-${lang}.js`, 'cdts-main-js', false); //TODO: change from false to sriHash
    },

    appendScriptElement: function appendScriptElement(parentElement, src, id, sriHash) {
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
    },

    deriveCDTSEnv: function deriveCDTSEnv(cssHref) {
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

            this.cdnEnv = cdnEnv;

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
    },

    findCDTSCssHref: function findCDTSCssHref() {
        return Array.from(document.head.querySelectorAll('link[rel="stylesheet"]')).map((e) => e.getAttribute('href')).find((href) => href?.includes('/cdts/cdts-')) || null;
    },

    resetExitScript: function resetExitScript(exitScriptObj) {
        var elems = document.getElementsByTagName('a');
        wet.utilities.wetExitScript(
            exitScriptObj.displayModal != null ? exitScriptObj.displayModal.toString() : 'undefined',
            exitScriptObj.exitURL != null ? exitScriptObj.exitURL : 'undefined',
            exitScriptObj.exitDomains != null ? exitScriptObj.exitDomains : 'undefined',
            exitScriptObj.exitMsg != null ? exitScriptObj.exitMsg : 'undefined',
            exitScriptObj.yesMsg != null ? exitScriptObj.yesMsg : 'undefined',
            exitScriptObj.cancelMsg != null ? exitScriptObj.cancelMsg : 'undefined',
            exitScriptObj.msgBoxHeader != null ? exitScriptObj.msgBoxHeader : 'undefined',
            exitScriptObj.targetWarning != null ? exitScriptObj.targetWarning : 'undefined',
            exitScriptObj.displayModalForNewWindow != null ? exitScriptObj.displayModalForNewWindow.toString() : 'undefined',
            elems);
    },

    resetExitScriptOnPage: function resetExitScriptOnPage() {
        if (this.exitScriptObj != null && this.exitScriptObj.exitScript && (this.exitScriptObj.displayModal || this.exitScriptObj.exitURL != "" || this.exitScriptObj.exitURL != null)) this.resetExitScript(this.exitScriptObj);
    },

    resetWetComponents: function resetWetComponents(component) {
        if (typeof $ === 'undefined') return;

        if (Array.isArray(component)) {
            for (let n of component) {
                $(`.${n}`).trigger('wb-init');
            }
        }
        else {
            $(`.${component}`).trigger('wb-init');
        }
    },

    resetExitScriptForSection: function resetExitScriptForSection() {
        if (this.exitScriptObj != null && this.exitScriptObj.exitScript && (this.exitScriptObj.displayModal || this.exitScriptObj.exitURL != "" || this.exitScriptObj.exitURL != null)) this.resetExitScript(this.exitScriptObj);
    },

    //Functions required by the ChangLang component
    SetCurrentPage: function SetCurrentPage(page) {
        this.currentPage = page;
    },

    GetCurrentPage: function GetCurrentPage() {
        // Invoke to call C# function from JavaScript.
        DotNet.invokeMethodAsync("GoC.WebTemplate.Blazor", "GetCurrentPage", this.currentPage);
    }
};
