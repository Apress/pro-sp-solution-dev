<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLoanApp.aspx.vb" Inherits="ViewLoanApp_ViewLoanApp" EnableSessionState="True" %>

<%@ Register Assembly="Microsoft.Office.InfoPath.Server, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.Office.InfoPath.Server.Controls" TagPrefix="cc1" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <cc1:XmlFormView ID="XmlFormView1" runat="server" Height="250px" Width="100%" XsnLocation="http://portal.sample.com/FormServerTemplates/Loan%20Application.xsn" />
    
    </div>
    </form>
</body>
</html>
