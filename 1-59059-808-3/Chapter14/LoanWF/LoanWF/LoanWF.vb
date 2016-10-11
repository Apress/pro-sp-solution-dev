Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.Workflow
Imports Microsoft.SharePoint.WorkflowActions
Imports Microsoft.Office.Workflow.Utility

'NOTE: When changing the namespace; please update XmlnsDefinitionAttribute in AssemblyInfo.vb
Public Class LoanWF
    Inherits SharePointSequentialWorkflowActivity
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public workflowProperties As SPWorkflowActivationProperties = New Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties

    Public dblLoanAmt As Double
    Public dblIncome As Double
    Public dblLoanRatio As Double
    Public strMailMessage As String
    Public taskStatus As String
    Public historyStatus As String

    Public taskIDField As System.Guid = Nothing
    Public taskPropertiesField As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public beforeProperties As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public afterProperties As SPWorkflowTaskProperties = New Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties
    Public loanWFProperties As New Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties


    Private Sub onWorkflowActivated1_Invoked(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        dblLoanAmt = Double.Parse(workflowProperties.Item("Loan Amt").ToString)
        dblIncome = Double.Parse(workflowProperties.Item("Income").ToString)
        dblLoanRatio = dblIncome / dblLoanAmt
    End Sub
    Private Sub codeApproved_ExecuteCode(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strMailMessage = "Your loan has been approved!"
        historyStatus = "Approved"
    End Sub

    Private Sub codeDeclined_ExecuteCode(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strMailMessage = "Your loan has been declined!"
        historyStatus = "Rejected"
    End Sub

    Private Sub createReviewTask_MethodInvoking(ByVal sender As System.Object, ByVal e As System.EventArgs)
        taskIDField = Guid.NewGuid()
        taskPropertiesField.AssignedTo = "sample\susiea"
        taskPropertiesField.TaskType = 0
        taskPropertiesField.Description = "Please approve or reject a loan for $" + workflowProperties.Item("Loan Amt").ToString
        taskPropertiesField.DueDate = DateTime.Today.AddDays(7)
        taskPropertiesField.Title = "Loan Approval for: " + workflowProperties.Item("Last Name").ToString + "-" + workflowProperties.Item("SSN").ToString
        taskPropertiesField.ExtendedProperties("SSN") = workflowProperties.Item("SSN")
        taskPropertiesField.ExtendedProperties("LoanAmt") = workflowProperties.Item("Loan Amt")
        taskPropertiesField.ExtendedProperties("Income") = workflowProperties.Item("Income")
        taskPropertiesField.ExtendedProperties("Name") = workflowProperties.Item("Last Name")

    End Sub

    Private Sub onTaskReviewChanged_Invoked(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.ExternalDataEventArgs)
        taskStatus = afterProperties.ExtendedProperties("TaskStatus").ToString()
    End Sub

    Private Sub completeReivewTask_MethodInvoking(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If taskStatus = "Approved" Then
            strMailMessage = "Your loan has been approved!"
            historyStatus = "Approved"
        Else
            strMailMessage = "Your loan has been rejected!"
            historyStatus = "Rejected"
        End If
    End Sub
    Public loanapp As BankWS.Loan

    Private Sub invokeWebServiceActivity1_Invoking(ByVal sender As System.Object, ByVal e As System.Workflow.Activities.InvokeWebServiceEventArgs)
        loanapp = New BankWS.Loan
        loanapp.SSN = workflowProperties.Item("SSN").ToString
        loanapp.LastName = workflowProperties.Item("Last Name")
        loanapp.LoanAmt = workflowProperties.Item("Loan Amt")
        loanapp.Income = workflowProperties.Item("Income")
    End Sub


End Class
