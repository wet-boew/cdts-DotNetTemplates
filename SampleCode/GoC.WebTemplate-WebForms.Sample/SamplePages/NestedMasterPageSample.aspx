<%@ Page Title="" Language="C#" MasterPageFile="~/SamplePages/NestedMasterPageSample.master" AutoEventWireup="true" CodeBehind="NestedMasterPageSample.aspx.cs" Inherits="GoC.WebTemplate.WebForm.Sample.SamplePages.NestedMasterPageSample1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContent" runat="server">
   <h1>GoC Web Template Samples - Nested Master Page</h1>
    <p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>

    <p>This sample demonstrates how a Nested Master Page could be implemented with the GoC Web Template.</p>
    
    <p>Nested master pages can be used to customize the layout of the parent master pages' content section, provide a central location to add controls needed on each page of your site and/or common functionality.</p>

    <p>The sample uses a Nested Master page to:</p>
    <ul>
        <li>Divide the 1 column GoC Web Template Master Page layout into 2 columns.  Note: This same approach could be used with the SideMenu Master Page of the Web template.</li>
        <li>Display a Local Weather Widget in the right column of all pages.  By using a nested master page, we could include this widget centrally in the nested master page once instead of including it on every web page of the site.</li>
        <li>Show that the nested master page can still access the functionality of the Web Template Master Page.  The nested master page sets the <code class="wb-prettify">"DateModified"</code> property of the GoC.WebTemplate Master Page.</li>
        <li>Show that the web page can still access the functionality of the Web Template Master Page.  The web page sets the <code class="wb-prettify">"HeaderTitle"</code> property of the GoC.WebTemplate Master page.</li>
    </ul>
     <h2>Steps to implement</h2>
    
    <ul>
        <li>Download the latest "GoC.WebTemplate" package from the internal NuGet source.</li>
        <li>Create a "Nested Master Page"
            <ul>
                <li>From your project select "Add/New Item"</li>
                <li>Select "Web Forms Master Page (Nested)"</li>
                <li>Give it a proper name, click "Add"</li>
                <li>Select the desired master page from the web template ex: "./GoC.WebTemplate/GoCWebTemplate.Master"</li>
                <li>Click "OK"</li>
            </ul>
        </li>
        <li>Modify the Nested Master Page to meet your requirements (create layout, add contentPlaceHolders, etc...)</li>
        <li>Add a "Web Page"
            <ul>
                <li>From your project select "Add/New Item"</li>
                <li>Select "Web Form with Master Page"</li>
                <li>Give it a proper name, click "Add"</li>
                <li>Select the Nested Master Page you just created</li>
                <li>Click "OK"</li>
            </ul>
            </li>
        <li>Modify the Web Page to meet your requirements</li>
        <li>Your web page should inherit from the GoC.WebTemplate.BasePage</li>
    </ul>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>HTML Code Sample of the Nested Master Page</h3>
        <pre>
&lt;asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"&gt;
    &lt;div class="row"&gt;
        &lt;!-- left column--&gt;
        &lt;section class="col-md-8"&gt;
            &lt;asp:ContentPlaceHolder ID="LeftContent" runat="server"&gt;&lt;/asp:ContentPlaceHolder&gt;
        &lt;/section&gt;
        &lt;!-- right column--&gt;
        &lt;aside class="col-md-4 mrgn-tp-lg"&gt;
            &lt;!-- include weather widget here so it is displayed on all pages--&gt;
            &lt;div class="well"&gt;
                &lt;section class="wb-feeds limit-1"&gt;
	                &lt;h3&gt;Ottawa Weather&lt;/h3&gt;
	                &lt;ul class="feeds-cont"&gt;
		                &lt;li&gt;
			                &lt;a href="http://weather.gc.ca/rss/city/on-118_e.xml" rel="external">Ottawa Ontario&lt;/a&gt;
		                &lt;/li&gt;
	                &lt;/ul&gt;
                &lt;/section&gt;
                &lt;!-- include a content place holder should the page require to add content to the right column below the weather widget--&gt;
                &lt;asp:ContentPlaceHolder ID="RightContent" runat="server">&lt;/asp:ContentPlaceHolder&gt;
            &lt;/div&gt;
        &lt;/aside&gt;
    &lt;/div&gt;
&lt;/asp:Content&gt;
        </pre>
    </div>
    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample of the Nested Master Page</h3>
        <pre>
protected void Page_Load(object sender, EventArgs e)
{
    //Set the goc web template date modified here. this would set the date for all pages of my site that inherit from the nested master page
    ((GoC.WebTemplate.WebTemplateMasterPage)this.Master).DateModified = DateTime.Now;

}
        </pre>
    </div>

     <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample of the Web Page</h3>
        <pre>
public partial class NestedMasterPageSample1 : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //set the page title
        this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "Nested Master Page Sample";
    }
}
        </pre>
    </div>
   <!-- #include virtual="SamplesNavigation.html" -->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">

</asp:Content>
