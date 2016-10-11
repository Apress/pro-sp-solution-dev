Imports System.Runtime.InteropServices
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Windows.Forms
Imports System.Xml
Imports Microsoft.Vbe.Interop.Forms


<ComVisible(True), _
Guid("88F7BFBE-7666-4a0c-BCFD-2740E6625E04"), _
ProgId("CustomerOutlookAddin.FormRegionHelper"), _
ClassInterface(ClassInterfaceType.AutoDual)> _
Public Class FormRegionHelper
    Implements Microsoft.Office.Interop.Outlook.FormRegionStartup

    Private mFormRegion As Outlook.FormRegion
    Private mUserForm As UserForm
    Private WithEvents cmdRetrieve As Outlook.OlkCommandButton
    Private WithEvents cmdGo As Outlook.OlkCommandButton
    Private OlkCustomerID As Outlook.OlkTextBox
    Private frmProperties As Outlook.OlkTextBox
    Private OlkCompany As Outlook.OlkTextBox
    Private OlkStreet As Outlook.OlkTextBox
    Private OlkCity As Outlook.OlkTextBox
    Private OlkState As Outlook.OlkTextBox
    Private OlkZip As Outlook.OlkTextBox
    Private OlkWebPage As Outlook.OlkTextBox


    Public Sub BeforeFormRegionShow(ByVal FormRegion As Microsoft.Office.Interop.Outlook.FormRegion) Implements Microsoft.Office.Interop.Outlook._FormRegionStartup.BeforeFormRegionShow
        Me.mFormRegion = FormRegion
        Me.mUserForm = FormRegion.Form
        Try
            Dim s As String = String.Empty

            OlkCustomerID = mUserForm.Controls.Item("OlkCustomerID")
            cmdRetrieve = mUserForm.Controls.Item("cmdRetrieve")
            OlkCompany = mUserForm.Controls.Item("OlkCompany")
            OlkStreet = mUserForm.Controls.Item("OlkStreet")
            OlkCity = mUserForm.Controls.Item("OlkCity")
            OlkState = mUserForm.Controls.Item("OlkState")
            OlkZip = mUserForm.Controls.Item("OlkZip")
            OlkWebPage = mUserForm.Controls.Item("OlkWebPage")
            cmdGo = mUserForm.Controls.Item("cmdGo")


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Public Function GetFormRegionIcon(ByVal FormRegionName As String, ByVal LCID As Integer, ByVal Icon As Microsoft.Office.Interop.Outlook.OlFormRegionIcon) As Object Implements Microsoft.Office.Interop.Outlook._FormRegionStartup.GetFormRegionIcon
        Throw New Exception("The methid or operation is not implemented")
    End Function

    Public Function GetFormRegionManifest(ByVal FormRegionName As String, ByVal LCID As Integer) As Object Implements Microsoft.Office.Interop.Outlook._FormRegionStartup.GetFormRegionManifest
        Throw New Exception("The methid or operation is not implemented")
    End Function

    Public Function GetFormRegionStorage(ByVal FormRegionName As String, ByVal Item As Object, ByVal LCID As Integer, ByVal FormRegionMode As Microsoft.Office.Interop.Outlook.OlFormRegionMode, ByVal FormRegionSize As Microsoft.Office.Interop.Outlook.OlFormRegionSize) As Object Implements Microsoft.Office.Interop.Outlook._FormRegionStartup.GetFormRegionStorage
        Application.DoEvents()
        Select Case FormRegionName
            Case "CustomerContactFormRegion"
                Dim ofsBytes As Byte()
                ofsBytes = My.Resources.CustomerContactFormRegion
                Return ofsBytes
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub cmdRetrieve_Click() Handles cmdRetrieve.Click
        Dim service As BDCWebService.BDCService = New BDCWebService.BDCService()
        service.Credentials = System.Net.CredentialCache.DefaultCredentials
        AddHandler service.GetEntitySpecificFinderCompleted, AddressOf Me.GetEntitySpecificFinderCompleted
        service.GetEntitySpecificFinderAsync("CustomerApplication", "CustomerApplicationInstance", "Customer", Me.OlkCustomerID.Text)
    End Sub

    Private Sub cmdGo_Click() Handles cmdGo.Click
        System.Diagnostics.Process.Start(OlkWebPage.Text)
    End Sub

    Public Sub GetEntitySpecificFinderCompleted(ByVal sender As Object, ByVal e As BDCWebService.GetEntitySpecificFinderCompletedEventArgs)
        If (e.Error Is Nothing) Then
            Dim xmlResult As XmlNode = e.Result
            Dim companyName As String = String.Empty
            Dim street As String = String.Empty
            Dim city As String = String.Empty
            Dim zip As String = String.Empty
            Dim state As String = String.Empty
            Dim action As String = String.Empty
            companyName = xmlResult.SelectSingleNode("//CompanyName").InnerText
            street = xmlResult.SelectSingleNode("//Street").InnerText
            city = xmlResult.SelectSingleNode("//City").InnerText
            zip = xmlResult.SelectSingleNode("//Zip").InnerText
            state = xmlResult.SelectSingleNode("//State").InnerText
            action = xmlResult.SelectSingleNode("//Action").InnerText
            Me.OlkCompany.Text = companyName
            Me.OlkStreet.Text = street
            Me.OlkCity.Text = city
            Me.OlkState.Text = state
            Me.OlkZip.Text = zip
            Me.OlkWebPage.Text = action
        End If
    End Sub
End Class
