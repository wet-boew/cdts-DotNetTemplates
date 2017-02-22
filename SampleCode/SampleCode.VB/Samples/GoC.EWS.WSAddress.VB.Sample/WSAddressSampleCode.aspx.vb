'**********************************************************************
' DISCLAIMER
'
' The following sample code is to be used as a starting point for the
' implementation of this component in your project. 
'
' THIS SAMPLE CODE SHOULD NOT BE CONSIDERED PRODUCTION READY.
'
' Proper error handling must be added.
' A full review of the values used for settings and parameters should 
' be reviewed to suit your applications needs.
'
' *** You are accountable and responsible for the integration and use 
' of this sample code in your project.***
'**********************************************************************
Imports GoC.ServiceCanada.SoftwareFactory.Proxies
Imports System.Net
Imports System.Reflection
Imports System.Globalization

Public Class WSAddressSampleCode
    Inherits System.Web.UI.Page

#Region "ASP .NET Page Events Handlers"

    ''' <summary>
    ''' Override the base Validate method as some Required Validator is not necessary depending on the Action chosen
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Validate()
        ' Set the validator to the default state first
        Me.addressLineValidator.Enabled = True
        Me.cityValidator.Enabled = True
        Me.postalCodeValidator.Enabled = True
        Me.streetNumberValidator.Enabled = False

        If (Page.IsPostBack) Then
            Dim action = Me.lstAction.SelectedItem.Value

            If (action.Equals("Search") OrElse action.Equals("Query")) Then
                ' For Search and FormattedSearch action, disble the AddressLine, City, and Postal Code 
                ' required field validator
                Me.addressLineValidator.Enabled = False
                Me.cityValidator.Enabled = False
                Me.postalCodeValidator.Enabled = False
            ElseIf (action.Equals("UniqueSearch")) Then
                ' For Unique Search action, disable the AddressLine and City required 
                ' field validator, as well as enable the StreetNumber validator
                Me.addressLineValidator.Enabled = False
                Me.cityValidator.Enabled = False
                Me.streetNumberValidator.Enabled = True
            End If
        End If

        ' Call the super Validate method with the validator(s) at the correct state, 
        ' so to trigger the validation messages appropriately depending on the action chosen
        MyBase.Validate()
    End Sub

    ''' <summary>
    ''' Page Load event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Page.IsPostBack) Then
            'Verify if the web.config keys for this sample code have values
            VerifyConfigValues()

            ' On GET, add the onChange attribute to the Action drop down list, so that it will call 
            ' the appropriate javascript function
            Me.lstAction.Attributes.Add("onChange", "onActionSelectionChanged();")
        End If

    End Sub

#End Region

#Region "Submit Button Action Handler"

    ''' <summary>
    ''' Submit button Event Handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Exit immediately if there are
        If (Not Page.IsValid) Then Return

        Dim selectedAction As String = Me.lstAction.SelectedItem.Value

        ' choose the appropriate Action Handler depending on the action chosen
        Select Case selectedAction
            Case "Correct"                      ' Correct Action
                HandleCorrectAction()
            Case "Validate"                     ' Validate Action
                HandleValidateAction()
            Case "Format"                       ' Format Action
                HandleFormatAction()
            Case "Parse"                        ' Parse Action
                HandleParseAction()
            Case "Search"                       ' Search Action
                HandleSearchAction()
            Case "UniqueSearch"                 ' Unique Search Action
                HandleUniqueSearchAction()
        End Select
    End Sub

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Helper function to help Initialize the WSAddressTransaction object
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTransaction() As WSAddress.WSAddressTransaction

        Dim applicationName As String = "WSAddress Sample Code"
        Dim applicationVersion As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
        Dim applicationIpAddress As String = String.Empty
        Dim applicationMachineName As String = Dns.GetHostName()
        Dim applicationRole As String = String.Empty

        Dim ipAddresses() As IPAddress
        ipAddresses = Dns.GetHostAddresses(applicationMachineName)
        If ipAddresses.Length > 0 Then
            applicationIpAddress = ipAddresses(0).ToString()
        End If

        Dim employeeId As String = Me.Session.SessionID
        Dim employeeName As String = GetUserSamAccountName()
        Dim employeeIpAddress As String = Request.UserHostAddress
        Dim employeeMachineName As String = Request.UserHostName
        Dim employeeRole As String = String.Empty

        Dim clientId As String = String.Empty
        Dim clientName As String = String.Empty
        Dim clientIpAddress As String = String.Empty
        Dim clientMachineName As String = String.Empty
        Dim clientRole As String = String.Empty
        Dim assuranceLevel As String = String.Empty

        'If you do not have a service account or your service account does not have access to this service please complete the proper excel on the sharepoint site "http://architecture/SF-ML/softwarefactory/General%20Documents/Forms/AllItems.aspx"
        'Note: The sample code does not expect the password to be encrypted, but in your implementation you should encrypt this value.
        Dim userName As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.ServiceAccount.Name")
        Dim password As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.ServiceAccount.Password")
        Dim domain As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.Domain")
        Dim wSAddressUrl As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.URL")
        Dim timeout As Integer = ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.TimeOut")

        Dim wSAddressTransaction As WSAddress.WSAddressTransaction

        wSAddressTransaction = New WSAddress.WSAddressTransaction(applicationName, _
                                                 applicationVersion, _
                                                 applicationIpAddress, _
                                                 applicationMachineName, _
                                                 applicationRole, _
                                                 employeeId, _
                                                 employeeName, _
                                                 employeeIpAddress, _
                                                 employeeMachineName, _
                                                 employeeRole, _
                                                 clientId, _
                                                 clientName, _
                                                 clientIpAddress, _
                                                 clientMachineName, _
                                                 clientRole, _
                                                 assuranceLevel, _
                                                 userName, _
                                                 password, _
                                                 domain, _
                                                 wSAddressUrl, _
                                                 timeout)

        Return wSAddressTransaction

    End Function

    ''' <summary>
    ''' This method retrieves the samAccountName of the current logged in user accessing your application
    ''' </summary>
    ''' <returns>String: the firstname.lastname of the logged in user. ex: Eddy.Murphy</returns>
    ''' <remarks></remarks>
    Private Shared Function GetUserSamAccountName() As String

        Dim samAccountName() As String = My.User.Name.Split("\"c)

        Try
            If (samAccountName.Length() > 0) Then
                Return samAccountName(samAccountName.Length() - 1).ToLowerInvariant()
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try

    End Function

    ''' <summary>
    ''' Helper function to populate the WSAddressTransaction with input parameter values that 
    ''' are always needed, this is applicable to the following WSAddress Action:
    ''' 
    ''' - Correct
    ''' - Validate
    ''' - Format
    ''' - Parse
    ''' - Search
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="wsaddressTransaction"></param>
    ''' <remarks></remarks>
    Private Sub HandleActionCommon(ByVal action As String, ByVal wsaddressTransaction As WSAddress.WSAddressTransaction)
        With wsaddressTransaction.AllRequests.AddRequests().AddRequest()
            .Name = action

            With .InputParameters
                ' for the purpose of sample code, the Language here is hard-coded to be "English", 
                ' in a real production setting, this value should be derived dynamically depending on 
                ' the user's current language setting
                .AddParameter("Language", "English")

                .AddParameter("AddressLine", txtAddressLine.Text.Trim())
                .AddParameter("City", txtCity.Text.Trim())
                .AddParameter("Province", lstProvince.SelectedItem.Value)
                .AddParameter("PostalCode", txtPostalCode.Text.Trim())
                .AddParameter("Country", lstCountry.SelectedItem.Value)
            End With
        End With
    End Sub

    ''' <summary>
    ''' This method will generate the HTML required for the collapsible Request/Response xml viewer
    ''' </summary>
    ''' <param name="wsaddressTransaction"></param>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleRequestResponseXml(ByVal wsaddressTransaction As WSAddress.WSAddressTransaction, ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        ' The request and response xml will be embedded in the <xmp> tag. NOTE: the <xmp> tag will not be supported by IE starting IE 11, 
        ' the collapse/expand arrow is a HTML entity that when clicked, will invoked the "toggle(...)" javascript declared on the aspx page

        If (wsaddressTransaction IsNot Nothing) Then
            Me.requestXml.InnerHtml = "<h3>Request xml: <a href='#' title='expand' style='text-decoration: none;' id='requestXmlTriangle' onclick=""toggle('request')"">&#9660;</a></h3>" +
                                      "<div id='requestXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsaddressTransaction.GetXML +
                                      "</xmp></div></div>"
        End If

        If (wsaddressResponse IsNot Nothing) Then
            Me.responseXml.InnerHtml = "<h3>Response xml: <a href='#' title='expand' style='text-decoration: none;' id='responseXmlTriangle' onclick=""toggle('response')"">&#9660;</a></h3>" +
                                       "<div id='responseXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsaddressResponse.GetXML +
                                       "</xmp></div></div>"
        End If
    End Sub


    ''' <summary>
    ''' This method verifies that the web.config keys that this sample code relies on have values.  
    ''' If they are empty an error will be displayed to the user.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VerifyConfigValues()

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.ServiceAccount.Name") = String.Empty Then
            Dim cv = New CustomValidator()
            Using cv
                cv.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.ServiceAccount.Name' in the web.config, for this sample code to work."
                cv.IsValid = False
                cv.ValidationGroup = ""
                Page.Validators.Add(cv)
            End Using
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.ServiceAccount.Password") = String.Empty Then
            Dim cv = New CustomValidator()
            Using cv
                cv.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.ServiceAccount.Password' in the web.config, for this sample code to work."
                cv.IsValid = False
                cv.ValidationGroup = ""
                Page.Validators.Add(cv)
            End Using
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.Domain") = String.Empty Then
            Dim cv = New CustomValidator()
            Using cv
                cv.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.Domain' in the web.config, for this sample code to work."
                cv.IsValid = False
                cv.ValidationGroup = ""
                Page.Validators.Add(cv)
            End Using
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.URL") = String.Empty Then
            Dim cv = New CustomValidator()
            Using cv
                cv.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.URL' in the web.config, for this sample code to work."
                cv.IsValid = False
                cv.ValidationGroup = ""
                Page.Validators.Add(cv)
            End Using
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSAddress.TimeOut") = String.Empty Then
            Dim cv = New CustomValidator()
            Using cv
                cv.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.TimeOut' in the web.config, for this sample code to work."
                cv.IsValid = False
                cv.ValidationGroup = ""
                Page.Validators.Add(cv)
            End Using
        End If
    End Sub

#End Region

#Region "Correct Action"

    ''' <summary>
    ''' WSAddress CORRECT action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleCorrectAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' Initialize the WSAddress Transaction object input parameters
            HandleActionCommon("Correct", wsaddressTransaction)

            If (Me.chkFormatResult.Checked) Then
                ' Besides the common set of the input parameters, the CORRECT action
                ' could also requires the FormatResult input parameters to 
                ' be populated for the WSAddressTransaction object depending on the selction 
                ' state of the FormatResult checkbox
                With wsaddressTransaction.AllRequests(0)(0)
                    With .InputParameters
                        .AddParameter("FormatResult", "True")
                    End With
                End With
            End If

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleCorrectResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try
    End Sub

    ''' <summary>
    ''' WSAddress CORRECT action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleCorrectResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim functionalMessages As WSAddress.WSAddressFunctionalMessages

        ' The CORRECT action can return both Output Parameters and Functional Messages
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
            functionalMessages = .FunctionalMessages
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter as a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameters in a html table
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(param.Name)
                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                html.Append(param.Value)
                html.Append("</td></tr>")
            End If
        Next
        html.Append("</table><br />")

        ' Display the functional messages in a separate html table
        html.Append("<table style='border:1px solid black; width:800px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Functional messages:</th></tr>")
        For Each msg As WSAddress.WSAddressFunctionalMessage In functionalMessages
            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
            html.Append(msg.Action)
            html.Append("</td><td style='border:1px solid black; width:550px; border-collapse:collapse;'>")
            html.Append(msg.Value)
            html.Append("</td></tr>")
        Next
        html.Append("</table>")

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

#End Region

#Region "Validate Action"

    ''' <summary>
    ''' WSAddress VALIDATE action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleValidateAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' Initialize the WSAddress Transaction object input parameters
            HandleActionCommon("Validate", wsaddressTransaction)

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleValidateResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try
    End Sub

    ''' <summary>
    ''' WSAddress VALIDATE action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleValidateResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim functionalMessages As WSAddress.WSAddressFunctionalMessages

        ' The VALIDATE action can return both output parameters and function messages
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
            functionalMessages = .FunctionalMessages
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter as a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameters in a html table
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(param.Name)
                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                html.Append(param.Value)
                html.Append("</td></tr>")
            End If
        Next
        html.Append("</table><br />")

        ' Display the functional messages in a separate html table
        html.Append("<table style='border:1px solid black; width:800px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Functional messages:</th></tr>")
        For Each msg As WSAddress.WSAddressFunctionalMessage In functionalMessages
            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
            html.Append(msg.Action)
            html.Append("</td><td style='border:1px solid black; width:550px; border-collapse:collapse;'>")
            html.Append(msg.Value)
            html.Append("</td></tr>")
        Next
        html.Append("</table>")

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

#End Region

#Region "Format Action"

    ''' <summary>
    ''' WSAddress FORMAT action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleFormatAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' Initialize the WSAddress Transaction object input parameters
            HandleActionCommon("Format", wsaddressTransaction)

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleFormatResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try
    End Sub

    ''' <summary>
    ''' WSAddress FORMAT action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleFormatResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim formattedAddressLine As StringBuilder = New StringBuilder()

        ' The FORMAT action can ONLY return output parameters
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter as a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameters in a html table, 
        ' for the FORMAT action, the only other output parameters besides the 
        ' status code are the MESSAGE and FORMATTEDADDRESSLINE (up to a max of 5 
        ' separate entries for FORMATTEDADDRESSLINE)
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                If (param.Name.Equals("Message")) Then
                    ' Display the MESSAGE output paramter as a html table row
                    html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                    html.Append(param.Name)
                    html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                    html.Append(param.Value)
                    html.Append("</td></tr>")
                Else
                    ' Store the FormattedAddressLine output parameter(s) into a string variable, 
                    ' so they can displayed together in a single html table cell
                    formattedAddressLine.Append(param.Value)
                    formattedAddressLine.Append("<br />")
                End If
            End If
        Next

        ' Display the FORMATTEDADDRESSLINE output paramter values when applicable
        If (formattedAddressLine.Length <> 0) Then
            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>Formatted Addr. Line:</td>")
            html.Append("<td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
            html.Append(formattedAddressLine.ToString())
            html.Append("</td></tr>")
        End If
        html.Append("</table>")

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

#End Region

#Region "Parse Action"

    ''' <summary>
    ''' WSAddress PARSE action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleParseAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' Initialize the WSAddress Transaction object input parameters
            HandleActionCommon("Parse", wsaddressTransaction)

            ' Besides the common set of the input parameters, the PARSE action
            ' also requires the ParseType and GeographicScope input parameters to 
            ' be populated for the WSAddressTransaction object
            With wsaddressTransaction.AllRequests(0)(0)
                With .InputParameters
                    .AddParameter("ParseType", lstParseType.SelectedItem.Value)
                    .AddParameter("GeographicScope", lstGeoScope.SelectedItem.Value)
                End With
            End With

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleParseResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try
    End Sub

    ''' <summary>
    ''' WSAddress PARSE action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleParseResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim functionalMessages As WSAddress.WSAddressFunctionalMessages
        Dim cdnPostInformation As WSAddress.WSAddressCanadaPostInformation

        ' The PARSE action can return Output Parameters, Functional Messages and 
        ' if applicable the Canada Post Information values
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
            functionalMessages = .FunctionalMessages
            cdnPostInformation = .CanadaPostInformation
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter as a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameter(s) in a html table
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(param.Name)
                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                html.Append(param.Value)
                html.Append("</td></tr>")
            End If
        Next
        html.Append("</table><br />")

        ' Display the functional messages in the a separate html table
        html.Append("<table style='border:1px solid black; width:800px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Functional messages:</th></tr>")
        For Each msg As WSAddress.WSAddressFunctionalMessage In functionalMessages
            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
            html.Append(msg.Action)
            html.Append("</td><td style='border:1px solid black; width:550px; border-collapse:collapse;'>")
            html.Append(msg.Value)
            html.Append("</td></tr>")
        Next
        html.Append("</table>")

        ' If the Canada Post Information datas are also returned, display them in a separate html table
        If (cdnPostInformation IsNot Nothing AndAlso cdnPostInformation.Count <> 0) Then
            html.Append("<br /><table>")
            For Each objCdnPostInfEntry As WSAddress.WSAddressCanadaPostInformationEntry In cdnPostInformation
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(objCdnPostInfEntry.Type)
                html.Append("</td><td style='border:1px solid black; width:550px; border-collapse:collapse;'>")
                html.Append(objCdnPostInfEntry.Description)
                html.Append("</td></tr>")
            Next
            html.Append("</table>")
        End If

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

#End Region

#Region "Search Action"

    ''' <summary>
    ''' Following are constants for the possible Address Match type that 
    ''' can be sought when doing a WSAddress SEARCH action
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ADDR_MATCH_TYPE_UA As String = "UrbanAddress"
    Private Const ADDR_MATCH_TYPE_UR As String = "UrbanRoute"
    Private Const ADDR_MATCH_TYPE_RGD As String = "RuralGeneralDelivery"
    Private Const ADDR_MATCH_TYPE_RLB As String = "RuralLockBox"
    Private Const ADDR_MATCH_TYPE_RR As String = "RuralRoute"

    ''' <summary>
    ''' WSAddress SEARCH action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleSearchAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' Initialize the WSAddress Transaction object input parameters
            HandleActionCommon("Search", wsaddressTransaction)

            If (Me.chkReturnAddressLine.Checked) Then
                ' Besides the common set of the input parameters, the SEARCH action
                ' could also requires the ReturnAddressLine input parameters to 
                ' be populated for the WSAddressTransaction object depending on the 
                ' selection state of the ReturnAddressLine checkbox
                With wsaddressTransaction.AllRequests(0)(0)
                    With .InputParameters
                        .AddParameter("ReturnAddressLine", "True")
                    End With
                End With
            End If

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleSearchResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try
    End Sub

    ''' <summary>
    ''' WSAddress SEARCH action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleSearchResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim addrMatches As WSAddress.WSAddressMatches

        ' The SEARCH action returns the Output Parameters and the applicable Address Matches
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
            addrMatches = .AddressMatches
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter as a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameters in a html table
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(param.Name)
                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                html.Append(param.Value)
                html.Append("</td></tr>")
            End If
        Next
        html.Append("</table><br />")

        ' Display the # of address match found in a single line
        html.Append("<h3>" + addrMatches.Count.ToString(CultureInfo.InvariantCulture) + " match(es) found:</h3>")

        If (addrMatches.Count <> 0) Then
            Dim UAMatches As StringBuilder = New StringBuilder()
            Dim URMatches As StringBuilder = New StringBuilder()
            Dim RGDMatches As StringBuilder = New StringBuilder()
            Dim RLBMatches As StringBuilder = New StringBuilder()
            Dim RRMatches As StringBuilder = New StringBuilder()

            ' Loop through the list of AddressMatch object and handle each 
            ' appropriately depending on the Address Match Type value
            For Each AddrMatch As WSAddress.WSAddressMatch In addrMatches
                If (AddrMatch.AddressType.Equals(ADDR_MATCH_TYPE_UA)) Then
                    ' Urba Address match handler
                    If (UAMatches.Length = 0) Then
                        UAMatches.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>")
                        UAMatches.Append("<tr bgcolor='#C0C0C0'><th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>City</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Province</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Country</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Address Line</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Name</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Number Maximum</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Number Minimum</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Suite Number Maximum</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Suite Number Minimum</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Type</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Direction</th>")
                        UAMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Address Sequence</th></tr>")
                    End If

                    ' generate html for urban address row
                    UAMatches.Append(CreateUrbanAddressHtml(AddrMatch))
                ElseIf (AddrMatch.AddressType.Equals(ADDR_MATCH_TYPE_UR)) Then
                    ' Urban Route match handler
                    If (URMatches.Length = 0) Then
                        URMatches.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>")
                        URMatches.Append("<tr bgcolor='#C0C0C0'><th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>City</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Province</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Country</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Name</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Number Maximum</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Number Minimum</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Suite Number Maximum</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Suite Number Minimum</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Type</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Direction</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Street Address Sequence</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Route Service Type</th>")
                        URMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Route Service Number</th></tr>")
                    End If

                    ' generate html for urban route address row
                    URMatches.Append(CreateUrbanRouteHtml(AddrMatch))
                ElseIf (AddrMatch.AddressType.Equals(ADDR_MATCH_TYPE_RGD)) Then
                    ' Rural General Delivery match handler
                    If (RGDMatches.Length = 0) Then
                        RGDMatches.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>")
                        RGDMatches.Append("<tr bgcolor='#C0C0C0'><th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>City</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Province</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Country</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Area Name</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Description</th>")
                        RGDMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Qualifier Name</th></tr>")
                    End If

                    ' generate html for rural general delivery address row
                    RGDMatches.Append(CreateRuralGeneralDeliveryHtml(AddrMatch))
                ElseIf (AddrMatch.AddressType.Equals(ADDR_MATCH_TYPE_RLB)) Then
                    ' Rural Lock Box match handler
                    If (RLBMatches.Length = 0) Then
                        RLBMatches.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>")
                        RLBMatches.Append("<tr bgcolor='#C0C0C0'><th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>City</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Province</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Country</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Address Line</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Area Name</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Description</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Qualifier Name</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Lock Box Number Maximum</th>")
                        RLBMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Lock Box Number Minimum</th></tr>")
                    End If

                    ' generate html for rural lockbox address row
                    RLBMatches.Append(CreateRuralLockBoxHtml(AddrMatch))
                ElseIf (AddrMatch.AddressType.Equals(ADDR_MATCH_TYPE_RR)) Then
                    ' Rural Route match handler
                    If (RRMatches.Length = 0) Then
                        RRMatches.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>")
                        RRMatches.Append("<tr bgcolor='#C0C0C0'><th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>City</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Province</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Country</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Area Name</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Description</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Delivery Installation Qualifier Name</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Rural Route Service Type</th>")
                        RRMatches.Append("<th style='border:1px solid black; border-collapse:collapse;'>Rural Route Service Number</th></tr>")
                    End If

                    ' generate html for rural route address row
                    RRMatches.Append(CreateRuralRouteHtml(AddrMatch))
                End If
            Next

            If (UAMatches.Length <> 0) Then
                UAMatches.Append("</table><br />")
                html.Append(UAMatches.ToString())
            End If

            If (URMatches.Length <> 0) Then
                URMatches.Append("</table><br />")
                html.Append(URMatches.ToString())
            End If

            If (RGDMatches.Length <> 0) Then
                RGDMatches.Append("</table><br />")
                html.Append(RGDMatches.ToString())
            End If

            If (RLBMatches.Length <> 0) Then
                RLBMatches.Append("</table><br />")
                html.Append(RLBMatches.ToString())
            End If

            If (RRMatches.Length <> 0) Then
                RRMatches.Append("</table><br />")
                html.Append(RRMatches.ToString())
            End If
        End If

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

    ''' <summary>
    ''' Generate the HTML row for a Urban Address match found
    ''' </summary>
    ''' <param name="addrMatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateUrbanAddressHtml(ByVal addrMatch As WSAddress.WSAddressMatch) As String
        Return "<tr><td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("AddressLine") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetNumberMaximum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetNumberMinimum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("SuiteNumberMaximum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("SuiteNumberMinimum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetType") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetDirection") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetAddressSequence") + "</td></tr>"
    End Function

    ''' <summary>
    ''' Generate the HTML row for a Urban Route match found
    ''' </summary>
    ''' <param name="addrMatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateUrbanRouteHtml(ByVal addrMatch As WSAddress.WSAddressMatch) As String
        Return "<tr><td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetNumberMaximum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetNumberMinimum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("SuiteNumberMaximum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("SuiteNumberMinimum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetType") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetDirection") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetAddressSequence") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("RouteServiceType") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("RouteServiceNumber") + "</td></tr>"
    End Function

    ''' <summary>
    ''' Generate the HTML row for a Rural General Delivery match found
    ''' </summary>
    ''' <param name="addrMatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateRuralGeneralDeliveryHtml(ByVal addrMatch As WSAddress.WSAddressMatch) As String
        Return "<tr><td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationDescription") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationQualifierName") + "</td></tr>"
    End Function

    ''' <summary>
    ''' Generate the HTML row for a Rural Lock Box match found
    ''' </summary>
    ''' <param name="addrMatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateRuralLockBoxHtml(ByVal addrMatch As WSAddress.WSAddressMatch) As String
        Return "<tr><td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("AddressLine") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationDescription") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationQualifierName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("LockBoxNumberMaximum") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("LockBoxNumberMinimum") + "</td></tr>"
    End Function

    ''' <summary>
    ''' Generate the HTML row for a Rural Route match found
    ''' </summary>
    ''' <param name="addrMatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateRuralRouteHtml(ByVal addrMatch As WSAddress.WSAddressMatch) As String
        Return "<tr><td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationAreaName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationDescription") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DeliveryInstallationQualifierName") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("RuralRouteServiceType") + "</td>" +
               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("RuralRouteServiceNumber") + "</td></tr>"
    End Function

#End Region

#Region "Unique Search Action"

    ''' <summary>
    ''' WSAddress UNIQUE SEARCH action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleUniqueSearchAction()
        Dim wsaddressTransaction = GetTransaction()
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            ' The unique search action supplies a different set of input parameters 
            ' when compare to the other WSAddress action
            With wsaddressTransaction.AllRequests.AddRequests().AddRequest()
                .Name = "UniqueSearch"

                With .InputParameters
                    .AddParameter("Language", "English")
                    .AddParameter("StreetNumber", txtStreetNumber.Text.Trim())
                    .AddParameter("UnitNumber", txtUnitNumber.Text.Trim())
                    .AddParameter("PostalCode", txtPostalCode.Text.Trim())
                End With
            End With

            ' Execute the WSAddressTransaction object
            wsaddressResponse = wsaddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                HandleUniqueSearchResponse(wsaddressResponse)
            End If
        Catch ex As Exception
            ' Create custom validator and it to the default validation summary object
            Dim cv As CustomValidator = New CustomValidator()
            Using cv
                cv.ValidationGroup = ""
                cv.IsValid = False
                cv.ErrorMessage = Server.HtmlEncode(ex.ToString())
                Page.Validators.Add(cv)
            End Using
        Finally
            ' Create the HTML for the Request/Response collapsible xml viewer
            HandleRequestResponseXml(wsaddressTransaction, wsaddressResponse)
        End Try

    End Sub

    ''' <summary>
    ''' WSAddress UNIQUE SEARCH action response handler method
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub HandleUniqueSearchResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters

        ' The UNIQUE SEARCH action ONLY returns output parameters
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
        End With

        Dim html As StringBuilder = New StringBuilder()
        ' Display the Status Code output parameter in a single line
        html.Append("<h3>Status Code : [ ")
        html.Append(outputParameters.Item("StatusCode"))
        html.Append(" ]</h3>")

        ' Display the rest of the output parameters in a html table
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output parameters:</th></tr>")
        For Each param As WSAddress.WSAddressParameter In outputParameters
            If (Not param.Name.Equals("StatusCode")) Then
                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                html.Append(param.Name)
                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                html.Append(param.Value)
                html.Append("</td></tr>")
            End If
        Next
        html.Append("</table>")

        Me.resultContainer.InnerHtml = html.ToString()
    End Sub

#End Region

End Class