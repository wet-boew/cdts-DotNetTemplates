<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Application.Master" AutoEventWireup="true" CodeBehind="ApplicationTemplate.aspx.cs" Inherits="ApplicationTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    
    
    
    <h1>GoC Web Template Samples - Standard Application Settings Sample</h1>
<section class="alert alert-danger">
    <h2>Notice for Implementers</h2>
    <p>Please ensure you have permission from your department, TBS or Principal Publisher before proceeding. The changes below DO NOT follow the <a rel="external" class="alert-link" href="http://www.gcpedia.gc.ca/wiki/Canada.ca_Content_and_Information_Architecture_Specification">C&IA specifications document</a> </p>
</section>

<p><a rel="external" href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>


<div class="alert alert-warning">
    <h2>Left Menu Variant</h2>
    <p>There is also a left menu version of this template it's available by using the <code class="wb-prettify">GoCWebTemplate.Application.LeftMenu_Layout.cshtml</code> layout for your page.</p>
    <p>The Left Menu is implmented in the same way as the Left Menu for the other templates. See <a href="LeftSideMenuSample.aspx">Left Side Menu Sample</a> on how to implement this.</p>
</div>

<h2>Application Name</h2>
<p>
    This setting determines the title for your site. Set programmatically by the <code class="wb-prettify">ApplicationName.Title</code>
    property of the web template
</p>

<div class="wb-prettify all-pre lang-c# linenums">
    <pre>//Set the name of the web application
WebTemplateCore.ApplicationTitle.Text ="Application Name"; 
</pre>
</div>


<h2>Change Language Link</h2>
<p>You can set a custom link for changing the language of your application</p>
<p>This can be set programmatically using the <code class="wb-prettify">LanguageLink.Href</code> property</p>
<div class="wb-prettify all-pre lang-c# linenums">
    <pre>
//Set the link to be used for changing languages.
WebTemplateCore.LanguageLink.Href = "about:blank";
</pre>
</div>

<h2>Custom Footer Links</h2>
<p>
    A list of <code class="wb-prettify">FooterLinks</code> is available for you to provide your own links in the application footer
</p>
<p>
    <code class="wb-prettify">FooterLinks</code> expects a list of <code class="wb-prettify">FooterLink</code> objects that have the following properties.
</p>
<ul>
    <li><code class="wb-prettify">Href</code> the HREF for the footer link</li>
    <li><code class="wb-prettify">Text</code> the text to display on the link</li>
    <li><code class="wb-prettify">NewWindow</code> a flag to specify if this link opens in a new tab, defaults to false</li>
</ul>
<div class="wb-prettify all-pre lang-c# linenums">
<pre>
WebTemplateCore.FooterLinks.Add(new FooterLink {
    Href = "about:blank",
    Text = "Link Text 1"
});
WebTemplateCore.FooterLinks.Add(new FooterLink {
    Href = "about:blank",
    Text = "Link Text 2", 
    NewWindow = true
})
</pre>
</div>



<h2>Custom Search</h2>
<p>This setting allows you to override the default search behaviour.</p>
<p><code class="wb-prettify">CustomSearch</code> You need to contact the CDTS team to enable this option, as it needs to be added to the CDTS Template.</p>
<div class="wb-prettify all-pre lang-c# linenums">
<pre>
//Use the SDS Custom Search from the templates
WebTemplateCore.CustomSearch ="SDS"; 
</pre>
</div>

<h3>Features</h3>
<p>his will be used by the Principal Publisher to insert GoC activities and initiatives into your page. By default this is ALWAYS shown on all pages. You will need authorization from the Principal Publisher to not include this content in your web asset.</p>
<p> If you receive such authorization then you can programmatically turn this off using the <code class="wb-prettify">ShowFeatures</code> or in the web.config</p>

<div class="wb-prettify all-pre lang-c# linenums">
<pre>
//Turn off the features in the footer 
WebTemplateCore.ShowFeatures = true; 
</pre>
</div>

<h2>Global Nav</h2>
<p>This setting determines if the global navigation menu for Canada.ca is displayed in the footer.</p>
<p>This is set programmatically by the <code class="wb-prettify">ShowGlobalNav</code> property of the application template and can also be set in the web.config </p>
<div class="alert alert-warning">
    <p>Setting this to true will override the default footer, custom footer links, and transactional footer</p>
</div>

<div class="wb-prettify all-pre lang-c# linenums">
<pre>
//Show the Global Navigation in the footer 
WebTemplateCore.ShowGlobalNav = true; 
</pre>
</div>

<h2>Hide Search</h2>
<p>This setting allows you to hide or show the search bar on the page.</p>
<p>The setting <code class="wb-prettify">ShowSearch</code> can be set programmatically or in the web.config.  This setting defaults to true.  </p>
<div class="wb-prettify all-pre lang-c# linenums">
<pre>
//Use ShowSearch to hide the search field on the page.
WebTemplateCore.ShowSearch = false; 
</pre>
</div>

<h2>Pre-Content</h2>
<p> This will be used by Principal Publisher to insert content into the pre content space of your page. By default this is ALWAYS shown on all pages. You will need authorization from the Principal Publisher to not include this content in your web asset</p>
<p>If you recieve such authorization then you can turn off the pre-content programmatically using the <code class="wb-prettify">ShowPreContent</code> flag or in the web.config.</p>
    <div class="wb-prettify all-pre lang-c# linenums">
<pre>//show or hide the pages pre-content 
WebTemplateCore.ShowPreContent = false; 
</pre>
</div>

<h2>Site Menu</h2>
<p>The site menu can be hidden or overridden using the following settings</p>
<ul>
    <li><code class="wb-prettify">ShowSiteMenu</code> is used to hide the site menu. This defaults to true. can be set in web.config</li>
    <li>
        <code class="wb-prettify">CustomSiteMenuURL</code> this variable is used to display a custom menu.
        <p>Although you don't need to we suggest you contact the cdts team to enable this option. if you have done this before then simply provide the url to your valid menu file to enable this option. </p>

        <p>You can copy the format in the sample custom menu file provided below in our sample.</p>
        <a href="https://ssl-templates.services.gc.ca/app/cls/wet/gcweb/v4_0_24/cdts/custommenu-en.html">https://ssl-templates.services.gc.ca/app/cls/wet/gcweb/v4_0_24/cdts/custommenu-en.html</a>
    </li>
</ul>

<div class="wb-prettify all-pre lang-c# linenums">
    <pre>//show or hide the site menu
WebTemplateCore.ShowSiteMenu = true; 
//set the custom site menu url
WebTemplateCore.CustomSiteMenuURL ="https://ssl-templates.services.gc.ca/app/cls/wet/gcweb/v4_0_24/cdts/custommenu-en.html";
</pre>
</div>

<h2>Secure Icon</h2>
<p>
    <code class="wb-prettify">ShowSecure</code> is a boolean variable that is used to either show or hide the secure icon
</p>

<div class="wb-prettify all-pre lang-c# linenums">
    <pre>//Show the lock icons
WebTemplateCore.ShowSecure = true; 
</pre>
</div>

<h2>Sign In & Out</h2>
<p>You are able to specify the visibility of and the url for sign in and sign out buttons on the application menu.</p>

<div class="alert alert-warning">
    <p>
        <code class="wb-prettify">ShowSignInLink</code> and <code class="wb-prettify">ShowSignOutLink</code> cannot both be set to true at the same time.
    </p>
</div>
<h3>Sign In</h3>
<ul>
    <li> <code class="wb-prettify">ShowSignInLink</code> is a boolean variable that is used to either show or hide the Sign In Button </li>
    <li>
        <code class="wb-prettify">ShowSignInURL</code> is the location of your sign in page for this web application, this can also be set in the web.config.
        If this is set in the web.config it should navigate to a page that is hooked into the applications localization.
    </li>
</ul>

<div class="wb-prettify all-pre lang-c# linenums">
    <pre>
//Show the sign in button
WebTemplateCore.ShowSignInLink = true; 
//The URL to your applications sign in page.
WebTemplateCore.SignInLinkURL = "about:blank"; 
</pre>
</div>

<h3>Sign Out</h3>
<ul>
    <li> <code class="wb-prettify">ShowSignOutLink</code> is a boolean variable that is used to either show or hide the Sign Out Button </li>
    <li> <code class="wb-prettify">ShowSignOutURL</code> is the location of your sign out service. </li>
</ul>
<div class="wb-prettify all-pre lang-c# linenums">
    <pre>//Show the sign out button
WebTemplateCore.ShowSignOutLink = true; 
//The URL to your applications sign out service.
WebTemplateCore.SignOutLinkURL = "about:blank"; 
</pre>
</div>

<h2>Terms and Condition and Privacy Link</h2>
<p>See the example at the <a href="TransactionalSample.aspx">Transactional Sample Page</a> for how to implement this.</p>

<!-- #include virtual="SamplesNavigation.html" -->

</asp:Content>
