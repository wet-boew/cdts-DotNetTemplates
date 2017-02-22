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

Public Class WSEMailSampleCode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            'Verify if the web.config keys for this sample code have values
            VerifyConfigValues()
        End If
    End Sub

#Region "Button Click - Call to WSEMail Method"

    ''' <summary>
    ''' When the user clicks the button, the call to WSEMail is executed
    ''' </summary>
    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        'Clear fields
        Me.resultContainer.InnerText = String.Empty

        If Page.IsValid() Then

            Dim wsemailTransaction As WSEMail.WSEMailTransaction
            Dim wsemailResponse As WSEMail.WSEMailResponse = Nothing

            'Set the Transaction information
            wsemailTransaction = GetTransaction()

            'Set the request information
            With wsemailTransaction.AllRequests.AddRequests().AddRequest()

                'Set the person responsible for sending the email. The Sample uses your Service Account name with "@hrsdc-rhdcc.gc.ca". This should fall in line with ETI standards
                With .Originator
                    .From.DisplayName = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Name")
                    .From.EmailAddress = String.Concat(ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Name"), "@hrsdc-rhdcc.gc.ca")
                End With

                'Set the people who will receive the email
                With .Recipients

                    Dim toEmails() As String = txtTo.Text.Trim.Split(","c)
                    Dim ccEmails() As String = txtCC.Text.Trim.Split(","c)

                    'loop through each email address entered in the TO textbox
                    For Each toEmail As String In toEmails
                        toEmail = toEmail.Trim()

                        If Not String.IsNullOrEmpty(toEmail) Then
                            .To.AddEmailAddress(toEmail)
                        End If
                    Next

                    'loop through each email address entered in the CC textbox
                    For Each ccEmail As String In ccEmails
                        If ccEmail.Trim() IsNot String.Empty Then
                            .CC.AddEmailAddress(ccEmail.Trim())
                        End If
                    Next

                    ' BCC is also available but not used in this sample code
                    '    .BCC.AddEmailAddress("[recipient email address]")
                End With

                'Set the Subject and Body of the email
                With .Content
                    .BodyType = WSEMail.BodyType.HTML
                    .Priority = WSEMail.Priority.Normal
                    .DeliveryNotification = WSEMail.DeliveryNotification.Never

                    .Subject = "This is a test from the WSEMail Sample Code"
                    .Body = "<H1>WSEMail Sample Code</H1>The content of your <font color=red>email</font> body would be located here. This is an example of a simple html email body. <br><img src=""cid:LeafImage""><br><h2><a href=""http://architecture/SF-ML/softwarefactory/WSEMail/Forms/AllItems.aspx"">WSEMail Developers Guide</a></h2><br><i><b>Signature</b></i>"
                End With

                'Add the Embedded Image to the email that is referenced in the body of the email
                Try
                    .EmbeddedImages.AddEmbeddedImage("LeafImage", _
                                                    "image/gif", _
                                                    My.Computer.FileSystem.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("\Samples\GoC.EWS.WSEMail.VB.Sample\lffl.gif")))
                Catch ex As Exception
                    'For this sample code we will absorb the exception should there be an issue with the file
                    'but your application should properly handle the exception and log it.
                End Try

                'Add an Attachment to the email
                Try
                    Dim filePath As String = System.Web.HttpContext.Current.Server.MapPath("\Samples\GoC.EWS.WSEMail.VB.Sample\ReadMe.txt")

                    .Attachments.AddAttachment("ReadMe.txt", _
                                               My.Computer.FileSystem.ReadAllBytes(filePath))
                Catch ex As Exception
                    'For this sample code we will absorb the exception should there be an issue with the file
                    'but your application should properly handle the exception and log it.
                End Try
            End With

            Try
                'Call WSEMail
                wsemailResponse = wsemailTransaction.Execute()

                'Validate and Parse the response
                If wsemailResponse.Validate(True) = True Then
                    ParseResponse(wsemailResponse)
                End If

            Catch ex As Exception
                'For this sample code we will simply display the exception on screen but your application should properly handle the exception and log it.
                'You should also loop through the Messages (WSEMailresponse.Messages) of the response and log the Errors, Warnings and Information messages that it may contain.
                Me.vldCustomValidator.ErrorMessage = ex.ToString()
                Me.vldCustomValidator.IsValid = False

            Finally
                If wsemailTransaction IsNot Nothing And wsemailResponse IsNot Nothing Then
                    'Display request and response xml on screen
                    HandleRequestResponseXml(wsemailTransaction, wsemailResponse)
                End If
            End Try

        End If

    End Sub

#End Region

#Region "Parse result of Call to WSEMail"
    ''' <summary>
    ''' This method parses the results of a call to WSEMail.  
    ''' The method build a string to displays the status and processing time on screens.
    ''' </summary>
    ''' <param name="Response"></param>
    ''' <remarks>
    ''' Possible Statuses:
    '''     Sent: Email was successfully sent to all recipients.	
    '''     SentWithWarnings: SMTP server realizes that an email address or its domain does not exist.  
    '''                       The email was still sent but did not reach all recipients.  
    '''                       The following warning message will be returned: [FailedToSendToAllRecipients] Email failed to send to one or more recipients.
    '''</remarks>
    Private Sub ParseResponse(ByVal response As WSEMail.WSEMailResponse)

        Dim html As StringBuilder = New StringBuilder()

        With response.AllResults.Item(0).Item(0)

            'Display status of call
            html.Append("<h3>Status Code : [ ")
            html.Append(.SentStatus.ToString)
            html.Append(" ]</h3> Execution time: ")
            html.Append(.Statistics.ProcessDuration.ToString(CultureInfo.InvariantCulture))
            html.Append(" milliseconds")

            'Display any warning messages
            If response.Messages.Warnings.Count > 0 Then
                For iIndex As Integer = 0 To response.Messages.Warnings.Count - 1
                    'For this sample code we will simply display the messages on screen but your application should properly handle the scenario and log it.
                    Me.vldCustomValidator.ErrorMessage = String.Concat("Warning: ", response.Messages.Warnings.Item(iIndex).Complete)
                    Me.vldCustomValidator.IsValid = False
                Next
            End If
        End With

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

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Name") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSEMail.ServiceAccount.Name' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Password") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSEMail.ServiceAccount.Password' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.Domain") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSEMail.Domain' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.URL") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSEMail.URL' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

        If ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.TimeOut") = String.Empty Then
            Me.vldCustomValidator.ErrorMessage = "You need to supply a value for the key 'GoC.EWS.WSEMail.TimeOut' in the web.config, for this sample code to work."
            Me.vldCustomValidator.IsValid = False
        End If

    End Sub

    ''' <summary>
    ''' This function prepares a Transaction object to be used for the WS call.  It sets the security information of your user and application and contains the 
    ''' connection information to be able to reach WSEMail.
    ''' </summary>
    ''' <returns>WSEMail.WSEMailTransaction object</returns>
    ''' <remarks>The values used should be reviewed and updated to meet your application.</remarks>
    Private Function GetTransaction() As WSEMail.WSEMailTransaction

        Dim applicationName As String = "WSEMail Sample Code"
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
        Dim userName As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Name")
        Dim password As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.ServiceAccount.Password")
        Dim domain As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.Domain")
        Dim wsemailUrl As String = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.URL")
        Dim timeout As Integer = ConfigurationManager.AppSettings.Get("GoC.EWS.WSEMail.TimeOut")

        Dim wsemailTransaction As WSEMail.WSEMailTransaction

        wsemailTransaction = New WSEMail.WSEMailTransaction(applicationName, _
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
                                                 wsemailUrl, _
                                                 timeout)

        Return wsemailTransaction

    End Function

    ''' <summary>
    '''  This method will generate the HTML required for the collapsible Request/Response xml viewer
    ''' </summary>
    ''' <param name="WSEMailTransaction"></param>
    ''' <param name="WSEMailResponse"></param>
    ''' <remarks>
    '''  The request and response xml will be embedded in the xmp tag. NOTE: the xmp tag will not be supported by IE starting IE 11, 
    '''  the collapse/expand arrow is a HTML entity that when clicked, will invoked the "toggle(...)" javascript declared on the aspx page
    ''' </remarks>
    Private Sub HandleRequestResponseXml(ByVal wsemailTransaction As WSEMail.WSEMailTransaction, ByVal wsemailResponse As WSEMail.WSEMailResponse)

        Me.requestXml.InnerHtml = "<h3>Request xml: <a href='#' title='expand' style='text-decoration: none;' id='requestXmlTriangle' onclick=""toggle('request')"">&#9660;</a></h3>" +
                                  "<div id='requestXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsemailTransaction.GetXML() + "</xmp></div></div>"

        Me.responseXml.InnerHtml = "<h3>Response xml: <a href='#' title='expand' style='text-decoration: none;' id='responseXmlTriangle' onclick=""toggle('response')"">&#9660;</a></h3>" +
                                   "<div id='responseXmlContainer' style='display: none;'><div style='border: 1px dashed black;'><xmp>" + wsemailResponse.GetXML() + "</xmp></div></div>"

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

#End Region

End Class