<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoanWF

    'NOTE: The following procedure is required by the Workflow Designer
    'It can be modified using the Workflow Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Private Sub InitializeComponent()
        Me.CanModifyActivities = True
        Dim activitybind1 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind2 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim workflowparameterbinding1 As System.Workflow.ComponentModel.WorkflowParameterBinding = New System.Workflow.ComponentModel.WorkflowParameterBinding
        Dim correlationtoken1 As System.Workflow.Runtime.CorrelationToken = New System.Workflow.Runtime.CorrelationToken
        Dim activitybind3 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind4 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind5 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind6 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind7 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind8 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim ruleconditionreference1 As System.Workflow.Activities.Rules.RuleConditionReference = New System.Workflow.Activities.Rules.RuleConditionReference
        Dim ruleconditionreference2 As System.Workflow.Activities.Rules.RuleConditionReference = New System.Workflow.Activities.Rules.RuleConditionReference
        Dim ruleconditionreference3 As System.Workflow.Activities.Rules.RuleConditionReference = New System.Workflow.Activities.Rules.RuleConditionReference
        Dim activitybind9 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind10 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim activitybind11 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Dim correlationtoken2 As System.Workflow.Runtime.CorrelationToken = New System.Workflow.Runtime.CorrelationToken
        Dim activitybind12 As System.Workflow.ComponentModel.ActivityBind = New System.Workflow.ComponentModel.ActivityBind
        Me.logToHistoryListActivity2 = New Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity
        Me.invokeWebServiceActivity1 = New System.Workflow.Activities.InvokeWebServiceActivity
        Me.codeDeclined = New System.Workflow.Activities.CodeActivity
        Me.codeApproved = New System.Workflow.Activities.CodeActivity
        Me.completeReviewTask = New Microsoft.SharePoint.WorkflowActions.CompleteTask
        Me.onTaskReviewChanged = New Microsoft.SharePoint.WorkflowActions.OnTaskChanged
        Me.createReviewTask = New Microsoft.SharePoint.WorkflowActions.CreateTask
        Me.faultHandlerActivity1 = New System.Workflow.ComponentModel.FaultHandlerActivity
        Me.ifElseBranchActivity5 = New System.Workflow.Activities.IfElseBranchActivity
        Me.ifElseBranchActivity4 = New System.Workflow.Activities.IfElseBranchActivity
        Me.ifElseBranchActivity3 = New System.Workflow.Activities.IfElseBranchActivity
        Me.ifElseApprove = New System.Workflow.Activities.IfElseBranchActivity
        Me.ifElseReview = New System.Workflow.Activities.IfElseBranchActivity
        Me.cancellationHandlerActivity1 = New System.Workflow.ComponentModel.CancellationHandlerActivity
        Me.faultHandlersActivity1 = New System.Workflow.ComponentModel.FaultHandlersActivity
        Me.logToHistoryListActivity1 = New Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity
        Me.sendEmail1 = New Microsoft.SharePoint.WorkflowActions.SendEmail
        Me.ifElseActivity2 = New System.Workflow.Activities.IfElseActivity
        Me.ifElseActivity1 = New System.Workflow.Activities.IfElseActivity
        Me.onWorkflowActivated1 = New Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated
        '
        'logToHistoryListActivity2
        '
        Me.logToHistoryListActivity2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808")
        Me.logToHistoryListActivity2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment
        activitybind1.Name = "faultHandlerActivity1"
        activitybind1.Path = "Fault.Message"
        Me.logToHistoryListActivity2.HistoryOutcome = "Error"
        Me.logToHistoryListActivity2.Name = "logToHistoryListActivity2"
        Me.logToHistoryListActivity2.OtherData = ""
        Me.logToHistoryListActivity2.UserId = -1
        Me.logToHistoryListActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, CType(activitybind1, System.Workflow.ComponentModel.ActivityBind))
        '
        'invokeWebServiceActivity1
        '
        Me.invokeWebServiceActivity1.MethodName = "AddLoanApp"
        Me.invokeWebServiceActivity1.Name = "invokeWebServiceActivity1"
        activitybind2.Name = "LoanWF"
        activitybind2.Path = "loanapp"
        workflowparameterbinding1.ParameterName = "Loan"
        workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, CType(activitybind2, System.Workflow.ComponentModel.ActivityBind))
        Me.invokeWebServiceActivity1.ParameterBindings.Add(workflowparameterbinding1)
        Me.invokeWebServiceActivity1.ProxyClass = GetType(LoanWF.BankWS.BankWS)
        AddHandler Me.invokeWebServiceActivity1.Invoking, AddressOf Me.invokeWebServiceActivity1_Invoking
        '
        'codeDeclined
        '
        Me.codeDeclined.Name = "codeDeclined"
        AddHandler Me.codeDeclined.ExecuteCode, AddressOf Me.codeDeclined_ExecuteCode
        '
        'codeApproved
        '
        Me.codeApproved.Name = "codeApproved"
        AddHandler Me.codeApproved.ExecuteCode, AddressOf Me.codeApproved_ExecuteCode
        '
        'completeReviewTask
        '
        correlationtoken1.Name = "taskToken"
        correlationtoken1.OwnerActivityName = "LoanWF"
        Me.completeReviewTask.CorrelationToken = correlationtoken1
        Me.completeReviewTask.Name = "completeReviewTask"
        activitybind3.Name = "LoanWF"
        activitybind3.Path = "taskIDField"
        Me.completeReviewTask.TaskOutcome = Nothing
        AddHandler Me.completeReviewTask.MethodInvoking, AddressOf Me.completeReivewTask_MethodInvoking
        Me.completeReviewTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, CType(activitybind3, System.Workflow.ComponentModel.ActivityBind))
        '
        'onTaskReviewChanged
        '
        activitybind4.Name = "LoanWF"
        activitybind4.Path = "afterProperties"
        activitybind5.Name = "LoanWF"
        activitybind5.Path = "beforeProperties"
        Me.onTaskReviewChanged.CorrelationToken = correlationtoken1
        Me.onTaskReviewChanged.Executor = Nothing
        Me.onTaskReviewChanged.Name = "onTaskReviewChanged"
        activitybind6.Name = "LoanWF"
        activitybind6.Path = "taskIDField"
        AddHandler Me.onTaskReviewChanged.Invoked, AddressOf Me.onTaskReviewChanged_Invoked
        Me.onTaskReviewChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, CType(activitybind6, System.Workflow.ComponentModel.ActivityBind))
        Me.onTaskReviewChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, CType(activitybind4, System.Workflow.ComponentModel.ActivityBind))
        Me.onTaskReviewChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, CType(activitybind5, System.Workflow.ComponentModel.ActivityBind))
        '
        'createReviewTask
        '
        Me.createReviewTask.CorrelationToken = correlationtoken1
        Me.createReviewTask.ListItemId = -1
        Me.createReviewTask.Name = "createReviewTask"
        Me.createReviewTask.SpecialPermissions = Nothing
        activitybind7.Name = "LoanWF"
        activitybind7.Path = "taskIDField"
        activitybind8.Name = "LoanWF"
        activitybind8.Path = "taskPropertiesField"
        AddHandler Me.createReviewTask.MethodInvoking, AddressOf Me.createReviewTask_MethodInvoking
        Me.createReviewTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskIdProperty, CType(activitybind7, System.Workflow.ComponentModel.ActivityBind))
        Me.createReviewTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskPropertiesProperty, CType(activitybind8, System.Workflow.ComponentModel.ActivityBind))
        '
        'faultHandlerActivity1
        '
        Me.faultHandlerActivity1.Activities.Add(Me.logToHistoryListActivity2)
        Me.faultHandlerActivity1.FaultType = GetType(System.Exception)
        Me.faultHandlerActivity1.Name = "faultHandlerActivity1"
        '
        'ifElseBranchActivity5
        '
        Me.ifElseBranchActivity5.Name = "ifElseBranchActivity5"
        '
        'ifElseBranchActivity4
        '
        Me.ifElseBranchActivity4.Activities.Add(Me.invokeWebServiceActivity1)
        ruleconditionreference1.ConditionName = "IfApproved"
        Me.ifElseBranchActivity4.Condition = ruleconditionreference1
        Me.ifElseBranchActivity4.Name = "ifElseBranchActivity4"
        '
        'ifElseBranchActivity3
        '
        Me.ifElseBranchActivity3.Activities.Add(Me.codeDeclined)
        Me.ifElseBranchActivity3.Name = "ifElseBranchActivity3"
        '
        'ifElseApprove
        '
        Me.ifElseApprove.Activities.Add(Me.codeApproved)
        ruleconditionreference2.ConditionName = "Approve"
        Me.ifElseApprove.Condition = ruleconditionreference2
        Me.ifElseApprove.Name = "ifElseApprove"
        '
        'ifElseReview
        '
        Me.ifElseReview.Activities.Add(Me.createReviewTask)
        Me.ifElseReview.Activities.Add(Me.onTaskReviewChanged)
        Me.ifElseReview.Activities.Add(Me.completeReviewTask)
        ruleconditionreference3.ConditionName = "Review"
        Me.ifElseReview.Condition = ruleconditionreference3
        Me.ifElseReview.Name = "ifElseReview"
        '
        'cancellationHandlerActivity1
        '
        Me.cancellationHandlerActivity1.Name = "cancellationHandlerActivity1"
        '
        'faultHandlersActivity1
        '
        Me.faultHandlersActivity1.Activities.Add(Me.faultHandlerActivity1)
        Me.faultHandlersActivity1.Name = "faultHandlersActivity1"
        '
        'logToHistoryListActivity1
        '
        Me.logToHistoryListActivity1.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808")
        Me.logToHistoryListActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment
        Me.logToHistoryListActivity1.HistoryDescription = "Loan Approval Completed"
        activitybind9.Name = "LoanWF"
        activitybind9.Path = "historyStatus"
        Me.logToHistoryListActivity1.Name = "logToHistoryListActivity1"
        Me.logToHistoryListActivity1.OtherData = ""
        activitybind10.Name = "LoanWF"
        activitybind10.Path = "workflowProperties.OriginatorUser.ID"
        Me.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, CType(activitybind9, System.Workflow.ComponentModel.ActivityBind))
        Me.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, CType(activitybind10, System.Workflow.ComponentModel.ActivityBind))
        '
        'sendEmail1
        '
        Me.sendEmail1.BCC = Nothing
        activitybind11.Name = "LoanWF"
        activitybind11.Path = "strMailMessage"
        Me.sendEmail1.CC = Nothing
        correlationtoken2.Name = "workflowToken"
        correlationtoken2.OwnerActivityName = "LoanWF"
        Me.sendEmail1.CorrelationToken = correlationtoken2
        Me.sendEmail1.From = Nothing
        Me.sendEmail1.Headers = Nothing
        Me.sendEmail1.IncludeStatus = False
        Me.sendEmail1.Name = "sendEmail1"
        Me.sendEmail1.Subject = "Loan Application"
        Me.sendEmail1.To = "susiea@sample.com"
        Me.sendEmail1.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BodyProperty, CType(activitybind11, System.Workflow.ComponentModel.ActivityBind))
        '
        'ifElseActivity2
        '
        Me.ifElseActivity2.Activities.Add(Me.ifElseBranchActivity4)
        Me.ifElseActivity2.Activities.Add(Me.ifElseBranchActivity5)
        Me.ifElseActivity2.Name = "ifElseActivity2"
        '
        'ifElseActivity1
        '
        Me.ifElseActivity1.Activities.Add(Me.ifElseReview)
        Me.ifElseActivity1.Activities.Add(Me.ifElseApprove)
        Me.ifElseActivity1.Activities.Add(Me.ifElseBranchActivity3)
        Me.ifElseActivity1.Name = "ifElseActivity1"
        '
        'onWorkflowActivated1
        '
        Me.onWorkflowActivated1.CorrelationToken = correlationtoken2
        Me.onWorkflowActivated1.EventName = "OnWorkflowActivated"
        Me.onWorkflowActivated1.Name = "onWorkflowActivated1"
        activitybind12.Name = "LoanWF"
        activitybind12.Path = "workflowProperties"
        AddHandler Me.onWorkflowActivated1.Invoked, AddressOf Me.onWorkflowActivated1_Invoked
        Me.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, CType(activitybind12, System.Workflow.ComponentModel.ActivityBind))
        '
        'LoanWF
        '
        Me.Activities.Add(Me.onWorkflowActivated1)
        Me.Activities.Add(Me.ifElseActivity1)
        Me.Activities.Add(Me.ifElseActivity2)
        Me.Activities.Add(Me.sendEmail1)
        Me.Activities.Add(Me.logToHistoryListActivity1)
        Me.Activities.Add(Me.faultHandlersActivity1)
        Me.Activities.Add(Me.cancellationHandlerActivity1)
        Me.Name = "LoanWF"
        Me.CanModifyActivities = False

    End Sub
    Private cancellationHandlerActivity1 As System.Workflow.ComponentModel.CancellationHandlerActivity
    Private faultHandlerActivity1 As System.Workflow.ComponentModel.FaultHandlerActivity
    Private faultHandlersActivity1 As System.Workflow.ComponentModel.FaultHandlersActivity
    Private logToHistoryListActivity2 As Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity
    Private invokeWebServiceActivity1 As System.Workflow.Activities.InvokeWebServiceActivity
    Private codeDeclined As System.Workflow.Activities.CodeActivity
    Private codeApproved As System.Workflow.Activities.CodeActivity
    Private completeReviewTask As Microsoft.SharePoint.WorkflowActions.CompleteTask
    Private onTaskReviewChanged As Microsoft.SharePoint.WorkflowActions.OnTaskChanged
    Private createReviewTask As Microsoft.SharePoint.WorkflowActions.CreateTask
    Private ifElseBranchActivity5 As System.Workflow.Activities.IfElseBranchActivity
    Private ifElseBranchActivity4 As System.Workflow.Activities.IfElseBranchActivity
    Private ifElseBranchActivity3 As System.Workflow.Activities.IfElseBranchActivity
    Private ifElseApprove As System.Workflow.Activities.IfElseBranchActivity
    Private ifElseReview As System.Workflow.Activities.IfElseBranchActivity
    Private logToHistoryListActivity1 As Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity
    Private sendEmail1 As Microsoft.SharePoint.WorkflowActions.SendEmail
    Private ifElseActivity2 As System.Workflow.Activities.IfElseActivity
    Private ifElseActivity1 As System.Workflow.Activities.IfElseActivity
    Private onWorkflowActivated1 As Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated

End Class
