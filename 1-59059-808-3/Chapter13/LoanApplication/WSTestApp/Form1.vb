Public Class Form1

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ws As New localhost.BankWS

        Dim Loan As New localhost.Loan

        Loan.SSN = TextBox1.Text
        Loan.FirstName = TextBox2.Text
        Loan.LastName = TextBox3.Text
        Loan.Street = TextBox4.Text
        Loan.City = TextBox5.Text
        Loan.State = TextBox6.Text
        Loan.Zip = TextBox7.Text
        Loan.Income = TextBox8.Text

        ws.AddLoanApp(Loan)
    End Sub
End Class
