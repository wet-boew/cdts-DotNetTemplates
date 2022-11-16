<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Master" AutoEventWireup="true" CodeBehind="AdobeAnalyticsSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.AdobeAnalyticsSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>GoC Web Template Samples - Adobe Analytics</h1>
    <p><a href="https://cenw-wscoe.github.io/sgdc-cdts/docs/templates-en.html">Web Template Documentation</a></p>

    <p>This sample page demonstrates how your application can incorporate Adobe Analytics via the GoC Web Template.</p>

    <p>Set programmatically via <code class="wb-prettify">"WebAnalytics"</code> which has four properties:</p>
    <ul>
        <li><code class="wb-prettify">"active"</code>: set to true to enable analytics.</li>
        <li><code class="wb-prettify">"environment"</code>: the environment in which analytics will run (staging or production).</li>
        <li><code class="wb-prettify">"version"</code>: a choice between 1 or 2.</li>
        <li><code class="wb-prettify">"custom"</code>: if the environment/version options do not satisfy, you can provide a custom string.</li>
    </ul>

    <h3>Using Adobe Analytics <em>version</em> 1 on staging <em>environment</em>.</h3>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Active = true;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Environment = GoC.WebTemplate.Components.Entities.WebAnalytics.EnvironmentOption.staging;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Version = 1;        
        </pre>
    </div>

    <h3>Using Adobe Analytics <em>version</em> 2 on production <em>environment</em>.</h3>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Active = true;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Environment = GoC.WebTemplate.Components.Entities.WebAnalytics.EnvironmentOption.production;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Version = 2; 
        </pre>
    </div>

    <h3>Using Adobe Analytics <em>version</em> 3 on staging <em>environment</em>.</h3>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Active = true;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Custom = "launch-EN11c0261481f74c56b7656937bbd995e9-staging.min.js";
        </pre>
    </div>

    <h3>Using Adobe Analytics <em>version</em> 3 on production <em>environment</em>.</h3>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Active = true;
WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Custom = "launch-EN0cf6c2810a2b48f8a4c36502a1b09541.min.js";
        </pre>
    </div>
    <!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>