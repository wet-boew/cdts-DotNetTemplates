<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Master" AutoEventWireup="true" CodeBehind="ExtendedBasePageSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.ExtendedBasePageSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="wb-cont">GoC Web Template Samples - Extended BasePage</h1>
    <p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>

    <p>This page provides an example on how to extend the <code class="wb-prettify">BasePage</code> provided by the Web Template.  The <code class="wb-prettify">BasePage</code> provided by the Web Template has very little functions and therefore applications may wish to extend its functionality. This will be useful should you require to add your own common logic that will be used by your applications' pages.</p>

    <p>In this sample, we've created a new class <code class="wb-prettify">"ExtendedBasePage"</code>, which extend the BasePage from the Web Template and it includes the following:</p>
    <ul>
        <li>A function <code class="wb-prettify">"GetWeather"</code>, which returns a string.</li>
        <li>A property <code class="wb-prettify">"SessionID"</code>, which returns the ID of the user's session</li>
        <li>Sets the <code class="wb-prettify">"GoC.WebTemplate.HeaderTitle"</code> of the WebTemplate in the page_init.  This would set the page title for all pages of the application.</li>
    </ul>
    <p>The web page inherits from the <code class="wb-prettify">"ExtendedBasePage"</code> class, which allows it to have access to the properties and methods of the <code class="wb-prettify">"ExtendedBasePage"</code> but also the properties and methods of the Web Template <code class="wb-prettify">"BasePage"</code>.  In the sample the SessionID and GetWeather are called and the value is displayed on screen.</p>

    <h2>Level of inheritance:</h2>
    <ul>
        <li>Your pages will inherit from your Extended Base Page</li>
        <li>Your Extended Base Page will inherit from the <code class="wb-prettify">BasePage</code> of the Web Template</li>
        <li>The <code class="wb-prettify">BasePage</code> of the Web Template inherits from <code class="wb-prettify">System.UI.Page</code></li>
    </ul>

    <h2>Steps to Extend the Template BasePage</h2>
    <ul>
        <li>Create a new class</li>
        <li>Inherit from <code class="wb-prettify">"GoC.WebTemplate.BasePage"</code></li>
        <li>Include the logic required for your application</li>
    </ul>
    <h2>Steps when adding your Web Pages</h2>
    <ul>
        <li>Create a new Web Page</li>
        <li>Change the inheritance from <code class="wb-prettify">System.UI.Web</code> to your Extended Base Page class</li>
    </ul>
    
    <h2>Values from the "ExtendedBasePage"</h2>
    <div>
        Session ID: <asp:Label ID="lblSessionID" runat="server" Text=""></asp:Label>
    </div>
    <div>
        Today's Weather: <asp:Label ID="lblWeather" runat="server" Text=""></asp:Label>
    </div>
    <!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>
