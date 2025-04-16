﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Master" AutoEventWireup="true" CodeBehind="SessionTimeoutSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.SessionTimeoutSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 id="wb-cont">GoC Web Template Samples - Session Timeout</h1>

    <asp:Label ID="lblID" runat="server"></asp:Label>

    <p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>

    <p>This sample helps web page owners by providing session timeout and inactivity timeout functionality and is based on the <a href="http://wet-boew.github.io/wet-boew/demos/session-timeout/session-timeout-en.html">WET Session Timeout plugin</a>. When a user requests a page with this plugin implemented their session will begin. After the specified session period, they will be notified that their session is about to timeout. At this point, they will have the option to remain logged in by clicking "Continue session", or signing out by clicking "End session now".</p>

    <p>At any time during the session, if the user remains idle for a specified amount of time, they will be notified that they're session is about to timeout. In either case, if the user does not respond to the timeout notification within a specified amount of time, once they click either "Continue session" or "End session now" they will be automatically redirected to the sign out page.</p>

    <p>The WET plugin is responsible for the pop up to display.  It is up to you to write the neccessary code to update the session timeout on the server as the plug in does not take care of this.  This sample has a complete example on how to implement the plug in properly and update your server session.</p>

    <h4>Configurations</h4>
    <h5>To Enable the session timeout plug in:</h5>
    <ul>
        <li>Set the key <code class="wb-prettify">"GoC.WebTemplate.sessionTimeOut.enabled"</code> in the web.config to "true".</li>
        <li>or set the property programmatically. <code class="wb-prettify">"this.WebTemplateMaster.WebTemplateModel.SessionTimeout.Enabled = true;"</code></li>
    </ul>
    <p>The rest of the configuration are set in the web.config or programmatically under <code class="wb-prettify">"GoC.WebTemplate.SessionTimeout":</code></p>
    <ul>
        <li><code class="wb-prettify">inactivity</code>: inactivity period of time after which the modal dialog will appear (default 20 minutes)</li>
        <li><code class="wb-prettify">reactionTime</code>: period of time the user has to perform an action once the modal dialog is displayed (default 3 minutes)</li>
        <li><code class="wb-prettify">sessionalive</code>: period of time for the session to stay alive until the modal dialog appears (default 20 minutes)</li>
        <li><code class="wb-prettify">logouturl</code>: URL that users are sent to when the session has expired.  This page can be invisible to the user and should perform any necessary clean up code.</li>        
        <li><code class="wb-prettify">refreshCallbackUrl</code>: URL used to perform an ajax request to determine the validity of the session. This page is invisible to the user and will return "true" is the server session is still valid, and "false" if it is expired or no longer valid.</li>
        <li><code class="wb-prettify">refreshOnClick</code>: Determines if clicking on the web page should reset the inactivity timeout and perform an ajax request (if a refreshCallbackUrl has been specified).</li>
        <li><code class="wb-prettify">refreshLimit</code>: Sets the amount of time that must pass before an ajax request can be made</li>
        <li><code class="wb-prettify">method</code>: Sets the request method used for ajax requests. Recommended: GET or POST</li>
        <li><code class="wb-prettify">additionalData</code>: Additional data to send with the request</li>
        <li><code class="wb-prettify">signInUrl</code>: URL to the Sign-In page</li>
    </ul>
    <p>Override the default text and message elements programmatically under <code class="wb-prettify">TextOverrides</code> using the SessionTimeoutTextOverrides object: </p>
    <ul>
        <li><code class="wb-prettify">buttonContinue</code>: Text for the Continue Session button</li>
        <li><code class="wb-prettify">buttonEnd</code>: Text for the End Session button</li>
        <li><code class="wb-prettify">buttonSignin</code>: Text for the Sign In button</li>
        <li><code class="wb-prettify">timeoutEnd</code>: Text for the message displayed below the timer</li>
        <li><code class="wb-prettify">timeoutAlready</code>: Text for the message displayed when the session has expired</li>
    </ul>

    <h4>Notes:</h4>
    <ul>
        <li>The <code class="wb-prettify">inactivity, reactionTime and sessionalive</code> parameters are set in milliseconds. For help with the time values, use this time converter.</li>
        <li>Your <code class="wb-prettify">sessionalive and inactivity</code> parameters should be equal to your web server session alive time minus the reactionTime time. If you set your <code class="wb-prettify">sessionalive time and inactivity time</code> to the same as your web server without taking into consideration the <code class="wb-prettify">reactionTime time</code> then the session will have ended by the server as soon as the popup appears to extend the session.</li>
        <li>The server response needs to contain a message body. Don't use a request method (e.g. HEAD) that disallows a message body in the response.</li>
    </ul> 
    
    <h4>In this sample</h4>
    <p>In this sample we have:</p>
    <ul>
        <li>Set the server session timeout to 1 minute</li>
        <li>Set the <code class="wb-prettify">Inactivity and Sessionalive</code> to 30 seconds. This means the popup will be displayed every 30 seconds.</li>
        <li>Set the <code class="wb-prettify">reactionTime</code> to 10 second. This gives the user 10 seconds to make a selection once the pop up is displayed</li>
        <li>Set the <code class="wb-prettify">logoutURL</code> to "logout.aspx".  This page will clean up the session and redirect the user to another page, simulating a proper logout flow.</li>
        <li>Set the <code class="wb-prettify">refreshCallbackUrl</code> to "SessionValidity.aspx".  This page ensures the server session is still valid and return "true" if it is and "false" if the session is expired or no longer valid.</li>
        <li>Set the <code class="wb-prettify">refreshOnClick</code> to "false" so that clicks made by the user on the page does not trigger a call to the url set in the "refreshCallbackURL" to validate the server session.</li>
    </ul>

    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
//enable the sessionTimeout feature
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Enabled = true;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Inactivity = 30000;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.ReactionTime = 10000;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.SessionAlive = 30000;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.LogoutUrl = "Logout.aspx";            
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "SessionValidity.aspx";
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshOnClick = false;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshLimit = 3;
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Method = "";
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.AdditionalData = "";
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.SignInUrl = "";
WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.TextOverrides = new Components.Entities.SessionTimeoutTextOverrides()
{
    ButtonContinue = "Continue Button",
    ButtonEnd = "End Button",
    ButtonSignIn = "Sign In Button",
    TimeoutAlready = "Timeout Already",
    TimeoutEnd = "Timeout End"
};
        </pre>
    </div>

     <div class="wb-prettify all-pre lang-vb linenums">
        <h3>SessionValidity.aspx</h3>
        <pre>
protected void Page_Load(object sender, EventArgs e)
{  
//returns "true" if the original session is still alive
Response.Write((!Session.IsNewSession).ToString().ToLower()); 
Response.End();
}
        </pre>
    </div>

     <div class="wb-prettify all-pre lang-vb linenums">
        <h3>Logout.aspx</h3>
        <pre>
protected void Page_Load(object sender, EventArgs e)
{
//destroy users sessions
this.Session.Abandon();

//perform any other clean up that needs to occur for your application

//redirect to the page of your preference
Response.Redirect("BaseSettingsSample.aspx");
}
        </pre>
    </div>
   <!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>
