<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WSAddressSampleCode.aspx.vb" Inherits="SampleCode.VB.WSAddressSampleCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9"/>
    <title>WSAddress Sample Code</title>
</head>
<script type="text/javascript">
    // TODO: Add comments here
    function toggleInputFieldsOnActionChanged() {
        document.getElementById("parseType").style.display = "none";
        document.getElementById("geographicScope").style.display = "none";
        document.getElementById("addressLine").style.display = "inline";
        document.getElementById("city").style.display = "inline";
        document.getElementById("province").style.display = "inline";
        document.getElementById("country").style.display = "inline";
        document.getElementById("streetNumber").style.display = "none";
        document.getElementById("unitNumber").style.display = "none";
        document.getElementById("formatResult").style.display = "none";
        document.getElementById("returnAddressLine").style.display = "none";

        if (document.getElementById("lstAction").value == "Correct") {
            document.getElementById("formatResult").style.display = "inline";
        }
        else if (document.getElementById("lstAction").value == "Parse") {
            document.getElementById("parseType").style.display = "inline";
            document.getElementById("geographicScope").style.display = "inline";
        }
        else if (document.getElementById("lstAction").value == "Search") {
            document.getElementById("returnAddressLine").style.display = "inline";
        }
        else if (document.getElementById("lstAction").value == "UniqueSearch") {
            document.getElementById("addressLine").style.display = "none";
            document.getElementById("city").style.display = "none";
            document.getElementById("province").style.display = "none";
            document.getElementById("country").style.display = "none";
            document.getElementById("streetNumber").style.display = "inline";
            document.getElementById("unitNumber").style.display = "inline";
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
<body onload="onFormLoad()">
    <form id="form1" runat="server">
    <div>
        <h1>WSAddress Sample Code</h1>

        <a href="http://architecture/SF-ML/softwarefactory/WSAddress/Forms/AllItems.aspx?RootFolder=%2FSF%2DML%2Fsoftwarefactory%2FWSAddress%2FDocumentation%20and%20Developer%27s%20Guide&FolderCTID=0x012000F210153B1AE1C340868D9A8D0D9E9078&View={E0BF68F2-5B5B-4644-A454-547589F50D20}" target="_blank">WSAddress Developer's Guide</a><br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ForeColor="Red" />
        <br />
    </div>

    <div>
        <div class="inputRow"><div class="labelContainer">
            <label for="lstAction"><asp:Label ID="Label2" runat="server" CssClass="inputLabel"><strong>WSAddress Action:</strong></asp:Label></label>
            <asp:DropDownList ID="lstAction" runat="server">
                <asp:ListItem Value="Correct" Text="Correct"/>
                <asp:ListItem Value="Validate" Text="Validate"/>
                <asp:ListItem Value="Format" Text="Format"/>
                <asp:ListItem Value="Parse" Text="Parse"/>
                <asp:ListItem Value="Search" Text="Search"/>
                <asp:ListItem Value="UniqueSearch" Text="Unique Search"/>
            </asp:DropDownList>
        </div></div><br />

        <fieldset style="width:600px;">
            <legend>
                <asp:Label ID="lblFieldSetLegend" runat="server" Text="Enter Address Information"></asp:Label>
            </legend>

            <div id="parseType" style="display:none;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="lstParseType"><asp:Label ID="Label3" runat="server" CssClass="inputLabel" Width="150px">Parse Type:</asp:Label></label>
                        <asp:DropDownList ID="lstParseType" runat="server" TabIndex="5">
                            <asp:ListItem Value="CorrectAndParse" Text="Correct And Parse"/>
                            <asp:ListItem Value="ValidateAndParse" Text="Parse And Validate"/>
                            <asp:ListItem Value="ParseOnly" Text="ParseOnly"/>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            
            <div id="geographicScope" style="display:none;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="lstGeoScope"><asp:Label ID="Label4" runat="server" CssClass="inputLabel" Width="150px">Geo. Scope:</asp:Label></label>
                        <asp:DropDownList ID="lstGeoScope" runat="server" TabIndex="5">
                            <asp:ListItem Value="Canada" Text="Canada"/>
                            <asp:ListItem Value="Foreign" Text="Foreign"/>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>            

            <div id="streetNumber" style="display:none;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtStreetNumber"><asp:Label ID="Label5" runat="server" CssClass="inputLabel" Width="150px">Street Number:</asp:Label></label>
                        <asp:TextBox ID="txtStreetNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="streetNumberValidator" EnableClientScript="false" ControlToValidate="txtStreetNumber" Display="None" runat="server" ErrorMessage="Street number is required for unique search"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div id="unitNumber" style="display:none;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtUnitNumber"><asp:Label ID="Label6" runat="server" CssClass="inputLabel" Width="150px">Unit Number:</asp:Label></label>
                        <asp:TextBox ID="txtUnitNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div id="addressLine" style="display: inline;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtAddressLine"><asp:Label ID="lblAddressLine" runat="server" CssClass="inputLabel" Width="150px">Address Line:</asp:Label></label>
                        <asp:TextBox ID="txtAddressLine" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="addressLineValidator" EnableClientScript="false" ControlToValidate="txtAddressLine" Display="None" runat="server" ErrorMessage="Address line is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>           

            <div id="city" style="display: inline;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="txtCity"><asp:Label ID="lblCity" runat="server" CssClass="inputLabel" Width="150px">City:</asp:Label></label>
                        <asp:TextBox ID="txtCity" TabIndex="2" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="cityValidator" EnableClientScript="false" ControlToValidate="txtCity" Display="None" runat="server" ErrorMessage="City is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div id="province" style="display:inline;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="lstProvince"><asp:Label ID="lblProvince" runat="server" CssClass="inputLabel" Width="150px">Province:</asp:Label></label>
                        <asp:DropDownList ID="lstProvince" runat="server" TabIndex="3">
                            <asp:ListItem Value="AB" Text="Alberta"/>
                            <asp:ListItem Value="BC" Text="British Columbia"/>
                            <asp:ListItem Value="MB" Text="Manitoba"/>
                            <asp:ListItem Value="NB" Text="New Brunswick"/>
                            <asp:ListItem Value="NL" Text="Newfoundland and Labrador"/>
                            <asp:ListItem Value="NT" Text="Northwest Territories"/>
                            <asp:ListItem Value="NS" Text="Nova Scotia"/>
                            <asp:ListItem Value="NU" Text="Nunavut"/>
                            <asp:ListItem Value="ON" Text="Ontario"/>
                            <asp:ListItem Value="PE" Text="Prince Edward Island"/>
                            <asp:ListItem Value="QC" Text="Quebec"/>
                            <asp:ListItem Value="SK" Text="Saskatchewan"/>
                            <asp:ListItem Value="YT" Text="Yukon"/>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="inputRow">
                <div class="labelContainer">
                    <label for="txtPostalCode"><asp:Label ID="lblPostalCode" runat="server" CssClass="inputLabel" Width="150px">PostalCode:</asp:Label></label>
                    <asp:TextBox ID="txtPostalCode" TabIndex="4" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="postalCodeValidator" EnableClientScript="false" ControlToValidate="txtPostalCode" Display="None" runat="server" ErrorMessage="Postal code is required"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div id="country" style="display: inline;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="lstCountry"><asp:Label ID="Label1" runat="server" CssClass="inputLabel" Width="150px">Country:</asp:Label></label>
                        <asp:DropDownList ID="lstCountry" runat="server" TabIndex="5">
                            <asp:ListItem Value="CA" Text="Canada"/>
                            <asp:ListItem Value="US" Text="United States"/>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div id="formatResult" style="display: inline;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="chkFormatResult"><asp:Label ID="Label7" runat="server" CssClass="inputLabel" Width="150px">Format Result:</asp:Label></label>
                        <asp:CheckBox id="chkFormatResult" runat="server" />                        
                    </div>
                </div>
            </div>      

            <div id="returnAddressLine" style="display: none;">
                <div class="inputRow">
                    <div class="labelContainer">
                        <label for="chkReturnAddressLine"><asp:Label ID="Label8" runat="server" CssClass="inputLabel" Width="150px">Return AddressLine:</asp:Label></label>
                        <asp:CheckBox id="chkReturnAddressLine" runat="server" />                        
                    </div>
                </div>
            </div>      
        </fieldset>

        <div class="buttonRow">
            <br />
            <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" />
        </div>

        <div class="inputRow">
            <div id="resultContainer" runat="server"></div>
            <br /><div id="requestXml" runat="server"></div>
            <br /><div id="responseXml" runat="server"></div>
        </div>
    </div>
    </form>
</body>
</html>
