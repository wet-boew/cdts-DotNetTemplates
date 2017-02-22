<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WSEDSampleCode.aspx.vb" Inherits="SampleCode.VB.WSEDSampleCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WSED Sample Code</title>
</head>

<!-- The following javascript is used:
        -to display the proper input field based on the selected action 
        -manage the collapsable request XML and response XML sections
 -->
<script type="text/javascript">
    function toggleInputFieldsOnActionChanged() {

        if (document.getElementById("lstAction").value === "GetGUIDAttributes") {
            document.getElementById("UserName").style.display = "none";
            document.getElementById("ObjectGuid").style.display = "inline";
            document.getElementById("UsersAttributes").style.display = "none";
            document.getElementById("Password").style.display = "none";
        }
        else if (document.getElementById("lstAction").value === "GetUsersAttributes") {
            document.getElementById("UserName").style.display = "none";
            document.getElementById("ObjectGuid").style.display = "none";
            document.getElementById("UsersAttributes").style.display = "inline";
            document.getElementById("Password").style.display = "none";
        }
        else if (document.getElementById("lstAction").value === "IsValidCredential") {
            document.getElementById("UserName").style.display = "inline";
            document.getElementById("Password").style.display = "inline";
            document.getElementById("ObjectGuid").style.display = "none";
            document.getElementById("UsersAttributes").style.display = "none";
        }
        else {
            document.getElementById("UserName").style.display = "inline";
            document.getElementById("ObjectGuid").style.display = "none";
            document.getElementById("UsersAttributes").style.display = "none";
            document.getElementById("Password").style.display = "none";
        }
    }

    function onFormLoad() {
        toggleInputFieldsOnActionChanged();
    }

    function onActionSelectionChanged() {
        toggleInputFieldsOnActionChanged();
    }

    function toggle(toggletype) {
        var strTriangle = "";
        var strContainer = "";
        if (toggletype === "request") {
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

<body onload="onFormLoad()">
    <form id="form1" runat="server">
        <div>
            <h1>WSED Sample Code</h1>
        </div>
        <div>
            <h3><a href="http://architecture/SF-ML/softwarefactory/WSED/Forms/AllItems.aspx" target="_blank">WSED Developers Guide</a></h3>
        </div>
        <div>
            <h3><a href="http://service-wiki.prv/mediawiki/index.php/DCS_Directory_Services:Enterprise_Directory_Web_Service:EDWS_Attributes" target="_blank">Active Directory User Attributes</a></h3>
        </div>

        <div>
            <asp:ValidationSummary ID="vldSummary" runat="server" ForeColor="Red" />
        </div>

        <div class="inputRow">
            <div class="labelContainer">
                <label for="lstAction">
                    <asp:Label ID="Label2" runat="server" CssClass="inputLabel"><strong>WSED Action:</strong></asp:Label></label>
                <asp:DropDownList ID="lstAction" runat="server" onChange="onActionSelectionChanged">
                    
                    <asp:ListItem Value="IsValidCredential" Text="IsValidCredential" />
                    <asp:ListItem Value="IsUserInGroups" Text="IsUserInGroups" />                    
                    <asp:ListItem Value="GetGUIDAttributes" Text="GetGUIDAttributes" />
                    <asp:ListItem Value="GetUserAttributes" Text="GetUserAttributes" />
                    <asp:ListItem Value="GetUsersAttributes" Text="GetUsersAttributes" />
                    <asp:ListItem Value="GetUserAttributesAndGroups" Text="GetUserAttributesAndGroups" />                    

                </asp:DropDownList>
            </div>
        </div>
        <br />

        <div>
            <fieldset>
                <legend>
                    <asp:Label ID="lblFieldSetLegend" runat="server" Text="Enter User Information"></asp:Label>
                </legend>
                <br />

                <asp:CustomValidator ID="vldCustomValidator" runat="server" Text="*" ErrorMessage="An xx must be provided." Display="Dynamic" ForeColor="Red" EnableClientScript="False"></asp:CustomValidator>

                <div id="UserName" style="display: none;">
                    <div class="inputRow">
                        <div class="labelContainer">
                            <label for="txtUserName">
                                <asp:Label ID="lblUserName" runat="server" CssClass="inputLabel">UserName:</asp:Label>

                            </label>
                        </div>
                        <div class="controlContainer">
                            <asp:TextBox ID="txtUserName" TabIndex="1" ClientIDMode="Static"
                                runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="200"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div id="ObjectGuid" style="display: none;">
                    <div class="inputRow">
                        <div class="labelContainer">
                            <label for="txtObjectGuid">
                                <asp:Label ID="lblObjectGuid" runat="server" CssClass="inputLabel">ObjectGuid:</asp:Label>

                            </label>
                        </div>
                        <div class="controlContainer">
                            <asp:TextBox ID="txtObjectGuid" TabIndex="1" ClientIDMode="Static"
                                runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="300"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div id="UsersAttributes" style="display: none;">
                    <div class="inputRow">
                        <div class="labelContainer">
                            <label for="txtLastName">
                                <asp:Label ID="lblLastName" runat="server" CssClass="inputLabel">Last Name:</asp:Label>

                            </label>
                        </div>
                        <div class="controlContainer">
                            <asp:TextBox ID="txtLastName" TabIndex="1" ClientIDMode="Static"
                                runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="200"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div id="Password" style="display: none;">                   
                    <div class="inputRow">
                        <div class="labelContainer">
                            <label for="txtPassword">
                                <asp:Label ID="lblPassword" runat="server" CssClass="inputLabel">Password:</asp:Label>
                            </label>
                        </div>
                        <div class="controlContainer">
                            <asp:TextBox ID="txtPassword" TextMode="Password" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100" Width="200" ></asp:TextBox>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>

        <div class="buttonRow">
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
        </div>

        <div class="inputRow">
            <div id="resultContainer" runat="server"></div>
            <br />
            <div id="requestXml" runat="server"></div>
            <br />
            <div id="responseXml" runat="server"></div>
            <br />
            <div id="exceptionContainer" runat="server"></div>
        </div>

    </form>
</body>
</html>
