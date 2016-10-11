Module Module1

    Sub Main()
        Dim service As BDCWebService.BDCService = New BDCWebService.BDCService()
        service.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim doc As Xml.XmlNode = Nothing
        doc = service.GetEntitySpecificFinder("CustomerApplication", "CustomerApplicationInstance", "Customer", "1101")
        Console.WriteLine(doc.OuterXml)
        Console.ReadLine()
    End Sub

End Module
