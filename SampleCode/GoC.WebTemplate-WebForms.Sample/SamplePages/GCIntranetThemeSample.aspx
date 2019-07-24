<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Master" AutoEventWireup="true" CodeBehind="GCIntranetThemeSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.GCIntranetThemeSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1>GoC Web Template Samples - GCIntranet Theme</h1>
<p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>

<p>This sample page provides the basic items to configure when required to use the GCIntranet theme.</p>

<h2>CDNEnvironment</h2>
<p>Set the web.config <code class="wb-prettify">GoC.WebTemplate.environment</code> to the environment of choice (other than Akamai) example "ESDC_PROD". It can also be set programmatically via <code class="wb-prettify">WebTemplateCore.CDNEnvironment</code>.</p>
    
<div class="wb-prettify all-pre lang-vb linenums">
    <h3>Web Config Sample</h3>
    <pre>
&lt;GoC.WebTemplate environment="ESDC_PROD"&gt;
    </pre>

    <h3>C# Code Sample</h3>
    <pre>
WebTemplateMaster.WebTemplateCore.CDNEnvironment = "ESDC_PROD";       
    </pre>
</div>

<h2>Sub Themes Title</h2>
<p>The Sub Theme intranet Title can be customized in the code though <code class="wb-prettify">WebTemplateCore.IntranetTitle</code></p>

<div class="wb-prettify all-pre lang-vb linenums">
    <h3>C# Code Sample</h3>
    <pre>
WebTemplateMaster.WebTemplateCore.IntranetTitle = new IntranetTitle
{
    Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-esdc-en.shtml",
    BoldText = "ESDC Sub",
    Acronym = "Employment and Social Development Canada Sub Theme",
    Text = " Custom Title"
};        
    </pre>
</div>

<h2>Application Title</h2>
<p>When using the "GCIntranet" theme in your application, you can optionally set the Application Title text and its url. The application title is displayed at the top of the page, above the menu.</p>
<p>Set programmatically via the <code class="wb-prettify">"WebTemplateCore.ApplicationTitle.Text"</code>.</p>
<p>The URL for the Application Title is optional.  If left blank the subTheme will determine the URL to use.  You can override the default Href value of the subTheme by setting programmatically the <code class="wb-prettify">"WebTemplateCore.ApplicationTitle.Href"</code> property of the Web Template.</p>

<div class="wb-prettify all-pre lang-vb linenums">
    <h3>Web Config Sample</h3>
    <pre>
&lt;GoC.WebTemplate 
        environment="ESDC_PROD" 
        </pre>
</div>

<div class="wb-prettify all-pre lang-vb linenums">
    <h3>C# Code Sample</h3>
    <pre>
WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";           
        </pre>
</div>
   <!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>
