<%@ Page Title="" Language="C#" MasterPageFile="~/GoC.WebTemplate/GoCWebTemplate.Transactional.Master" AutoEventWireup="true" CodeBehind="TransactionalSample.aspx.cs" Inherits="TransactionalSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>GoC Web Template Samples - Transactional look</h1>
    <p><a href="http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide">Web Template Documentation (GCPedia)</a></p>

    <p>This sample page provides an example of a page using the Transactional Master/Layout Page of the Web Template.</p>

    <p>Customizing the Transactional site can be performed in the same manner as the regular GoC Web Template Master/Layout Page. However 2 items are specific to the transactional mode and that is the url for the Terms and Conditions and the Privacy links at the bottom of the page. These 2 URL can be configured programmatically.</p>
    <form runat="server">
        <div class="form-horizontal">
             <div class="form-group">
                <label for="data1" class="col-sm-3 control-label">Form&#32;<code>input</code></label>
                <div class="col-sm-9">
                    <input type="text" id="data1" name="data1" class="form-control">
                </div>
            </div>
            <div class="form-group">
                <label for="data2" class="col-sm-3 control-label">Form&#32;<code>textarea</code></label>
                <div class="col-sm-9">
                    <textarea id="data2" rows="3" cols="15" name="data2" class="form-control"></textarea>
                </div>
            </div>
            <div class="form-group">
                <label for="data4" class="col-sm-3 control-label">Form&#32;<code>select</code></label>
                <div class="col-sm-9">
                    <select name="data4" id="data4" class="form-control">
                        <option value="1">Option&#32;1</option>
                        <option value="2">Option&#32;2</option>
                        <option value="3">Option&#32;3</option>
                        <option value="4">Option&#32;4</option>
                    </select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <button type="submit" class="btn btn-primary">Submit</button>
                </div>
            </div>
        </div>
     </form>

    <div class="wb-prettify all-pre lang-vb linenums">
        <h3>C# Code Sample</h3>
        <pre>
//set the Terms and Condition Link
this.WebTemplateMaster.WebTemplateCore.TermsConditionsLink_URL = "http://www.tsn.ca";
//set the Privacy link
this.WebTemplateMaster.WebTemplateCore.PrivacyLink_URL = "http://www.lapresse.ca";
        </pre>
    </div>
<!-- #include virtual="SamplesNavigation.html" -->
</asp:Content>
