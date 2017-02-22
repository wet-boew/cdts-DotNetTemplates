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

Public Class WSAddressSampleFlow
    Inherits System.Web.UI.Page

#Region "Constants"

    Private Const WSADDRESS_COUNTRY_CODE As String = "CAN"
    Private Const WSADDRESS_COUNTRY_NAME As String = "CANADA"

    Private Const WSADDRESS_ADDR_TYPE_UA As String = "UrbanAddress"
    Private Const WSADDRESS_ADDR_TYPE_UR As String = "UrbanRoute"
    Private Const WSADDRESS_ADDR_TYPE_RGD As String = "RuralGeneralDelivery"
    Private Const WSADDRESS_ADDR_TYPE_RLB As String = "RuralLockBox"
    Private Const WSADDRESS_ADDR_TYPE_RR As String = "RuralRoute"

    Private Const WSADDRESS_VALIDATE_VALID_STATUS As String = "Valid"
    Private Const WSADDRESS_VALIDATE_CORRECTED_STATUS As String = "Corrected"
    Private Const WSADDRESS_VALIDATE_NOT_CORRECT_STATUS As String = "NotCorrect"

    Private Const WSADDRESS_SEARCH_RECORDS_FOUND_STATUS As String = "RecordsFound"

    Private Const WSADDRESS_SEARCH_ACTION_CODE As String = "Search"
    Private Const WSADDRESS_VALIDATE_ACTION_CODE As String = "Correct"

    Private Const ADDR_MATCHES_SESSION_KEY As String = "addressMatches"
    Private Const ADDR_MATCH_SEL_SESSION_KEY As String = "addressMatchSelected"
    Private Const CORRECT_ADDR_RESPONSE_SESSION_KEY As String = "addressWSResponse"

#End Region

#Region "Variable Declarations"
    Private WSAddressTransaction As WSAddress.WSAddressTransaction
#End Region

#Region "ASP .NET Page Events Handlers"

    ''' <summary>
    ''' Page Load event handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Initialize the Address Match dictionary/collection in the Session if not already done
        If (Me.Session(ADDR_MATCHES_SESSION_KEY) Is Nothing) Then Me.Session(ADDR_MATCHES_SESSION_KEY) = New Dictionary(Of String, WSAddress.WSAddressMatch)()
    End Sub

#End Region

#Region "Previous and Next button click event handlers"

    ''' <summary>
    ''' Click event handler for the "NEXT" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        ' First find out the page we are currently on base on the value of the 
        ' containerSelected hidden field value
        Dim currentPage As String = Me.containerSelected.Value

        ' Invoke the corresponding On...PageSubmit() method depending on which page 
        ' we currently are on
        Select Case currentPage
            Case "postalCodeContainer"
                OnPostalCodeEntryPageSubmit()
            Case "searchResultContainer"
                OnSearchResultPageSubmit()
            Case "urbanAddressContainer"
                OnUrbanAddressPageSubmit()
            Case "urbanRouteContainer"
                OnUrbanRoutePageSubmit()
            Case "ruralGDContainer"
                OnRuralGeneralDeliveryPageSubmit()
            Case "ruralLockBoxContainer"
                OnRuralLockBoxPageSubmit()
            Case "ruralRouteContainer"
                OnRuralRoutePageSubmit()
            Case "correctedAddressContainer"
                OnCorrectedAddressPageSubmit()
        End Select
    End Sub

    ''' <summary>
    ''' Click event handler for the "PREVIOUS" button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        ' First find out the page we are currently on base on the value of the 
        ' containerSelected hidden field value
        Dim currentPage As String = Me.containerSelected.Value

        Select Case currentPage
            Case "searchResultContainer"
                ' If on the search grid page, navigate back to the Postal Code 
                ' input and disable the Previous button
                Me.containerSelected.Value = "postalCodeContainer"
                Me.btnPrev.Enabled = False
            Case "urbanAddressContainer", "urbanRouteContainer", "ruralGDContainer",
                 "ruralLockBoxContainer", "ruralRouteContainer"
                ' If on any of the 5 different Address Detail page, navigate back to 
                ' the Search Grid page
                Me.containerSelected.Value = "searchResultContainer"
            Case "correctedAddressContainer", "validAddressContainer"
                ' First retrieve the currently selected AddressMatch object from the session
                Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch)

                ' Navigate back to one of the 5 address detail page depending on the 
                ' currently selected AddressMatch object's AddressType value
                Select Case addrMatchSelected.AddressType
                    Case WSADDRESS_ADDR_TYPE_UA
                        Me.containerSelected.Value = "urbanAddressContainer"
                    Case WSADDRESS_ADDR_TYPE_UR
                        Me.containerSelected.Value = "urbanRouteContainer"
                    Case WSADDRESS_ADDR_TYPE_RGD
                        Me.containerSelected.Value = "ruralGDContainer"
                    Case WSADDRESS_ADDR_TYPE_RLB
                        Me.containerSelected.Value = "ruralLockBoxContainer"
                    Case WSADDRESS_ADDR_TYPE_RR
                        Me.containerSelected.Value = "ruralRouteContainer"
                End Select

                ' Enable the Next button
                Me.btnNext.Enabled = True
        End Select
    End Sub

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Helper routine to add a CustomValidator to the ValidationSummary object
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Private Sub AddValidationMessage(ByVal message As String)
        Dim cv As CustomValidator = New CustomValidator()
        Using cv
            cv.ValidationGroup = "ValList"
            cv.IsValid = False
            cv.ErrorMessage = message
            Page.Validators.Add(cv)
        End Using
    End Sub

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

        Dim returnedWSAddressTransaction As WSAddress.WSAddressTransaction

        returnedWSAddressTransaction = New WSAddress.WSAddressTransaction(applicationName, _
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

        Return returnedWSAddressTransaction

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
    ''' Helper function to get the correct value from the Address Suffix drop-down list
    ''' </summary>
    ''' <param name="selValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetSuffixDropDownValue(ByVal selValue As String) As String
        If (selValue.Equals("0")) Then Return String.Empty

        ' 1 means 1/4, 2 means 1/2, and 3 means 3/4
        If (selValue.Equals("1") OrElse selValue.Equals("2") OrElse selValue.Equals("3")) Then
            Return If(selValue.Equals("1"), " 1/4", If(selValue.Equals("2"), " 1/2", " 3/4"))
        End If

        ' otherwise, just return the corresponding alpha value
        Return selValue
    End Function

    ''' <summary>
    ''' Helper function to validate the Street Number value inputted, this is invoked when clicking 
    ''' the "NEXT" submit button on the Urban and Urban Route Address details page
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <param name="streetNumber"></param>
    ''' <remarks></remarks>
    Private Sub ValidateUrbanAddress(ByVal addrMatchSelected As WSAddress.WSAddressMatch, ByVal streetNumber As String)

        If (String.IsNullOrEmpty(streetNumber.Trim) OrElse Not Integer.TryParse(streetNumber.Trim(), Nothing)) Then
            ' Error message if the Street Number value was not inputted or non-numeric
            AddValidationMessage("Street number is required and must be a number")
        Else
            ' According to the Address currently selected, retrieve the Max/Min Street Number value allowed
            Dim streetNumberMax As Integer = Integer.Parse(addrMatchSelected.AddressParameters("StreetNumberMaximum"), CultureInfo.InvariantCulture)
            Dim streetNumberMin As Integer = Integer.Parse(addrMatchSelected.AddressParameters("StreetNumberMinimum"), CultureInfo.InvariantCulture)

            ' Retrieve the Street Number value inputted
            Dim sn As Integer = Integer.Parse(streetNumber.Trim(), CultureInfo.InvariantCulture)

            If (sn < streetNumberMin OrElse sn > streetNumberMax) Then
                ' Error message if the inputted street number is not within the allowed range
                AddValidationMessage("The street number entered is not within the range of " + streetNumberMin.ToString(CultureInfo.InvariantCulture) + " to " + streetNumberMax.ToString(CultureInfo.InvariantCulture))
            End If

            ' According to the Address currently selected, retrieve the Street Address Sequence value
            Dim seq As String = addrMatchSelected.AddressParameters("StreetAddressSequence")
            If (Not seq.Equals("Consecutive")) Then
                ' If not Consecutive, depending on the inputted street number value, determine if it 
                ' is Even or Odd as according to the allowed value, create Error Message if the inputted 
                ' value is not appropriate

                If (seq.Equals("Even") AndAlso Not sn Mod 2 = 0) Then
                    AddValidationMessage("The street number entered must be an even number")
                End If

                If (seq.Equals("Odd") AndAlso sn Mod 2 = 0) Then
                    AddValidationMessage("The street number entered must be an odd number")
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Helper function to retrieve the Street Perfect Status Code after calling CORRECT
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetSPCorrectActionStatusCode(ByVal wsaddressResponse As WSAddress.WSAddressResponse) As String
        Dim outputParameters As WSAddress.WSAddressParameters

        ' The VALIDATE action can return both output parameters and function messages
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
        End With

        Return outputParameters.Item("StatusCode").Trim()
    End Function

#End Region

#Region "Page Next Submit Button Handlers"

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Postal
    ''' Code input page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnPostalCodeEntryPageSubmit()
        If String.IsNullOrEmpty(Me.txtPostalCode.Text.Trim()) Then
            ' Make sure that a value was inputted
            AddValidationMessage("Postal code is required to perform address search")
        Else
            ' First clear the Address Search Results from the Session
            DirectCast(Me.Session(ADDR_MATCHES_SESSION_KEY), Dictionary(Of String, WSAddress.WSAddressMatch)).Clear()

            ' Search Street Perfect using Postal Code only
            If (Me.SearchByPostalCode() = False) Then
                ' Error message when no address is found
                AddValidationMessage("Could not find any address matching the postal code entered")
            Else
                ' Retrieve the updated Address Search Results from the Session
                Dim addrMatches As Dictionary(Of String, WSAddress.WSAddressMatch) = DirectCast(Me.Session(ADDR_MATCHES_SESSION_KEY), Dictionary(Of String, WSAddress.WSAddressMatch))

                If (addrMatches.Count = 1) Then
                    ' If there is only 1 address found using Street Perfect, navigate directly to the 
                    ' Address Detail page depending on the Address Type
                    Dim addrMatchSelected As WSAddress.WSAddressMatch = addrMatches.Values.First

                    ' Stored the only Address Match Result in the Session as the Address Selected
                    Me.Session(ADDR_MATCH_SEL_SESSION_KEY) = addrMatchSelected

                    ' Navigate to the apporpriate Address Detail page
                    Select Case addrMatchSelected.AddressType
                        Case "UrbanAddress"
                            NavigateToUrbanAddressPage(addrMatchSelected)
                        Case "UrbanRoute"
                            NavigateToUrbanRoutePage(addrMatchSelected)
                        Case "RuralGeneralDelivery"
                            NavigateToRuralGeneralDeliveryPage(addrMatchSelected)
                        Case "RuralLockBox"
                            NavigateToRuralLockBoxPage(addrMatchSelected)
                        Case "RuralRoute"
                            NavigateToRuralRoutePage(addrMatchSelected)
                    End Select
                Else
                    ' Since there are multiple Search results, navigate to the Search Result 
                    ' grid page, so that the user can pick which address to view detail on
                    Me.containerSelected.Value = "searchResultContainer"
                End If

                ' Enable the "Previous" submit button
                Me.btnPrev.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Search 
    ''' Result grid selection page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnSearchResultPageSubmit()
        ' Retrieve the Address Selection radio button value from the Request
        Dim guidSelected As String = Me.Request.Form("addrMatchSel")

        If (guidSelected Is Nothing) Then
            ' Error message when no address is selected
            AddValidationMessage("Please pick an address from the matching list before proceeding")

            ' Force the user to stay on the same Address Results selection grid page
            Me.containerSelected.Value = "searchResultContainer"
        Else
            ' Base on Address GUID selected from the request, location the corresponding Address Match 
            ' object from the Address Search Result list in Session
            Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCHES_SESSION_KEY), Dictionary(Of String, WSAddress.WSAddressMatch))(guidSelected)

            ' Store the selected Address in the Session
            Me.Session(ADDR_MATCH_SEL_SESSION_KEY) = addrMatchSelected

            ' Navigate to the corresponding Address Detail page depending on the 
            ' Selected Address Address Type
            Select Case addrMatchSelected.AddressType
                Case "UrbanAddress"
                    NavigateToUrbanAddressPage(addrMatchSelected)
                Case "UrbanRoute"
                    NavigateToUrbanRoutePage(addrMatchSelected)
                Case "RuralGeneralDelivery"
                    NavigateToRuralGeneralDeliveryPage(addrMatchSelected)
                Case "RuralLockBox"
                    NavigateToRuralLockBoxPage(addrMatchSelected)
                Case "RuralRoute"
                    NavigateToRuralRoutePage(addrMatchSelected)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Address 
    ''' Details page for an Urban Address
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnUrbanAddressPageSubmit()
        ' Retrieve the selected Address from Session
        Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch)

        ' Call the Urban Address street number validation helper function
        ValidateUrbanAddress(addrMatchSelected, Me.txtUAStreetNumber.Text)

        If (Me.Page.IsValid) Then
            ' Call Street Perfect Correct, which in turn 
            ' will handle the UI navigation as appropriate
            CorrectAddress(addrMatchSelected)
        End If
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Address 
    ''' Details page for an Urban Route Address
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnUrbanRoutePageSubmit()
        ' Retrieve the selected Address from Session
        Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch)

        ' Call the Urban Address street number validation helper function
        ValidateUrbanAddress(addrMatchSelected, Me.txtURStreetNumber.Text)

        If (Me.Page.IsValid) Then
            ' Call Street Perfect Correct, which in turn 
            ' will handle the UI navigation as appropriate
            CorrectAddress(addrMatchSelected)
        End If
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Address
    ''' Details page for Rural General Delivery Address
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnRuralGeneralDeliveryPageSubmit()
        Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch)

        ' Call Street Perfect Correct, which in turn 
        ' will handle the UI navigation as appropriate
        CorrectAddress(addrMatchSelected)
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Address
    ''' Details page for a Rural Lock Box Address (P.O. Box)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnRuralLockBoxPageSubmit()
        ' Retrieve the currently selected Address Match object from Session
        Dim addrMatchSelected As WSAddress.WSAddressMatch = DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch)

        If (String.IsNullOrEmpty(Me.txtRLBBoxNumber.Text.Trim()) OrElse Not Integer.TryParse(Me.txtRLBBoxNumber.Text.Trim(), Nothing)) Then
            ' Error message if the Lock Box Number value was not inputted or is not a number
            AddValidationMessage("Lock box number is required and must be a number")
        Else
            ' According to the Address currently selected, retrieve the Max/Min range allowed for this address
            Dim streetNumberMax As Integer = Integer.Parse(addrMatchSelected.AddressParameters("LockBoxNumberMaximum"), CultureInfo.InvariantCulture)
            Dim streetNumberMin As Integer = Integer.Parse(addrMatchSelected.AddressParameters("LockBoxNumberMinimum"), CultureInfo.InvariantCulture)

            ' Retrieve the Lock Bux Number inputted
            Dim sn As Integer = Integer.Parse(Me.txtRLBBoxNumber.Text.Trim(), CultureInfo.InvariantCulture)

            If (sn < streetNumberMin OrElse sn > streetNumberMax) Then
                ' Error message if the Lock Box Number is not within the range allowed
                AddValidationMessage("The lock box number entered is not within the range of " + streetNumberMin.ToString(CultureInfo.InvariantCulture) + " to " + streetNumberMax.ToString(CultureInfo.InvariantCulture))
            End If
        End If

        If (Me.Page.IsValid) Then
            ' Call Street Perfect Correct, which in turn 
            ' will handle the UI navigation as appropriate
            CorrectAddress(addrMatchSelected)
        End If
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the Address
    ''' Details page for Rural Route Address
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnRuralRoutePageSubmit()
        ' Call Street Perfect Correct, which in turn 
        ' will handle the UI navigation as appropriate
        CorrectAddress(DirectCast(Me.Session(ADDR_MATCH_SEL_SESSION_KEY), WSAddress.WSAddressMatch))
    End Sub

    ''' <summary>
    ''' This method is invoked when clicking the "NEXT" submit button on the CORRECTED 
    ''' Address confirmation page
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnCorrectedAddressPageSubmit()
        ' Clicking "NEXT" on this page means that the user has accepted the 
        ' CORRECTED address as being accurate
        Me.NavigateToValidAddressPage(DirectCast(Me.Session(CORRECT_ADDR_RESPONSE_SESSION_KEY), WSAddress.WSAddressResponse))
    End Sub

#End Region

#Region "Search Result Navigation Handler"

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the Details page of an Urban Address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToUrbanAddressPage(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        'UI Controls initialization
        Me.txtUAStreetNumber.Text = String.Empty
        Me.lblUAStreetNumberHint.Text = String.Empty
        Me.cmbUAUnitType.SelectedValue = "0"
        Me.txtUAUnitNumber.Text = String.Empty
        Me.txtUAStreetNumber.Enabled = True
        Me.txtUAUnitNumber.Enabled = True

        If (addrMatchSelected.AddressParameters("StreetNumberMaximum").Equals(addrMatchSelected.AddressParameters("StreetNumberMinimum"))) Then
            ' If the street number max/min value are the same, set the value of the street number 
            ' textbox to be of that value
            Me.txtUAStreetNumber.Text = addrMatchSelected.AddressParameters("StreetNumberMaximum")

            ' Disable the textbox as no other value can be entered
            Me.txtUAStreetNumber.Enabled = False
        Else
            ' If the street number does have a range, set the text of the street number hint label 
            ' to display the range allowed as well as the sequence, to help the user in input the 
            ' correct value
            Me.lblUAStreetNumberHint.Text = "(" + addrMatchSelected.AddressParameters("StreetNumberMinimum") + " - " + addrMatchSelected.AddressParameters("StreetNumberMaximum") +
                                            " " + addrMatchSelected.AddressParameters("StreetAddressSequence") + " )"
        End If

        ' Update the value of the street name textbox:
        '   - If the province of the address is QUEBEC, the street name value will be street type first followed by the actual street name
        '   - For all other provinces, the street name value will be street name first followed by the street type
        Me.txtUAStreetName.Text = If(addrMatchSelected.AddressParameters("Province").Equals("QC"),
                                      addrMatchSelected.AddressParameters("StreetType") + " " + addrMatchSelected.AddressParameters("StreetName"),
                                      addrMatchSelected.AddressParameters("StreetName") + " " + addrMatchSelected.AddressParameters("StreetType"))

        ' Populate the remaining UI controls
        Me.txtUACity.Text = addrMatchSelected.AddressParameters("City")
        Me.txtUAProvince.Text = addrMatchSelected.AddressParameters("Province")
        Me.txtUAPostalCode.Text = addrMatchSelected.AddressParameters("PostalCode")
        Me.txtUACountry.Text = WSADDRESS_COUNTRY_NAME

        ' Set the navigation flag to the correct value so that the Javascript will 
        ' use that to display the correct page to the end-user
        Me.containerSelected.Value = "urbanAddressContainer"
    End Sub

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the Details page of an Urban Route Address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToUrbanRoutePage(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        'UI Controls initialization
        Me.txtURStreetNumber.Text = String.Empty
        Me.lblURStreetNumberHint.Text = String.Empty
        Me.cmbURUnitType.SelectedValue = "0"
        Me.txtURUnitNumber.Text = String.Empty
        Me.txtURStreetNumber.Enabled = True
        Me.txtURUnitNumber.Enabled = True

        If (addrMatchSelected.AddressParameters("StreetNumberMaximum").Equals(addrMatchSelected.AddressParameters("StreetNumberMinimum"))) Then
            ' If the street number max/min value are the same, set the value of the street number 
            ' textbox to be of that value
            Me.txtURStreetNumber.Text = addrMatchSelected.AddressParameters("StreetNumberMaximum")

            ' Disable the textbox as no other value can be entered
            Me.txtURStreetNumber.Enabled = False
        Else
            ' If the street number does have a range, set the text of the street number hint label 
            ' to display the range allowed as well as the sequence, to help the user in input the 
            ' correct value
            Me.lblURStreetNumberHint.Text = "(" +
                                            addrMatchSelected.AddressParameters("StreetNumberMinimum") + " - " +
                                            addrMatchSelected.AddressParameters("StreetNumberMaximum") + " " +
                                            addrMatchSelected.AddressParameters("StreetAddressSequence") + " )"
        End If

        ' Populate the Route Service Type & Number UI controls
        Me.txtURRouteServiceType.Text = addrMatchSelected.AddressParameters("RouteServiceType")
        Me.txtURRouteServiceNum.Text = addrMatchSelected.AddressParameters("RouteServiceNumber")

        ' Populate the remaining UI controls
        Me.txtURStreetName.Text = addrMatchSelected.AddressParameters("StreetName") + " " + addrMatchSelected.AddressParameters("StreetType")
        Me.txtURCity.Text = addrMatchSelected.AddressParameters("City")
        Me.txtURProvince.Text = addrMatchSelected.AddressParameters("Province")
        Me.txtURPostalCode.Text = addrMatchSelected.AddressParameters("PostalCode")
        Me.txtURCountry.Text = WSADDRESS_COUNTRY_NAME

        ' Set the navigation flag to the correct value so that the Javascript will 
        ' use that to display the correct page to the end-user
        Me.containerSelected.Value = "urbanRouteContainer"
    End Sub

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the Details page of an Rural General Delivery Address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToRuralGeneralDeliveryPage(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        ' Populate the value of fields related to the Delivery Installation
        Me.txtRGDAreaName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationAreaName")
        Me.txtRGDDescription.Text = addrMatchSelected.AddressParameters("DeliveryInstallationDescription")
        Me.txtRGDQualifierName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationQualifierName")

        ' Populate the remaining UI controls
        Me.txtRGDCity.Text = addrMatchSelected.AddressParameters("City")
        Me.txtRGDProvince.Text = addrMatchSelected.AddressParameters("Province")
        Me.txtRGDPostalCode.Text = addrMatchSelected.AddressParameters("PostalCode")
        Me.txtRGDCountry.Text = WSADDRESS_COUNTRY_NAME

        ' Set the navigation flag to the correct value so that the Javascript will 
        ' use that to display the correct page to the end-user
        Me.containerSelected.Value = "ruralGDContainer"
    End Sub

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the Details page of an Rural Lock Box Address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToRuralLockBoxPage(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        'UI Controls initialization
        Me.txtRLBBoxNumber.Text = String.Empty
        Me.lblRLBBoxNumberHint.Text = String.Empty
        Me.txtRLBBoxNumber.Enabled = True

        If (addrMatchSelected.AddressParameters("LockBoxNumberMaximum").Equals(addrMatchSelected.AddressParameters("LockBoxNumberMinimum"))) Then
            ' If the lock box max/min value are the same, set the value of the lock box number
            ' textbox to be of that value
            Me.txtRLBBoxNumber.Text = addrMatchSelected.AddressParameters("LocKBoxNumberMaximum")

            ' Disable the textbox as no other value can be entered
            Me.txtRLBBoxNumber.Enabled = False
        Else
            ' If the street number does have a range, set the text of the lock box number hint label 
            ' to display the range allowed, to help the user in input the correct value
            Me.lblRLBBoxNumberHint.Text = "(" + addrMatchSelected.AddressParameters("LockBoxNumberMinimum") + " - " + addrMatchSelected.AddressParameters("LockBoxNumberMaximum") + ")"
        End If

        ' Populate the value of fields related to the Delivery Installation
        Me.txtRLBAreaName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationAreaName")
        Me.txtRLBDescription.Text = addrMatchSelected.AddressParameters("DeliveryInstallationDescription")
        Me.txtRLBQualifierName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationQualifierName")

        ' Populate the remaining UI controls
        Me.txtRLBCity.Text = addrMatchSelected.AddressParameters("City")
        Me.txtRLBProvince.Text = addrMatchSelected.AddressParameters("Province")
        Me.txtRLBPostalCode.Text = addrMatchSelected.AddressParameters("PostalCode")
        Me.txtRLBCountry.Text = WSADDRESS_COUNTRY_NAME

        ' Set the navigation flag to the correct value so that the Javascript will 
        ' use that to display the correct page to the end-user
        Me.containerSelected.Value = "ruralLockBoxContainer"
    End Sub

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the Details page of an Rural Route Address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToRuralRoutePage(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        ' Populate the value of fields related to the Delivery Installation
        Me.txtRRAreaName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationAreaName")
        Me.txtRRDescription.Text = addrMatchSelected.AddressParameters("DeliveryInstallationDescription")
        Me.txtRRQualifierName.Text = addrMatchSelected.AddressParameters("DeliveryInstallationQualifierName")

        ' Populate the Route Service Type & Number UI controls
        Me.txtRRRouteServiceType.Text = addrMatchSelected.AddressParameters("RuralRouteServiceType")
        Me.txtRRRouteServiceNum.Text = addrMatchSelected.AddressParameters("RuralRouteServiceNumber")

        ' Populate the remaining UI controls
        Me.txtRRCity.Text = addrMatchSelected.AddressParameters("City")
        Me.txtRRProvince.Text = addrMatchSelected.AddressParameters("Province")
        Me.txtRRPostalCode.Text = addrMatchSelected.AddressParameters("PostalCode")
        Me.txtRRCountry.Text = WSADDRESS_COUNTRY_NAME

        ' Set the navigation flag to the correct value so that the Javascript will 
        ' use that to display the correct page to the end-user
        Me.containerSelected.Value = "ruralRouteContainer"
    End Sub

    ''' <summary>
    ''' Helper method to prepare the UI for displaying the confirmation message on whether the 
    ''' CORRECTED address by Street Perfect is accurate or not
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToCorrectedAddressPage(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters = wsaddressResponse.AllResults.Item(0).Item(0).OutputParameters

        ' Display the CORRECTED address in a HTML table
        Dim html As StringBuilder = New StringBuilder()
        html.Append("<h3>There were some minor issue with your inputted address, belowed is the address Corrected by Street Perfect base on your inputs:</h3>")
        html.Append("<table style='border:1px solid black; width:600px; border-collapse:collapse;'>")
        html.Append("<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Address Line</td><td style='border:1px solid black; border-collapse:collapse;'>")
        html.Append(outputParameters("AddressLine").ToString())
        html.Append("</td></tr>")
        html.Append("<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>City</td><td style='border:1px solid black; border-collapse:collapse;'>")
        html.Append(outputParameters("City").ToString())
        html.Append("</td></tr>")
        html.Append("<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Province</td><td style='border:1px solid black; border-collapse:collapse;'>")
        html.Append(outputParameters("Province").ToString())
        html.Append("</td></tr>")
        html.Append("<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Postal Code</td><td style='border:1px solid black; border-collapse:collapse;'>")
        html.Append(outputParameters("PostalCode"))
        html.Append("</td></tr>")
        html.Append("<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Country</td><td style='border:1px solid black; border-collapse:collapse;'>")
        html.Append(WSADDRESS_COUNTRY_CODE)
        html.Append("</td></tr></table>")
        html.Append("<h3>If you don't think the address provided above is accurate, click the ""Previous"" button to go back and make further modification, otherwise, click the ""Next"" button to proceed.</h3>")

        ' Display the generated HTML in the corresponding DIV tag
        Me.correctedAddressInformation.InnerHtml = html.ToString()

        ' Store the WSAddress CORRECT response object in Session
        Me.Session(CORRECT_ADDR_RESPONSE_SESSION_KEY) = wsaddressResponse

        ' Set the current container name for UI purpose
        Me.containerSelected.Value = "correctedAddressContainer"
    End Sub

    ''' <summary>
    ''' Helper method to display the UI showing the end-user their valid Canada Post Address 
    ''' base on their previous inputs
    ''' </summary>
    ''' <param name="wsaddressResponse"></param>
    ''' <remarks></remarks>
    Private Sub NavigateToValidAddressPage(ByVal wsaddressResponse As WSAddress.WSAddressResponse)
        Dim outputParameters As WSAddress.WSAddressParameters = wsaddressResponse.AllResults.Item(0).Item(0).OutputParameters

        ' Display the Valid Address in a HTML table
        Me.validAddressMessage.InnerHtml = "<h3>Below is your Valid Canada Post address, this concludes the sample WSAddress application.</h3>" +
                                           "<table style='border:1px solid black; width:600px; border-collapse:collapse;'>" +
                                           "<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Address Line</td><td style='border:1px solid black; border-collapse:collapse;'>" +
                                           outputParameters("AddressLine") + "</td></tr>" +
                                           "<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>City</td><td style='border:1px solid black; border-collapse:collapse;'>" +
                                           outputParameters("City").ToString() + "</td></tr>" +
                                           "<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Province</td><td style='border:1px solid black; border-collapse:collapse;'>" +
                                           outputParameters("Province").ToString() + "</td></tr>" +
                                           "<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Postal Code</td><td style='border:1px solid black; border-collapse:collapse;'>" +
                                           outputParameters("PostalCode") + "</td></tr>" +
                                           "<tr><td style='border:1px solid black; width:200px; border-collapse:collapse; background-color: #C0C0C0;'>Country</td><td style='border:1px solid black; border-collapse:collapse;'>" +
                                           WSADDRESS_COUNTRY_CODE + "</td></tr></table>"

        ' Set the display container for UI purpose
        Me.containerSelected.Value = "validAddressContainer"

        ' Disble the "NEXT" button as this will be the last page of the entire collection process
        Me.btnNext.Enabled = False
    End Sub

#End Region

#Region "Address Line Composing Handlers"

    ''' <summary>
    ''' Compose the Address Line information for an Urban Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ComposeUrbanAddressAddressLine(ByVal addrMatchSelected As WSAddress.WSAddressMatch) As String
        ' <Street_Number>[SP]<Suffix*>[SP]<Street_Name>[SP]<Suite_Value*>
        ' * are optional value
        Return String.Format(CultureInfo.InvariantCulture,
                             addrMatchSelected.AddressParameters("AddressLine"),
                             Me.txtUAStreetNumber.Text.Trim(),
                             GetSuffixDropDownValue(Me.cmbUASuffix.SelectedValue),
                             If(Me.cmbUAUnitType.SelectedValue.Equals("0"), String.Empty, " " & Me.cmbUAUnitType.SelectedValue),
                             If(Me.cmbUAUnitType.SelectedValue.Equals("0"), String.Empty, " " & Me.txtUAUnitNumber.Text.Trim()))
    End Function

    ''' <summary>
    ''' Compose the Address Line information for an Urban Route Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ComposeUrbanRouteAddressLine(ByVal addrMatchSelected As WSAddress.WSAddressMatch) As String
        ' <Street_Number>[SP]<Suffix*>[SP]<Street_Name>[SP]<Suite_Value*>[SP]<Route_Code>[SP]<Route_Number>
        ' * are optional value
        Return String.Format(CultureInfo.InvariantCulture,
                             addrMatchSelected.AddressParameters("AddressLine"),
                             Me.txtURStreetNumber.Text.Trim(),
                             GetSuffixDropDownValue(Me.cmbURSuffix.SelectedValue),
                             If(Me.cmbURUnitType.SelectedValue.Equals("0"), String.Empty, " " & Me.cmbURUnitType.SelectedValue),
                             If(Me.cmbURUnitType.SelectedValue.Equals("0"), String.Empty, " " & Me.txtURUnitNumber.Text.Trim()))
    End Function

    ''' <summary>
    ''' Compose the Address Line information for a Rural General Delivery Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ComposeRuralGeneralDeliveryAddressLine(ByVal addrMatchSelected As WSAddress.WSAddressMatch) As String
        ' <PR/GD>[SP]<GD_Description>[SP]<GD_Qualifier_Name>
        Return addrMatchSelected.AddressParameters("AddressLine")
    End Function

    ''' <summary>
    ''' Compose the Address Line information for a Rural Lock Box Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ComposeRuralLockBoxAddressLine(ByVal addrMatchSelected As WSAddress.WSAddressMatch) As String

        Return String.Format(CultureInfo.InvariantCulture,
                            addrMatchSelected.AddressParameters("AddressLine"),
                             Me.txtRLBBoxNumber.Text.Trim(),
                             String.Empty, String.Empty, String.Empty)

    End Function

    ''' <summary>
    ''' Compose the Address Line information for a Rural Route Address
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ComposeRuralRouteAddressLine(ByVal addrMatchSelected As WSAddress.WSAddressMatch) As String

        Return addrMatchSelected.AddressParameters("AddressLine")

    End Function

#End Region

#Region "Search Action"

    ''' <summary>
    ''' Postal Code search using Street Perfect
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SearchByPostalCode() As Boolean
        Try
            ' Call the HandleSearchAction method to initialize the WSAddressTransaction object
            HandleSearchAction()

            ' Execute the WSAddressTransaction object
            Dim wsaddressResponse As WSAddress.WSAddressResponse = WSAddressTransaction.Execute()

            ' Call the HandleResponse method to handle the response got back from the execute call
            If wsaddressResponse.Validate(True) = True Then
                Return HandleSearchResponse(wsaddressResponse)
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' WSAddress SEARCH action handler method
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HandleSearchAction()
        ' Initialize the WSAddress Transaction object
        WSAddressTransaction = GetTransaction()

        ' Initialize Request Input Parameters
        With WSAddressTransaction.AllRequests.AddRequests().AddRequest()
            .Name = WSADDRESS_SEARCH_ACTION_CODE

            With .InputParameters
                .AddParameter("Language", Me.cmdLanguage.SelectedValue)
                .AddParameter("PostalCode", Me.txtPostalCode.Text.Trim)
                .AddParameter("ReturnAddressLine", "True")
            End With
        End With
    End Sub

    ''' <summary>
    ''' WSAddress SEARCH action response handler method
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HandleSearchResponse(ByVal wsaddressResponse As WSAddress.WSAddressResponse) As Boolean
        Dim outputParameters As WSAddress.WSAddressParameters
        Dim addrMatches As WSAddress.WSAddressMatches

        ' The SEARCH action returns the Output Parameters and the applicable Address Matches
        With wsaddressResponse.AllResults.Item(0).Item(0)
            outputParameters = .OutputParameters
            addrMatches = .AddressMatches
        End With

        If (Not outputParameters.Item("StatusCode").Trim().Equals(WSADDRESS_SEARCH_RECORDS_FOUND_STATUS)) Then
            ' Return False if the expected StatusCode value was NOT returned by Street Perfect
            Return False
        End If

        Dim html As StringBuilder = New StringBuilder()

        ' Display the # of address match found in a single line
        html.Append("<h3>" + addrMatches.Count.ToString(CultureInfo.InvariantCulture) + " match(es) found:</h3>")

        If (addrMatches.Count <> 0) Then
            html.Append("<table style='border:1px solid black; border-collapse:collapse;'><tr>" +
                           "<tr bgcolor='#C0C0C0'>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Sel.</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Directory Area Name</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>City</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Province</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Country</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Postal Code</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Address Type</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Street Name</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Street Type</th>" +
                           "<th style='border:1px solid black; border-collapse:collapse;'>Street Addr. Sequence</th>" +
                           "</tr>")

            ' Retrieve the Address Matches List from Session
            Dim addressMatches As Dictionary(Of String, WSAddress.WSAddressMatch) = DirectCast(Me.Session(ADDR_MATCHES_SESSION_KEY), Dictionary(Of String, WSAddress.WSAddressMatch))

            ' Loop through the list of AddressMatch objects
            Dim key As String = String.Empty
            For Each addrMatch As WSAddress.WSAddressMatch In addrMatches
                ' Each WSAddressMatch object in the loop would be stored in a Dictionary
                ' in Session for access later, the key to each entry in the Dictionary will 
                ' be indexed by an unique GUID
                key = Guid.NewGuid.ToString()

                ' Add the WSAddressMatch object to the Dictionary using the GUID just generated
                addressMatches.Add(key, addrMatch)

                ' Display the WSAddressMatch in a single table row, the value of the HTML radio button for 
                ' selection will be GUID that was just generated
                html.Append("<tr>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'><input type='radio' name='addrMatchSel' value='" + key + "' /></td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("DirectoryAreaName") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("City") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Province") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("Country") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("PostalCode") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressType.ToString() + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetName") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetType") + "</td>" +
                               "<td style='border:1px solid black; border-collapse:collapse;'>" + addrMatch.AddressParameters("StreetAddressSequence") + "</td>" +
                               "</tr>")
            Next

            html.Append("</table>")
        End If

        Me.addressGrid.InnerHtml = html.ToString()

        Return True
    End Function

#End Region

#Region "Correct Street Perfect Action"

    ''' <summary>
    ''' Call Street Perfect to Correct the inputted address
    ''' </summary>
    ''' <param name="addrMatchSelected"></param>
    ''' <remarks></remarks>
    Private Sub CorrectAddress(ByVal addrMatchSelected As WSAddress.WSAddressMatch)
        Dim wsaddressResponse As WSAddress.WSAddressResponse = Nothing

        Try
            Dim addressLine As String = String.Empty

            ' Invoke the corresponding helper function to compose the Address Line 
            ' correctly depending on the selected address Address Type value
            Select Case addrMatchSelected.AddressType
                Case WSADDRESS_ADDR_TYPE_UA
                    addressLine = ComposeUrbanAddressAddressLine(addrMatchSelected)
                Case WSADDRESS_ADDR_TYPE_UR
                    addressLine = ComposeUrbanRouteAddressLine(addrMatchSelected)
                Case WSADDRESS_ADDR_TYPE_RGD
                    addressLine = ComposeRuralGeneralDeliveryAddressLine(addrMatchSelected)
                Case WSADDRESS_ADDR_TYPE_RLB
                    addressLine = ComposeRuralLockBoxAddressLine(addrMatchSelected)
                Case WSADDRESS_ADDR_TYPE_RR
                    addressLine = ComposeRuralRouteAddressLine(addrMatchSelected)
            End Select

            ' Call the HandleValidateAction method to initialize the WSAddressTransaction object
            HandleCorrectAction(addressLine,
                                addrMatchSelected.AddressParameters("City"),
                                addrMatchSelected.AddressParameters("PostalCode"),
                                addrMatchSelected.AddressParameters("Province"))

            ' Execute the WSAddressTransaction object
            wsaddressResponse = WSAddressTransaction.Execute()

            ' Retrieve the WSAddress call Status Code value
            Dim correctStatusCode As String = GetSPCorrectActionStatusCode(wsaddressResponse)

            If (correctStatusCode.Equals(WSADDRESS_VALIDATE_NOT_CORRECT_STATUS)) Then
                ' Display error message if the Address is NOT CORRECT
                AddValidationMessage("The address inputted is not correct!")
            End If

            If (Me.Page.IsValid) Then
                If (correctStatusCode.Equals(WSADDRESS_VALIDATE_VALID_STATUS)) Then
                    ' if this is a completely VALID address, navigate to the Valid Address page 
                    ' of this wizard UI, thus the process has completed
                    NavigateToValidAddressPage(wsaddressResponse)
                ElseIf (correctStatusCode.Equals(WSADDRESS_VALIDATE_CORRECTED_STATUS)) Then
                    ' if this is a CORRECTED address, navigate to the page where we will 
                    ' display to the user the corrected address and give the end-user the 
                    ' choice of how to proceed
                    NavigateToCorrectedAddressPage(wsaddressResponse)
                End If
            End If
        Catch ex As Exception
            AddValidationMessage(Server.HtmlEncode(ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' Initialize the WSAddress Transaction object and its input parameters appropriately
    ''' </summary>
    ''' <param name="addressLine"></param>
    ''' <param name="city"></param>
    ''' <param name="postalCode"></param>
    ''' <param name="province"></param>
    ''' <remarks></remarks>
    Private Sub HandleCorrectAction(ByVal addressLine As String, ByVal city As String, ByVal postalCode As String, ByVal province As String)
        ' Initialize the WSAddress Transaction object
        WSAddressTransaction = GetTransaction()

        ' Initialize Request Input Parameters
        With WSAddressTransaction.AllRequests.AddRequests().AddRequest()
            .Name = WSADDRESS_VALIDATE_ACTION_CODE

            With .InputParameters
                .AddParameter("Language", Me.cmdLanguage.SelectedValue)
                .AddParameter("AddressLine", addressLine)
                .AddParameter("City", city)
                .AddParameter("Province", province)
                .AddParameter("PostalCode", postalCode)
                .AddParameter("Country", WSADDRESS_COUNTRY_CODE)
                .AddParameter("FormatResult", "True")
            End With
        End With
    End Sub

#End Region

End Class