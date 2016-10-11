Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.Data.Sql
Imports System.Data.SqlClient

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class BankWS
    Inherits System.Web.Services.WebService

    <WebMethod()> Sub AddLoanApp(ByVal Loan As Loan)
        Dim retVal As Double
        Dim conn As SqlConnection = Nothing
        Try
            Dim config As System.Configuration.Configuration
            config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/LoanWS")
            Dim connString As System.Configuration.ConnectionStringSettings
            connString = config.ConnectionStrings.ConnectionStrings("LoanApp")

            conn = New SqlConnection(connString.ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = conn
            cmd.CommandType = Data.CommandType.StoredProcedure
            cmd.CommandText = "InsertLoanApp"

            Dim param1 As SqlParameter = New SqlParameter()
            param1.DbType = SqlDbType.VarChar
            param1.Direction = Data.ParameterDirection.Input
            param1.IsNullable = False
            param1.ParameterName = "@SSN"
            param1.Size = 50
            param1.Value = Loan.SSN
            cmd.Parameters.Add(param1)

            Dim param2 As SqlParameter = New SqlParameter()
            param2.DbType = SqlDbType.VarChar
            param2.Direction = Data.ParameterDirection.Input
            param2.IsNullable = False
            param2.ParameterName = "@FirstName"
            param2.Size = 50
            param2.Value = Loan.FirstName
            cmd.Parameters.Add(param2)

            Dim param3 As SqlParameter = New SqlParameter()
            param3.DbType = SqlDbType.VarChar
            param3.Direction = Data.ParameterDirection.Input
            param3.IsNullable = False
            param3.ParameterName = "@LastName"
            param3.Size = 50
            param3.Value = Loan.LastName
            cmd.Parameters.Add(param3)

            Dim param4 As SqlParameter = New SqlParameter()
            param4.DbType = SqlDbType.VarChar
            param4.Direction = Data.ParameterDirection.Input
            param4.IsNullable = False
            param4.ParameterName = "@Street"
            param4.Size = 50
            param4.Value = Loan.Street
            cmd.Parameters.Add(param4)

            Dim param5 As SqlParameter = New SqlParameter()
            param5.DbType = SqlDbType.VarChar
            param5.Direction = Data.ParameterDirection.Input
            param5.IsNullable = False
            param5.ParameterName = "@City"
            param5.Size = 50
            param5.Value = Loan.City
            cmd.Parameters.Add(param5)

            Dim param6 As SqlParameter = New SqlParameter()
            param6.DbType = SqlDbType.VarChar
            param6.Direction = Data.ParameterDirection.Input
            param6.IsNullable = False
            param6.ParameterName = "@State"
            param6.Size = 50
            param6.Value = Loan.State
            cmd.Parameters.Add(param6)


            Dim param7 As SqlParameter = New SqlParameter()
            param7.DbType = SqlDbType.VarChar
            param7.Direction = Data.ParameterDirection.Input
            param7.IsNullable = False
            param7.ParameterName = "@Zip"
            param7.Size = 50
            param7.Value = Loan.Zip
            cmd.Parameters.Add(param7)


            Dim param8 As SqlParameter = New SqlParameter()
            param8.DbType = SqlDbType.Float
            param8.Direction = Data.ParameterDirection.Input
            param8.IsNullable = False
            param8.ParameterName = "@Income"
            param8.Value = Loan.Income
            cmd.Parameters.Add(param8)

            Dim param9 As SqlParameter = New SqlParameter()
            param9.DbType = SqlDbType.Float
            param9.Direction = Data.ParameterDirection.Input
            param9.IsNullable = False
            param9.ParameterName = "@LoanAmt"
            param9.Value = Loan.LoanAmt
            cmd.Parameters.Add(param9)

            conn.Open()
            retVal = cmd.ExecuteScalar()
            conn.Close()

        Catch ex As Exception
            Throw (ex)
        Finally
            If (conn IsNot Nothing AndAlso conn.State <> Data.ConnectionState.Closed) Then
                conn.Close()
            End If
        End Try
    End Sub

End Class

