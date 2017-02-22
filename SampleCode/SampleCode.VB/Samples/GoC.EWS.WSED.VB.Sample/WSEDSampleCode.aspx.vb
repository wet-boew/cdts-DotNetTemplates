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

''' <summary>
''' add page disclaimer.
''' </summary>
''' <remarks></remarks>

Public Class WSEDSampleCode
    Inherits System.Web.UI.Page

    'Constants to identify AD Attributes
    Private Const ADATTR_SN As String = "sn"
    Private Const ADATTR_GIVENNAME As String = "givenName"
    Private Const ADATTR_SAMACCOUNTNAME As String = "samAccountName"
    Private Const ADATTR_OBJECTGUID As String = "objectGuid"

    'Array of the groups you wish to verify group membership.  The sample code uses the software factory and ADC groups as an example.
    Private ADGroupsToVerfiy() As String = {"NA-IITB-DGIIT-ATS-SAT-SAS-SSA-SF-ML", "NA-IITB-DGIIT-DIST-DEV-NET-ADC-CDA-BILING"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            'Verify if the web.config keys for this sample code have values
            VerifyConfigValues()

            'Javascript command to control display when a method is selected from the dropdown
            Me.lstAction.Attributes.Add("onChange", "onActionSelectionChanged();")

            'Pre fill form controls
            Me.txtUserName.Text = GetUserSamAccountName()
            Me.txtObjectGuid.Text = GetUserObjectGuid()
            Me.txtLastName.Text = GetUserLastName()

        End If
    End Sub

#Region "Button Clicks"

    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        'Clear fields
        Me.resultContainer.InnerText = String.Empty

        If Page.IsValid() Then

            'Execute the proper WSED method based on the users selection
            Select Case Me.lstAction.SelectedItem.Value
                Case "IsUserInGroups"
                    IsUserInGroups()
                Case "GetGUIDAttributes"
                    GetGUIDAttributes()
                Case "GetUserAttributes"
                    GetUserAttributes()
                Case "GetUsersAttributes"
                    GetUsersAttributes()
                Case "GetUserAttributesAndGroups"
                    GetUserAttributesAndGroups()
            End Select
        End If

    End Sub

#End Region

#Region "Calls to WSED Methods"

    ''' <summary>
    ''' This method can be used to determine if a specific user is a member of one or many AD groups.  
    ''' Example: Determine a users role or access levels for your application.
    ''' </summary>
    Private Sub IsUserInGroups()

        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests().AddRequest()
            .Name = "IsObjectInGroups" 'WSED method to call

            With .InputParameters
                .AddParameter(ADATTR_SAMACCOUNTNAME, Me.txtUserName.Text.Trim()) ' Input parameter

                'Add the list of groups to verify membership.
                For Each adGroup As String In ADGroupsToVerfiy
                    .AddParameter("Group", adGroup)
                Next

            End With
        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then
                ParseOutputParametersResponse(wsedResponse.AllResults)
            End If

        Catch ex As Exception
            'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
            'You should also loop through the Messages (WSEDresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
            Me.vldCustomValidator.ErrorMessage = ex.ToString()
            Me.vldCustomValidator.IsValid = False

        Finally
            If wsedTransaction IsNot Nothing And wsedResponse IsNot Nothing Then
                'Display request and response xml on screen
                HandleRequestResponseXml(wsedTransaction, wsedResponse)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' This method allows the retrieval of specified user attributes from AD for the provided ObjectGuid.
    ''' </summary>
    Private Sub GetGUIDAttributes()

        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests().AddRequest()
            .Name = "GetGUIDAttributes" 'WSED method to call

            With .InputParameters
                'list input parameters
                .AddParameter("GUID", Me.txtObjectGuid.Text.Trim())
                .AddParameter("Delimiter", "|")
                'list of attributes to retrieve
                .AddParameter("Attribute", ADATTR_SN)
                .AddParameter("Attribute", ADATTR_GIVENNAME)
                .AddParameter("Attribute", ADATTR_OBJECTGUID)
            End With
        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then
                ParseOutputDataSetResponse(wsedResponse.AllResults)
            End If

        Catch ex As Exception
            'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
            'You should also loop through the Messages (WSEDresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
            Me.vldCustomValidator.ErrorMessage = ex.ToString()
            Me.vldCustomValidator.IsValid = False

        Finally
            If wsedTransaction IsNot Nothing And wsedResponse IsNot Nothing Then
                'Display request and response xml on screen
                HandleRequestResponseXml(wsedTransaction, wsedResponse)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' This method allows the retrieval of specified user attributes from AD for the provided samAccountName.
    ''' </summary>
    Private Sub GetUserAttributes()

        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests().AddRequest()
            .Name = "GetUserAttributes" 'WSED method to call

            With .InputParameters
                'list input parameters
                .AddParameter(ADATTR_SAMACCOUNTNAME, Me.txtUserName.Text.Trim())
                .AddParameter("Delimiter", "|")
                'list of attributes to retrieve
                .AddParameter("Attribute", ADATTR_SN)
                .AddParameter("Attribute", ADATTR_GIVENNAME)
                .AddParameter("Attribute", ADATTR_OBJECTGUID)
            End With
        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then
                ParseOutputDataSetResponse(wsedResponse.AllResults)
            End If

        Catch ex As Exception
            'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
            'You should also loop through the Messages (WSEDresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
            Me.vldCustomValidator.ErrorMessage = ex.ToString()
            Me.vldCustomValidator.IsValid = False
        Finally
            If wsedTransaction IsNot Nothing And wsedResponse IsNot Nothing Then
                'Display request and response xml on screen
                HandleRequestResponseXml(wsedTransaction, wsedResponse)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' This method allows the retrieval of specified user attributes from AD for the provided search criteria.
    ''' Since a wildcard (*) can be used in the search criteria this method can return multiple rows.
    ''' </summary>
    Private Sub GetUsersAttributes()

        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests().AddRequest()
            .Name = "GetUsersAttributes" 'WSED method to call

            With .InputParameters
                'list input parameters
                .AddParameter("SearchFilter", String.Concat(ADATTR_GIVENNAME, "=*"))
                .AddParameter("SearchFilter", String.Concat(ADATTR_SN, "=", Me.txtLastName.Text.Trim(), "*"))
                .AddParameter("Delimiter", "|")
                .AddParameter("MaxResults", "10")
                .AddParameter("Attribute", ADATTR_SN)
                .AddParameter("Attribute", ADATTR_GIVENNAME)
                .AddParameter("Attribute", ADATTR_OBJECTGUID)
            End With

        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then
                ParseOutputDataSetResponse(wsedResponse.AllResults)
            End If

        Catch ex As Exception
            'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
            'You should also loop through the Messages (WSEDresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
            Me.vldCustomValidator.ErrorMessage = ex.ToString()
            Me.vldCustomValidator.IsValid = False
        Finally
            If wsedTransaction IsNot Nothing And wsedResponse IsNot Nothing Then
                'Display request and response xml on screen
                HandleRequestResponseXml(wsedTransaction, wsedResponse)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' This method provides sample code that simulates the typical use of WSED in a single sign on application.  
    ''' It retrieves the logged in user's information and group memberships in 1 call to WSED.
    ''' </summary>
    Private Sub GetUserAttributesAndGroups()

        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests()

            Dim userAttributeRequest As WSED.WSEDRequest
            Dim userRolesRequest As WSED.WSEDRequest

            userAttributeRequest = .AddRequest()

            With userAttributeRequest
                .Name = "GetUserAttributes" 'WSED method to call

                With .InputParameters
                    'list input parameters
                    .AddParameter(ADATTR_SAMACCOUNTNAME, Me.txtUserName.Text.Trim())
                    .AddParameter("Delimiter", "|")
                    'list of attributes to retrieve
                    .AddParameter("Attribute", ADATTR_SN)
                    .AddParameter("Attribute", ADATTR_GIVENNAME)
                    .AddParameter("Attribute", ADATTR_OBJECTGUID)
                End With
            End With

            userRolesRequest = .AddRequest

            With userRolesRequest
                .Name = "IsObjectInGroups" 'WSED method to call

                With .InputParameters
                    .AddParameter(ADATTR_SAMACCOUNTNAME, Me.txtUserName.Text.Trim()) ' Input parameter
                    'Add the list of groups to verify membership.
                    For Each adGroup As String In ADGroupsToVerfiy
                        .AddParameter("Group", adGroup)
                    Next
                End With
            End With

        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then
                ParseGetUserAttributesAndGroupsResponse(wsedResponse.AllResults)
            End If

        Catch ex As Exception
            'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
            'You should also loop through the Messages (WSEDresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
            Me.vldCustomValidator.ErrorMessage = ex.ToString()
            Me.vldCustomValidator.IsValid = False
        Finally
            If wsedTransaction IsNot Nothing And wsedResponse IsNot Nothing Then
                'Display request and response xml on screen
                HandleRequestResponseXml(wsedTransaction, wsedResponse)
            End If
        End Try

    End Sub

#End Region

#Region "Parse results of Calls to WSED"
    ''' <summary>
    ''' This method parses the results of a call that returns a dataset (GetGUIDAttributes, GetUserAttributes, GetUsersAttributes).  
    ''' The method builds a table to display the results on screens.
    ''' </summary>
    ''' <param name="AllResults"></param>
    ''' <remarks></remarks>
    Private Sub ParseOutputDataSetResponse(ByVal AllResults As WSED.WSEDAllResults)

        Dim userFound As Boolean = True
        Dim html As StringBuilder = New StringBuilder()

        With AllResults.Item(0).Item(0)
            If .OutputDataset IsNot Nothing Then
                If .OutputDataset.Tables.Count() > 0 Then

                    html.Append("<h3>Status Code : [ Successful ]</h3> Execution time: ")
                    html.Append(.Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
                    html.Append(" milliseconds")

                    'Loop through the rows of the dataset
                    For Each tableRow As Data.DataRow In .OutputDataset.Tables(0).Rows

                        'Display the results in a table
                        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output Dataset:</th></tr>")

                        With tableRow
                            'get sn
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_SN)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_SN))
                            html.Append("</td></tr>")

                            'get givenName
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_GIVENNAME)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_GIVENNAME))
                            html.Append("</td></tr>")

                            'get objectGuid
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_OBJECTGUID)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_OBJECTGUID))
                            html.Append("</td></tr>")
                        End With

                        html.Append("</table><br />")
                    Next

                Else
                    userFound = False
                End If
            Else
                userFound = False
            End If
        End With

        'Display msg if user was not found
        If userFound = False Then
            html.Append("<h3>Status Code : [ Failed - User was not found.]</h3> Execution time: ")
            html.Append(AllResults(0)(0).Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
            html.Append(" milliseconds")
        End If

        'Write results to screen
        Me.resultContainer.InnerHtml = html.ToString()

    End Sub

    ''' <summary>
    ''' This method parses the results of a call that returns output parameters (IsUserInGroups).  
    ''' The method builds a table to display the results on screens.
    ''' </summary>
    ''' <param name="AllResults"></param>
    ''' <remarks>If the user was not found, the call to the retrieve the group membership will not fail but return false for all groups. </remarks>
    Private Sub ParseOutputParametersResponse(ByVal AllResults As WSED.WSEDAllResults)

        Dim html As StringBuilder = New StringBuilder()

        With AllResults.Item(0).Item(0)
            If .OutputParameters IsNot Nothing Then

                html.Append("<h3>Status Code : [ Successful ]</h3> Execution time: ")
                html.Append(.Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
                html.Append(" milliseconds")

                html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output Parameters:</th></tr>")

                'Loop through the results
                For Each outParam As WSED.WSEDParameter In .OutputParameters
                    'Display the results in a table
                    html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                    html.Append(outParam.Name)
                    html.Append("</td>")
                    html.Append("<td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                    html.Append(outParam.Value)
                    html.Append("</td></tr>")
                Next
                html.Append("</table><br />")

            End If
        End With

        'Write results to screen
        Me.resultContainer.InnerHtml = html.ToString()

    End Sub

    ''' <summary>
    ''' This method parses the results of a call to GetUserAttributesAndGroups which contains 2 WSED method calls (GetUserAttributes and IsObjectInGroups).  
    ''' The method builds 2 tables to display the results on screens.
    ''' </summary>
    ''' <param name="AllResults"></param>
    ''' <remarks></remarks>
    Private Sub ParseGetUserAttributesAndGroupsResponse(ByVal AllResults As WSED.WSEDAllResults)

        Dim userFound As Boolean = True
        Dim html As StringBuilder = New StringBuilder()

        With AllResults.Item(0).Item(0)
            If .OutputDataset IsNot Nothing Then
                If .OutputDataset.Tables.Count() > 0 Then
                    If .OutputDataset.Tables(0).Rows.Count() > 0 Then

                        'The user was found
                        html.Append("<h3>Status Code : [ Successful ]</h3> Execution time: ")
                        html.Append(.Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
                        html.Append(" milliseconds")

                        'Display the user attributes requested 
                        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output Dataset:</th></tr>")

                        With .OutputDataset.Tables(0).Rows(0)
                            'get sn
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_SN)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_SN))
                            html.Append("</td></tr>")

                            'get givenName
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_GIVENNAME)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_GIVENNAME))
                            html.Append("</td></tr>")

                            'get objectGuid
                            html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                            html.Append(ADATTR_OBJECTGUID)
                            html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                            html.Append(.Item(ADATTR_OBJECTGUID))
                            html.Append("</td></tr>")

                        End With

                        html.Append("</table><br />")

                        'Display the group memberships requested
                        '   Note: if the user was not found, the call to the retrieve the group membership will not fail but return false for all groups.
                        If AllResults.Item(0).Item(1).OutputParameters IsNot Nothing Then
                            html.Append("<h3>Status Code : [ Successful ]</h3> Execution time: ")
                            html.Append(AllResults(0)(1).Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
                            html.Append(" milliseconds")

                            html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'><tr bgcolor='#C0C0C0'><th colspan='2'>Output Parameters:</th></tr>")

                            'Loop through the results
                            For Each outParam As WSED.WSEDParameter In AllResults.Item(0).Item(1).OutputParameters
                                'Display the results in a table
                                html.Append("<tr><td style='border:1px solid black; width:250px; border-collapse:collapse;'>")
                                html.Append(outParam.Name)
                                html.Append("</td><td style='border:1px solid black; width:350px; border-collapse:collapse;'>")
                                html.Append(outParam.Value)
                                html.Append("</td></tr>")

                            Next
                            html.Append("</table><br />")
                        End If
                    Else
                        userFound = False
                    End If
                Else
                    userFound = False
                End If
            Else
                userFound = False
            End If
        End With

        'Display msg if user was not found
        If userFound = False Then
            html.Append("<h3>Status Code : [ Failed - User was not found.]</h3> Execution time: ")
            html.Append(AllResults(0)(0).Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
            html.Append(" milliseconds")
        End If

        'Write results to screen
        Me.resultContainer.InnerHtml = html.ToString()

    End Sub

#End Region

#Region "EWS Helper Methods"

    ''' <summary>
    ''' This method verifies that the web.config keys that this sample code relies on have values.  
    ''' If they are empty an error will be displayed to the user.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VerifyConfigValues()

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.ServiceAccount.Name") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.ServiceAccount.Name' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.ServiceAccount.Password") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.ServiceAccount.Password' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.Domain") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.Domain' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.URL") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.URL' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.TimeOut") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSED.TimeOut' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

    End Sub

    ''' <summary>
    ''' This function prepares a Transaction object to be used for the WS call.  It sets the security information of your user and application and contains the 
    ''' connection information to be able to reach WSED.
    ''' </summary>
    ''' <returns>WSED.WSEDTransaction object</returns>
    ''' <remarks>The values used should be reviewed and updated to meet your application.</remarks>
    Private Function GetTransaction() As WSED.WSEDTransaction

        Dim applicationName As String = "WSED Sample Code"
        Dim applicationVersion As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
        Dim applicationIpAddress As String = String.Empty
        Dim applicationMachineName As String = Dns.GetHostName()
        Dim applicationRole As String = String.Empty

        Dim ipAddresses() As IPAddress
        ipAddresses = Dns.GetHostAddresses(applicationMachineName)
        If ipAddresses.Length() > 0 Then
            applicationIpAddress = ipAddresses(0).ToString()
        End If

        Dim employeeId As String = Me.Session.SessionID
        Dim employeeName As String = GetUserSamAccountName()
        Dim employeeIpAddress As String = Request.UserHostAddress()
        Dim employeeMachineName As String = Request.UserHostName()
        Dim employeeRole As String = String.Empty

        Dim clientId As String = String.Empty
        Dim clientName As String = String.Empty
        Dim clientIpAddress As String = String.Empty
        Dim clientMachineName As String = String.Empty
        Dim clientRole As String = String.Empty
        Dim assuranceLevel As String = String.Empty

        'If you do not have a service account or your service account does not have access to this service please complete the proper excel on the sharepoint site "http://architecture/SF-ML/softwarefactory/General%20Documents/Forms/AllItems.aspx"
        'Note: The sample code does not expect the password to be encrypted, but in your implementation you should encrypt this value.
        Dim userName As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.ServiceAccount.Name")
        Dim password As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.ServiceAccount.Password")
        Dim domain As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.Domain")
        Dim wsedUrl As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.URL")
        Dim timeout As Integer = ConfigurationManager.AppSettings.Get("GoC.EWS.WSED.TimeOut")

        Dim wsedTransaction As WSED.WSEDTransaction

        wsedTransaction = New WSED.WSEDTransaction(applicationName, _
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
                                                 wsedUrl, _
                                                 timeout)

        Return wsedTransaction

    End Function

    ''' <summary>
    '''  This method will generate the HTML required for the collapsible Request/Response xml viewer
    ''' </summary>
    ''' <param name="WSEDTransaction"></param>
    ''' <param name="WSEDResponse"></param>
    ''' <remarks>
    '''  The request and response xml will be embedded in the xmp tag. NOTE: the xmp tag will not be supported by IE starting IE 11, 
    '''  the collapse/expand arrow is a HTML entity that when clicked, will invoked the "toggle(...)" javascript declared on the aspx page
    ''' </remarks>
    Private Sub HandleRequestResponseXml(ByVal wsedTransaction As WSED.WSEDTransaction, ByVal wsedResponse As WSED.WSEDResponse)

        Me.requestXml.InnerHtml = "<h3>Request xml: <a href='#' title='expand' style='text-decoration: none;' id='requestXmlTriangle' onclick=""toggle('request')"">&#9660;</a></h3>" +
                                  "<div id='requestXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsedTransaction.GetXML() + "</xmp></div></div>"

        Me.responseXml.InnerHtml = "<h3>Response xml: <a href='#' title='expand' style='text-decoration: none;' id='responseXmlTriangle' onclick=""toggle('response')"">&#9660;</a></h3>" +
                                   "<div id='responseXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsedResponse.GetXML() + "</xmp></div></div>"

    End Sub

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
    ''' This method retrieves the last name from the samAccountName of the current logged in user accessing your application
    ''' </summary>
    ''' <returns>String: the lastname of the logged in user. ex: Murphy</returns>
    ''' <remarks></remarks>
    Private Shared Function GetUserLastName() As String

        Dim samAccountName() As String = My.User.Name.Split("."c)

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
    ''' This is a copy of the method "GetUserAttributes" with a slight change to pre-populate the form field txtObjectGuid, during the page load
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If the call fails, the method will leave the txtObjectGuid textbox empty to allow the page to load.</remarks>
    Private Function GetUserObjectGuid() As String
        Dim wsedTransaction As WSED.WSEDTransaction
        Dim wsedResponse As WSED.WSEDResponse = Nothing
        Dim objectGuid As String = String.Empty

        'Set the Transaction information
        wsedTransaction = GetTransaction()

        'Set the request information
        With wsedTransaction.AllRequests.AddRequests().AddRequest()
            .Name = "GetUserAttributes" 'WSED method to call

            With .InputParameters
                'list input parameters
                .AddParameter(ADATTR_SAMACCOUNTNAME, Me.txtUserName.Text.Trim())
                .AddParameter("Delimiter", "|")
                'list of attributes to retrieve
                .AddParameter("Attribute", ADATTR_OBJECTGUID)
            End With
        End With

        Try
            'Call WSED
            wsedResponse = wsedTransaction.Execute()

            'Validate and Parse the response
            If wsedResponse.Validate(True) = True Then

                'Get the ObjectGuid from the response
                With wsedResponse.AllResults.Item(0).Item(0)
                    If .OutputDataset IsNot Nothing Then
                        If .OutputDataset.Tables.Count() > 0 Then
                            If .OutputDataset.Tables(0).Rows IsNot Nothing Then
                                objectGuid = .OutputDataset.Tables(0).Rows(0).Item(ADATTR_OBJECTGUID)
                            End If
                        End If
                    End If
                End With

            End If

        Catch ex As Exception
            'I will absorb any exceptions which will leave the txtObjectGuid textbox empty and allow the page to load. 
        End Try

        Return objectGuid
    End Function

    ''' <summary>
    ''' This method is used for the custom validator. It validates that a value is entered in the correct textbox based on the method selected in the dropdown.
    ''' The validation is performed when the submit button is clicked.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub vldCustomValidator_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles vldCustomValidator.ServerValidate

        'Validate the proper input control based selected method.
        Select Case Me.lstAction.SelectedItem.Value
            Case "IsUserInGroups", "GetUserAttributes", "GetUserAttributesAndGroups"
                If Me.txtUserName.Text = String.Empty Then
                    Me.vldCustomValidator.ErrorMessage = "A UserName must be provided."
                    Me.txtUserName.Focus()
                    args.IsValid = False
                End If

            Case "GetGUIDAttributes"
                If Me.txtObjectGuid.Text = String.Empty Then
                    Me.vldCustomValidator.ErrorMessage = "An objectGuid must be provided."
                    Me.txtObjectGuid.Focus()
                    args.IsValid = False
                End If

            Case "GetUsersAttributes"
                If Me.txtLastName.Text = String.Empty Then
                    Me.vldCustomValidator.ErrorMessage = "An Last Name must be provided."
                    Me.txtLastName.Focus()
                    args.IsValid = False
                End If
        End Select

    End Sub

#End Region

End Class