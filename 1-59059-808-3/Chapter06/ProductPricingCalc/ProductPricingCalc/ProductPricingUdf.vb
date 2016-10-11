Imports Microsoft.Office.Excel.Server.Udf
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Data.Sql
Imports System.Data.SqlClient

<UdfClass()> _
<Guid(ProductPricingUdf.ClsId)> _
<ProgId(ProductPricingUdf.ProgId)> _
<ClassInterface(ClassInterfaceType.AutoDual)> _
<ComVisible(True)> _
Public Class ProductPricingUdf
    Public Const ClsId As String = "C1C9EC29-16CB-4b13-9698-15810ACD3389"
    Public Const ProgId As String = "ProductPricingCalc.ProductPricingUdf"

    <UdfMethod()> _
    Public Function GetProductBasePrice(ByVal productCode As String) As Double
        Dim retVal As Double
        Dim conn As SqlConnection = Nothing
        Try
            conn = New SqlConnection(My.MySettings.Default.ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GetProductBasePrice"
            Dim param As SqlParameter = New SqlParameter()
            param.DbType = SqlDbType.VarChar
            param.Direction = ParameterDirection.Input
            param.IsNullable = False
            param.ParameterName = "@ProductCode"
            param.Size = 50
            param.Value = productCode
            cmd.Parameters.Add(param)
            conn.Open()
            retVal = cmd.ExecuteScalar()
            conn.Close()
            Return retVal
        Catch ex As Exception
            Debug.Print(ex.Message)
            Throw (ex)
        Finally
            If (conn IsNot Nothing AndAlso conn.State <> ConnectionState.Closed) Then
                conn.Close()
            End If
        End Try
    End Function

    <UdfMethod()> _
    Public Function GetShipping(ByVal warehouseZipCode As String, ByVal customerZipCode As String, ByVal totalWeight As Double) As Double
        Dim retVal As Double = -1
        Try
            Dim service As ShippingService.Shipping = New ShippingService.Shipping()
            service.Url = My.MySettings.Default.ProductPricingCalc_ShippingService_Shipping
            retVal = service.CalcShipping(warehouseZipCode, customerZipCode, totalWeight)
            Return retVal
        Catch ex As Exception
            Debug.Print(ex.Message)
            Throw (ex)
        End Try
    End Function

    <UdfMethod()> _
    Public Function GetSalesTax(ByVal customerZipCode As String) As Double
        Dim retVal As Double = 0
        Select Case customerZipCode
            Case "02108"
                retVal = 0.05
            Case "32803"
                retVal = 0.06
            Case Else
                Throw New ApplicationException("We do not sell to customers in that zip code")
        End Select
        Return retVal
    End Function
    <ComRegisterFunction()> _
    Public Shared Sub RegistrationMethod(ByVal t As Type)
        If (GetType(ProductPricingUdf) IsNot t) Then
            Exit Sub
        End If
        Dim key As RegistryKey = Registry.ClassesRoot.CreateSubKey("CLSID\{" & ClsId & "}\Programmable")
        key.Close()
    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub UnregistrationMethod(ByVal t As Type)
        If (GetType(ProductPricingUdf) IsNot t) Then
            Exit Sub
        End If
        Registry.ClassesRoot.DeleteSubKey("CLSID\{" & ClsId & "}\Programmable")
    End Sub


End Class
