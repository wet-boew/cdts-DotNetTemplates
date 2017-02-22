<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WSEMailSampleCode.aspx.vb" Inherits="SampleCode.VB.WSEMailSampleCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WSEMail Sample Code</title>
</head>

<!-- The following javascript is used:
        -to display the proper input field based on the selected action 
        -manage the collapsable request XML and response XML sections
 -->
<script type="text/javascript">
  

    function toggle(toggletype) {
        var strTriangle = "";
        var strContainer = "";
        if (toggletype == "request") {
            strTriangle = "requestXmlTriangle";
            strContainer = "requestXmlContainer";
        }
        else {
            strTriangle = "responseXmlTriangle";
            strContainer = "responseXmlContainer";
        }

        if (document.getElementById(strTriangle).title == "expand") {
            document.getElementById(strTriangle).title = "collapse";
            document.getElementById(strTriangle).innerHTML = "&#9650;";
            document.getElementById(strContainer).style.display = "inline";
        }
        else {
            document.getElementById(strTriangle).title = "expand";
            document.getElementById(strTriangle).innerHTML = "&#9660;";
            document.getElementById(strContainer).style.display = "none";
        }
    }
</script>

<body>
    <form id="form1" runat="server">
    <div>
        <h1>WSEMail Sample Code</h1>
    </div>
    <div><h3><a href="http://architecture/SF-ML/softwarefactory/WSEMail/Forms/AllItems.aspx" target="_blank">WSEMail Developers Guide</a></h3></div>
        
    <div><asp:ValidationSummary ID="vldSummary" runat="server" ForeColor="Red" /></div>

     <br />

    <div>
        <fieldset>
            <legend>
                <asp:Label ID="lblFieldSetLegend" runat="server" Text="Enter Email Information"></asp:Label>
            </legend>
            <br />
            
            <asp:CustomValidator ID="vldCustomValidator" runat="server" Text="*" ErrorMessage="An xx must be provided." Display="Dynamic" ForeColor="Red" EnableClientScript="False"></asp:CustomValidator>

            <div id="To">
                 <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtTo"><asp:Label ID="lblTo" runat="server" CssClass="inputLabel">To:</asp:Label>
                            
                        </label>
                    </div>
                    <div class="controlContainer">
                        <asp:TextBox ID="txtTo" TabIndex="1" ClientIDMode="Static" 
                            runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="300">@hrsdc-rhdcc.gc.ca</asp:TextBox>
                    </div>
                </div>
            </div>

            <div id="CC">
                 <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtCC"><asp:Label ID="lblCC" runat="server" CssClass="inputLabel">CC:</asp:Label>
                            
                        </label>
                    </div>
                    <div class="controlContainer">
                        <asp:TextBox ID="txtCC" TabIndex="1" ClientIDMode="Static" 
                            runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="300">@hrsdc-rhdcc.gc.ca</asp:TextBox>
                    </div>
                </div>
            </div>
            
        </fieldset>
    </div>

    <div class="buttonRow">
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Send Email" />                
    </div>

     <div class="inputRow">
            <div id="resultContainer" runat="server"></div>
            <br /><div id="requestXml" runat="server"></div>
            <br /><div id="responseXml" runat="server"></div>
            <br /><div id="exceptionContainer" runat="server"></div>
        </div>
    
    </form>
</body>
</html>
