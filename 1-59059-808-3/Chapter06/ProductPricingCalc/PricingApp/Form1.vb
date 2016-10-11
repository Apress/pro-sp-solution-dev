Imports PricingApp.ExcelWebService
Imports System.Web.Services.Protocols

Public Class Form1

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Dim es As ExcelService = New ExcelService()
        Dim outStatus() As Status = Nothing
        Dim targetWorkbookPath As String = My.MySettings.Default.WorkbookPath
        Dim sessionId As String = String.Empty
        es.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            sessionId = es.OpenWorkbook(targetWorkbookPath, "en-US", "en-US", outStatus)
            es.SetCellA1(sessionId, String.Empty, "ProductCode", Me.txtProductCode.Text)
            es.SetCellA1(sessionId, String.Empty, "CustomerZipCode", Me.txtCustomerZipCode.Text)
            es.SetCellA1(sessionId, String.Empty, "OrderQuantity", Double.Parse(Me.txtQuantity.Text))
            Dim totalSales As String = es.GetCellA1(sessionId, String.Empty, "Total", True, outStatus)
            Dim discountedTotal As String = es.GetCellA1(sessionId, String.Empty, "DiscountedTotal", True, outStatus)
            Me.lblTotal.Text = totalSales
            Me.lblDiscountedTotal.Text = discountedTotal

        Catch ex As SoapException
            'would return InvalidSheetName or FileOpenNotFound
            MessageBox.Show(ex.SubCode.Code.Name)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If (sessionId <> String.Empty) Then es.CloseWorkbookAsync(sessionId)
        End Try
    End Sub
End Class
