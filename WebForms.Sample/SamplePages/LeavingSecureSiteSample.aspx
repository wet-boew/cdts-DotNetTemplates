<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Master" AutoEventWireup="true" CodeBehind="LeavingSecureSiteSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.LeavingSecureSiteSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>GoC Web Template Samples - Leaving Secure Site</h1>
    <p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>
    
    <p>In certain scenarios (ex: secure sites) we want to notify the user that the link or action they have just performed will exit the current secured site/session and it is possible that data could be lost. The message allows the user to cancel the redirect or continue with the redirect.</p>
    <p>This feature can be enabled in the Web Template and will:</p>
    <ul>
        <li>inform the user via a modal window that they are leaving a secure site.</li>
        <li>display (or not) the message your application provides</li>
        <li>allow your application to execute any clean up code (ex: close session, gracefully logout user etc...)</li>
        <li>allow your application to exlude any domains from raising the warning</li>
        <li>you can choose to not display the modal window and send the user to a suplied redirect page to before the tasks listed above.</li>
    </ul>

    <h2>How it works</h2>
    <ul>
        <li>If <code class="wb-prettify">DisplayModalWindow</code> is set to true (default):
            <ul>
                <li>When the user clicks an external link, the modal window will be displayed to the user.</li>
                <li>A "Cancel" button appears on the window to allow the user to return to their page. (Text programmable, see below.)</li>
                <li>A "Yes" button appears on the window to allow the user to continue with the redirection to the selected link. (Text programmable, see below.)</li>
            </ul>
        </li>
        <li>if the "Yes" button is clicked on the modal window or if <code class="wb-prettify">DisplayModalWindow</code> was set to false:
            <ul>
                <li>the user will first be redirect to the url set in <code class="wb-prettify">"LeavingSecureSiteWarning.URL"</code></li>
                <li>the info of the linked that was clicked is part of the querystring</li>
                <li>this page should be invisible to the user.</li>
                <li>this page is there to execute any clean up code your application requires</li>
                <li>once executed this page redirects the user to the url of the clicked link</li>
            </ul>
        </li>
    </ul>

    <p>Here is a local link that will not display the warning: <a href="BaseSettingsSample.aspx">Link to Local Page</a></p>
    <p>Here is an external link that will display the warning:<a href="https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/Redirect-Page">Link to External Page</a></p>

    <h2>Steps to implement:</h2>
    <h3>Enable the leaving secure site feature</h3>
    <ul>
        <li>Set, via the web.config or programmatically, <code class="wb-prettify">"leavingSecureSiteWarning.Enabled"</code> to "true"</li>
        <li>Determine if the modal window should be displayed by setting the <code class="wb-prettify">"LeavingSecu.eSiteWarning.DisplayModalWindow"</code> property of the Web Template programmatically.</li>
        <li>Provide the message to be displayed by setting the <code class="wb-prettify">"LeavingSecureSiteWarning.Message"</code> property of the Web Template programmatically.</li>
        <li>Set, via the web.config or programmatically, <code class="wb-prettify">"LeavingSecureSiteWarning.URL"</code> to your page which will execute your clean up code and then redirect to the selected url.</li>
        <li>Set, via the web.config or programmatically, <code class="wb-prettify">"LeavingSecureSiteWarning.ExcludedDomains"</code> the list of domains you do not want to raise the warning. 
            <ul>
                <li>Do not include "http://" in your list, domains can start with "www" or "esdc.gc.ca".</li>
                <li>If you have multiple links to the same URL, but some links start with "www" and others don't, you will need to include both domains in the collections.</li>
            </ul>   
        </li>
        <li>Optionally provide a cancel message by setting the <code class="wb-prettify">LeavingSecureSiteWarning.CancelMessage</code> programatically. It will default to "Cancel".</li>
        <li>Optionally provide a yes message by setting the <code class="wb-prettify">LeavingSecureSiteWarning.YesMessage</code> programatically. It will default to "Yes".</li>
    </ul>
    <h3>Create your "redirect" page</h3>
    <ul>
        <li>Create a new web page</li>
        <li>In the <code class="wb-prettify">page_load</code> event</li>
        <li>enter your clean up code if required</li>
        <li>redirect to the <code class="wb-prettify">"targetURL"</code> in the querystring</li>
    </ul>
    
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample to enable the warning</h3>
        <pre>
//note: other then the message the rest could be set in the web.config
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = true;
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.RedirectUrl = "redirect.aspx";
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";  
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.CancelMessage = "NO NO NO NO!!!!";
WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.YesMessage = "mmmm Fine.";
        </pre>
    </div>

    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample for your Redirect page</h3>
        <pre>
protected void Page_Load(object sender, EventArgs e)
{
    //this page has no visual and will not be displayed to the user

    string targetURL = Server.HtmlDecode(this.Request.QueryString.Get("targetUrl"));

    //add any necessary clean up code (clear session, logout user, etc...)

    //redirect user to link they had clicked
    if (!string.IsNullOrEmpty(targetURL)) Response.Redirect(targetURL);

    // decide how you want to handle this situation
    throw new ApplicationException("targetURL must be specified.");
}
        </pre>
     </div>
    <!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>
