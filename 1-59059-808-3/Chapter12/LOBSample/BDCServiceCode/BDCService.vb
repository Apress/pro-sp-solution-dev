Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports Microsoft.Office.Server.ApplicationRegistry
Imports Microsoft.Office.Server.ApplicationRegistry.Runtime
Imports Microsoft.Office.Server.ApplicationRegistry.Infrastructure
Imports Microsoft.Office.Server.ApplicationRegistry.MetadataModel




<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class BDCService
     Inherits System.Web.Services.WebService

    
    <WebMethod()> _
    Public Function GetEntitySpecificFinder(ByVal ApplicationName As String, _
    ByVal ApplicationInstance As String, ByVal EntityName As String, _
    ByVal Parameter As String) As System.Xml.XmlDocument
        Dim returnDocument As XmlDocument = New XmlDocument()
        Dim root As XmlNode = returnDocument.CreateElement("root")
        returnDocument.AppendChild(root)
        Dim systemObj As LobSystem = ApplicationRegistry.GetLobSystems()(ApplicationName)
        Dim systemInstance As LobSystemInstance = systemObj.GetLobSystemInstances()(ApplicationInstance)
        Dim requestedEntity As Entity = systemObj.GetEntities()(EntityName)
        If requestedEntity.HasSpecificFinder() Then
            Dim instance As IEntityInstance = Nothing
            Dim paramTypeString = requestedEntity.GetSpecificFinderMethodInstance().GetMethod().GetInputParameters()(0).GetRootTypeDescriptor().TypeName
            Select Case paramTypeString
                Case "System.Int32"
                    instance = requestedEntity.FindSpecific(Integer.Parse(Parameter), systemInstance)
                Case "System.String"
                    instance = requestedEntity.FindSpecific(Parameter, systemInstance)
                Case "System.Guid"
                    instance = requestedEntity.FindSpecific(New Guid(Parameter), systemInstance)
            End Select
            Dim f As Field = Nothing
            For Each f In requestedEntity.GetSpecificFinderView().Fields
                Dim fieldNode As XmlNode = returnDocument.CreateElement(XmlConvert.EncodeName(f.Name))
                Dim txtNode As XmlNode = returnDocument.CreateTextNode(instance(f).ToString())
                fieldNode.AppendChild(txtNode)
                root.AppendChild(fieldNode)
            Next
            Dim actionNode As XmlNode = returnDocument.CreateElement("Action")
            Dim url As String = instance.GetActionUrl(requestedEntity.GetDefaultAction())
            Dim actionUrl As XmlNode = returnDocument.CreateTextNode(url)
            actionNode.AppendChild(actionUrl)
            root.AppendChild(actionNode)
        End If
        Return returnDocument
    End Function
End Class
