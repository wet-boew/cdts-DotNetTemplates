<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WSAddressSampleFlow.aspx.vb" Inherits="SampleCode.VB.WSAddressSampleFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <script type="text/javascript">
        function onFrmLoad() {
            if (document.getElementById("containerSelected").value == "postalCodeContainer") {
                document.getElementById("postalCodeContainer").style.display = "inline";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "searchResultContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "inline";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "urbanAddressContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "inline";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "urbanRouteContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "inline";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "ruralGDContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "inline";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "ruralLockBoxContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "inline";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "ruralRouteContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "inline";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "correctedAddressContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "inline";
                document.getElementById("validAddressContainer").style.display = "none";
            }
            else if (document.getElementById("containerSelected").value == "validAddressContainer") {
                document.getElementById("postalCodeContainer").style.display = "none";
                document.getElementById("searchResultContainer").style.display = "none";
                document.getElementById("urbanAddressContainer").style.display = "none";
                document.getElementById("urbanRouteContainer").style.display = "none";
                document.getElementById("ruralGDContainer").style.display = "none";
                document.getElementById("ruralLockBoxContainer").style.display = "none";
                document.getElementById("ruralRouteContainer").style.display = "none";
                document.getElementById("correctedAddressContainer").style.display = "none";
                document.getElementById("validAddressContainer").style.display = "inline";
            }
        }
    </script>
</head>
<body onload="onFrmLoad()">
    <form id="form1" runat="server">
        <asp:HiddenField ID="containerSelected" Value="postalCodeContainer" runat="server" />

        <div>
            <div style="width: 900px;">
                <h1>Disclaimer:</h1>
                <h2>This is a sample one-page application demonstrating how an application could search and collect address informations.</h2>
                <a href="WSAddressSampleCode.aspx">WSAddress Sample Code Page</a><br /><br />
            </div>
            <asp:ValidationSummary ID="ValidationSummary" ValidationGroup="ValList" runat="server" ForeColor="Red" />

            <div id="mainContainer" style="width: 900px;">
                <div id="languageContainer">
                    <div class="inputRow">
                        <div class="labelContainer">
                            <label for="cmdLanguage">Language:</label>
                            <asp:DropDownList ID="cmdLanguage" runat="server">
                                <asp:ListItem value="English" selected="True">English</asp:ListItem>
                                <asp:ListItem value="French">French</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div><br />

                <div id="postalCodeContainer" style="display: inline;">
                    <fieldset>
                        <legend>Search Address by Postal Code</legend>
                        <br />
                        <label for="txtPostalCode"><asp:Label ID="lblPostalCode" runat="server" CssClass="inputLabel" Width="100px">Postal Code:</asp:Label></label>
                        <asp:TextBox ID="txtPostalCode" TabIndex="4" ClientIDMode="Static" runat="server" MaxLength="100"></asp:TextBox>
                        <br /><br />
                    </fieldset>                    
                </div>

                <div id="searchResultContainer" style="overflow: scroll; display: none;">
                    <fieldset>
                        <legend>Search Results</legend>
                        <div id="addressGrid" runat="server"></div>
                        <br />
                    </fieldset>                    
                </div>

                <div id="urbanAddressContainer" style="display: none;">
                    <fieldset>
                        <legend>Fill out the remaining address information</legend>
                        <br />
                        <div id="streetNumber" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUAStreetNumber"><asp:Label ID="Label5" runat="server" CssClass="inputLabel" Width="100px">Street Number:</asp:Label></label>
                                    <asp:TextBox ID="txtUAStreetNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="lblUAStreetNumberHint" runat="server" CssClass="inputLabel" Width="300px"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div id="suffix" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="cmbUASuffix"><asp:Label ID="Label27" runat="server" CssClass="inputLabel" Width="100px">Suffix:</asp:Label></label>
                                    <asp:DropDownList ID="cmbUASuffix" runat="server">
                                        <asp:ListItem value="0" selected="True">---------</asp:ListItem>
                                        <asp:ListItem value="1">1/4</asp:ListItem>
                                        <asp:ListItem value="2">1/2</asp:ListItem>
                                        <asp:ListItem value="3">3/4</asp:ListItem>
                                        <asp:ListItem value="A">A</asp:ListItem>
                                        <asp:ListItem value="B">B</asp:ListItem>
                                        <asp:ListItem value="C">C</asp:ListItem>
                                        <asp:ListItem value="D">D</asp:ListItem>
                                        <asp:ListItem value="E">E</asp:ListItem>
                                        <asp:ListItem value="F">F</asp:ListItem>
                                        <asp:ListItem value="G">G</asp:ListItem>
                                        <asp:ListItem value="H">H</asp:ListItem>
                                        <asp:ListItem value="I">I</asp:ListItem>
                                        <asp:ListItem value="J">J</asp:ListItem>
                                        <asp:ListItem value="K">K</asp:ListItem>
                                        <asp:ListItem value="L">L</asp:ListItem>
                                        <asp:ListItem value="M">M</asp:ListItem>
                                        <asp:ListItem value="N">N</asp:ListItem>
                                        <asp:ListItem value="O">O</asp:ListItem>
                                        <asp:ListItem value="P">P</asp:ListItem>
                                        <asp:ListItem value="Q">Q</asp:ListItem>
                                        <asp:ListItem value="R">R</asp:ListItem>
                                        <asp:ListItem value="S">S</asp:ListItem>
                                        <asp:ListItem value="T">T</asp:ListItem>
                                        <asp:ListItem value="U">U</asp:ListItem>
                                        <asp:ListItem value="V">V</asp:ListItem>
                                        <asp:ListItem value="W">W</asp:ListItem>
                                        <asp:ListItem value="X">X</asp:ListItem>
                                        <asp:ListItem value="Y">Y</asp:ListItem>
                                        <asp:ListItem value="Z">Z</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="unitType" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="cmbUAUnitType"><asp:Label ID="Label36" runat="server" CssClass="inputLabel" Width="100px">Unit Type:</asp:Label></label>
                                    <asp:DropDownList ID="cmbUAUnitType" runat="server">
                                        <asp:ListItem value="0" selected="True">---------</asp:ListItem>
                                        <asp:ListItem value="APP">Appartement</asp:ListItem>
                                        <asp:ListItem value="APT">Apartment</asp:ListItem>
                                        <asp:ListItem value="BUREAU">Bureau</asp:ListItem>
                                        <asp:ListItem value="PH">Penthouse</asp:ListItem>
                                        <asp:ListItem value="PIECE">Piece</asp:ListItem>
                                        <asp:ListItem value="RM">Room</asp:ListItem>
                                        <asp:ListItem value="SALLE">Salle</asp:ListItem>
                                        <asp:ListItem value="SUITE">Suite</asp:ListItem>
                                        <asp:ListItem value="TH">Townhouse EEN</asp:ListItem>
                                        <asp:ListItem value="TWNHSE">Townhouse FEN</asp:ListItem>
                                        <asp:ListItem value="UNIT">Unit</asp:ListItem>
                                        <asp:ListItem value="UNITE">Unite</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="unitNumber" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUAUnitNumber"><asp:Label ID="Label6" runat="server" CssClass="inputLabel" Width="100px">Unit Number:</asp:Label></label>
                                    <asp:TextBox ID="txtUAUnitNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div id="streetName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUAStreetName"><asp:Label ID="Label3" runat="server" CssClass="inputLabel" Width="100px">Street Name:</asp:Label></label>
                                    <asp:TextBox ID="txtUAStreetName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="city" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUACity"><asp:Label ID="lblAddressLine" runat="server" CssClass="inputLabel" Width="100px">City:</asp:Label></label>
                                    <asp:TextBox ID="txtUACity" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="province" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUAProvince"><asp:Label ID="Label1" runat="server" CssClass="inputLabel" Width="100px">Province:</asp:Label></label>
                                    <asp:TextBox ID="txtUAProvince" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="postalCode" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUAPostalCode"><asp:Label ID="Label2" runat="server" CssClass="inputLabel" Width="100px">Postal Code:</asp:Label></label>
                                    <asp:TextBox ID="txtUAPostalCode" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="country" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtUACountry"><asp:Label ID="Label4" runat="server" CssClass="inputLabel" Width="100px">Country:</asp:Label></label>
                                    <asp:TextBox ID="txtUACountry" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        <br />
                    </fieldset>                    
                </div>

                <div id="urbanRouteContainer" style="overflow: scroll; display: none;">
                    <fieldset>
                        <legend>Fill out the remaining address information</legend>
                        <br />
                        <div id="urStreetNumber" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURStreetNumber"><asp:Label ID="Label8" runat="server" CssClass="inputLabel" Width="150px">Street Number:</asp:Label></label>
                                    <asp:TextBox ID="txtURStreetNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="lblURStreetNumberHint" runat="server" CssClass="inputLabel" Width="300px"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div id="urSuffix" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="cmbURSuffix"><asp:Label ID="Label32" runat="server" CssClass="inputLabel" Width="150px">Suffix:</asp:Label></label>
                                    <asp:DropDownList ID="cmbURSuffix" runat="server">
                                        <asp:ListItem value="0" selected="True">---------</asp:ListItem>
                                        <asp:ListItem value="1">1/4</asp:ListItem>
                                        <asp:ListItem value="2">1/2</asp:ListItem>
                                        <asp:ListItem value="3">3/4</asp:ListItem>
                                        <asp:ListItem value="A">A</asp:ListItem>
                                        <asp:ListItem value="B">B</asp:ListItem>
                                        <asp:ListItem value="C">C</asp:ListItem>
                                        <asp:ListItem value="D">D</asp:ListItem>
                                        <asp:ListItem value="E">E</asp:ListItem>
                                        <asp:ListItem value="F">F</asp:ListItem>
                                        <asp:ListItem value="G">G</asp:ListItem>
                                        <asp:ListItem value="H">H</asp:ListItem>
                                        <asp:ListItem value="I">I</asp:ListItem>
                                        <asp:ListItem value="J">J</asp:ListItem>
                                        <asp:ListItem value="K">K</asp:ListItem>
                                        <asp:ListItem value="L">L</asp:ListItem>
                                        <asp:ListItem value="M">M</asp:ListItem>
                                        <asp:ListItem value="N">N</asp:ListItem>
                                        <asp:ListItem value="O">O</asp:ListItem>
                                        <asp:ListItem value="P">P</asp:ListItem>
                                        <asp:ListItem value="Q">Q</asp:ListItem>
                                        <asp:ListItem value="R">R</asp:ListItem>
                                        <asp:ListItem value="S">S</asp:ListItem>
                                        <asp:ListItem value="T">T</asp:ListItem>
                                        <asp:ListItem value="U">U</asp:ListItem>
                                        <asp:ListItem value="V">V</asp:ListItem>
                                        <asp:ListItem value="W">W</asp:ListItem>
                                        <asp:ListItem value="X">X</asp:ListItem>
                                        <asp:ListItem value="Y">Y</asp:ListItem>
                                        <asp:ListItem value="Z">Z</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="urUnitType" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="cmbURUnitType"><asp:Label ID="Label37" runat="server" CssClass="inputLabel" Width="150px">Unit Type:</asp:Label></label>
                                    <asp:DropDownList ID="cmbURUnitType" runat="server">
                                        <asp:ListItem value="0" selected="True">---------</asp:ListItem>
                                        <asp:ListItem value="APP">Appartement</asp:ListItem>
                                        <asp:ListItem value="APT">Apartment</asp:ListItem>
                                        <asp:ListItem value="BUREAU">Bureau</asp:ListItem>
                                        <asp:ListItem value="PH">Penthouse</asp:ListItem>
                                        <asp:ListItem value="PIECE">Piece</asp:ListItem>
                                        <asp:ListItem value="RM">Room</asp:ListItem>
                                        <asp:ListItem value="SALLE">Salle</asp:ListItem>
                                        <asp:ListItem value="SUITE">Suite</asp:ListItem>
                                        <asp:ListItem value="TH">Townhouse EEN</asp:ListItem>
                                        <asp:ListItem value="TWNHSE">Townhouse FEN</asp:ListItem>
                                        <asp:ListItem value="UNIT">Unit</asp:ListItem>
                                        <asp:ListItem value="UNITE">Unite</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="urUnitNumber" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURUnitNumber"><asp:Label ID="Label9" runat="server" CssClass="inputLabel" Width="150px">Unit Number:</asp:Label></label>
                                    <asp:TextBox ID="txtURUnitNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div id="urStreetName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURStreetName"><asp:Label ID="Label14" runat="server" CssClass="inputLabel" Width="150px">Street Name:</asp:Label></label>
                                    <asp:TextBox ID="txtURStreetName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urRouteServiceType" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURRouteServiceType"><asp:Label ID="Label19" runat="server" CssClass="inputLabel" Width="150px">Route Service Type:</asp:Label></label>
                                    <asp:TextBox ID="txtURRouteServiceType" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urRouteServiceNum" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURRouteServiceNum"><asp:Label ID="Label20" runat="server" CssClass="inputLabel" Width="150px">Route Service #:</asp:Label></label>
                                    <asp:TextBox ID="txtURRouteServiceNum" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urCity" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURCity"><asp:Label ID="Label15" runat="server" CssClass="inputLabel" Width="150px">City:</asp:Label></label>
                                    <asp:TextBox ID="txtURCity" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urProvince" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURProvince"><asp:Label ID="Label16" runat="server" CssClass="inputLabel" Width="150px">Province:</asp:Label></label>
                                    <asp:TextBox ID="txtURProvince" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urPostalCode" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURPostalCode"><asp:Label ID="Label17" runat="server" CssClass="inputLabel" Width="150px">Postal Code:</asp:Label></label>
                                    <asp:TextBox ID="txtURPostalCode" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="urCountry" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtURCountry"><asp:Label ID="Label18" runat="server" CssClass="inputLabel" Width="150px">Country:</asp:Label></label>
                                    <asp:TextBox ID="txtURCountry" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        <br />
                    </fieldset>                
                </div>
            
                <div id="ruralGDContainer" style="display: none;">
                    <fieldset>
                        <legend>Rural General Delivery address information (Read-Only)</legend>
                        <br />
                        <div id="rgdAreaName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDAreaName"><asp:Label ID="Label7" runat="server" CssClass="inputLabel" Width="150px">DI Area Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDAreaName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdDescription" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDDescription"><asp:Label ID="Label21" runat="server" CssClass="inputLabel" Width="150px">DI Description:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDDescription" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdQualifierName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDQualifierName"><asp:Label ID="Label22" runat="server" CssClass="inputLabel" Width="150px">DI Qualifier Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDQualifierName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdCity" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDCity"><asp:Label ID="Label10" runat="server" CssClass="inputLabel" Width="150px">City:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDCity" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdProvince" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDProvince"><asp:Label ID="Label11" runat="server" CssClass="inputLabel" Width="150px">Province:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDProvince" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdPostalCode" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDPostalCode"><asp:Label ID="Label12" runat="server" CssClass="inputLabel" Width="150px">Postal Code:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDPostalCode" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rgdCountry" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRGDCountry"><asp:Label ID="Label13" runat="server" CssClass="inputLabel" Width="150px">Country:</asp:Label></label>
                                    <asp:TextBox ID="txtRGDCountry" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        <br />
                    </fieldset>                    
                </div>

                <div id="ruralLockBoxContainer" style="display: none;">
                    <fieldset>
                        <legend>Fill out the remaining address information</legend>
                        <br />
                         <div id="ruralLBBoxNumber" style="display:inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBBoxNumber"><asp:Label ID="Label26" runat="server" CssClass="inputLabel" Width="150px">Lock Box Number:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBBoxNumber" TabIndex="1" ClientIDMode="Static" runat="server" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="lblRLBBoxNumberHint" runat="server" CssClass="inputLabel" Width="300px"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div id="ruralLBAreaName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBAreaName"><asp:Label ID="Label23" runat="server" CssClass="inputLabel" Width="150px">DI Area Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBAreaName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBDescription" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBDescription"><asp:Label ID="Label24" runat="server" CssClass="inputLabel" Width="150px">DI Description:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBDescription" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBQualifierName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBQualifierName"><asp:Label ID="Label25" runat="server" CssClass="inputLabel" Width="150px">DI Qualifier Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBQualifierName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBCity" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBCity"><asp:Label ID="Label28" runat="server" CssClass="inputLabel" Width="150px">City:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBCity" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBProvince" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBProvince"><asp:Label ID="Label29" runat="server" CssClass="inputLabel" Width="150px">Province:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBProvince" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBPostalCode" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBPostalCode"><asp:Label ID="Label30" runat="server" CssClass="inputLabel" Width="150px">Postal Code:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBPostalCode" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="ruralLBCountry" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRLBCountry"><asp:Label ID="Label31" runat="server" CssClass="inputLabel" Width="150px">Country:</asp:Label></label>
                                    <asp:TextBox ID="txtRLBCountry" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        <br />
                    </fieldset>                    
                </div>
            
                <div id="ruralRouteContainer" style="overflow: scroll; display: none;">
                    <fieldset>
                        <legend>Rural Route address information (Read-Only)</legend>
                        <br />
                        <div id="rrAreaName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRAreaName"><asp:Label ID="Label33" runat="server" CssClass="inputLabel" Width="150px">DI Area Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRRAreaName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        
                        <div id="rrDescription" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRDescription"><asp:Label ID="Label34" runat="server" CssClass="inputLabel" Width="150px">DI Description:</asp:Label></label>
                                    <asp:TextBox ID="txtRRDescription" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        
                        <div id="rrQualifierName" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRQualifierName"><asp:Label ID="Label35" runat="server" CssClass="inputLabel" Width="150px">DI Qualifier Name:</asp:Label></label>
                                    <asp:TextBox ID="txtRRQualifierName" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrRouteServiceType" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRRouteServiceType"><asp:Label ID="Label39" runat="server" CssClass="inputLabel" Width="150px">Route Service Type:</asp:Label></label>
                                    <asp:TextBox ID="txtRRRouteServiceType" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrRouteServiceNum" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRRouteServiceNum"><asp:Label ID="Label40" runat="server" CssClass="inputLabel" Width="150px">Route Service #:</asp:Label></label>
                                    <asp:TextBox ID="txtRRRouteServiceNum" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrCity" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRCity"><asp:Label ID="Label41" runat="server" CssClass="inputLabel" Width="150px">City:</asp:Label></label>
                                    <asp:TextBox ID="txtRRCity" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrProvince" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRProvince"><asp:Label ID="Label42" runat="server" CssClass="inputLabel" Width="150px">Province:</asp:Label></label>
                                    <asp:TextBox ID="txtRRProvince" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrPostalCode" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRPostalCode"><asp:Label ID="Label43" runat="server" CssClass="inputLabel" Width="150px">Postal Code:</asp:Label></label>
                                    <asp:TextBox ID="txtRRPostalCode" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 

                        <div id="rrCountry" style="display: inline;">
                            <div class="inputRow">
                                <div class="labelContainer">
                                    <label for="txtRRCountry"><asp:Label ID="Label44" runat="server" CssClass="inputLabel" Width="150px">Country:</asp:Label></label>
                                    <asp:TextBox ID="txtRRCountry" TabIndex="1" ClientIDMode="Static" runat="server" Enabled="false" CssClass="inputTextBoxLong" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div> 
                        <br />
                    </fieldset>                
                </div>

                <div id="correctedAddressContainer" style="overflow: scroll; display: none;">       
                    <div id="correctedAddressInformation" runat="server"></div>
                </div>

                <div id="validAddressContainer" style="overflow: scroll; display: none;">
                    <div id="validAddressMessage" runat="server"></div>
                </div>
            </div>    
            
            <div id="buttonContainer" style="width: 900px; padding-top: 25px;">
                <div style="width: auto; float: left; height: 26px;"><asp:Button ID="btnPrev" runat="server" TabIndex="5" Text="Previous" Enabled="false" /></div>
                <div style="width: auto; float: right;"><asp:Button ID="btnNext" runat="server" TabIndex="5" Text="Next" /></div>
            </div>        
        </div>
    </form>
</body>
</html>
