<%@ Page Language="C#" Inherits="System.Web.UI.Page"%>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint.Utilities" %> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<% Response.ContentType = "text/xml"; %>

<wsdl:definitions xmlns:soap="http://schemas.xmlsoap.org/wsdl/soap/" xmlns:tm="http://microsoft.com/wsdl/mime/textMatching/" xmlns:soapenc="http://schemas.xmlsoap.org/soap/encoding/" xmlns:mime="http://schemas.xmlsoap.org/wsdl/mime/" xmlns:tns="http://tempuri.org/" xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://schemas.xmlsoap.org/wsdl/soap12/" xmlns:http="http://schemas.xmlsoap.org/wsdl/http/" targetNamespace="http://tempuri.org/" xmlns:wsdl="http://schemas.xmlsoap.org/wsdl/">
  <wsdl:types>
    <s:schema elementFormDefault="qualified" targetNamespace="http://tempuri.org/">
      <s:element name="GetEntitySpecificFinder">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="ApplicationName" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="ApplicationInstance" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="EntityName" type="s:string" />
            <s:element minOccurs="0" maxOccurs="1" name="Parameter" type="s:string" />
          </s:sequence>
        </s:complexType>
      </s:element>
      <s:element name="GetEntitySpecificFinderResponse">
        <s:complexType>
          <s:sequence>
            <s:element minOccurs="0" maxOccurs="1" name="GetEntitySpecificFinderResult">
              <s:complexType mixed="true">
                <s:sequence>
                  <s:any />
                </s:sequence>
              </s:complexType>
            </s:element>
          </s:sequence>
        </s:complexType>
      </s:element>
    </s:schema>
  </wsdl:types>
  <wsdl:message name="GetEntitySpecificFinderSoapIn">
    <wsdl:part name="parameters" element="tns:GetEntitySpecificFinder" />
  </wsdl:message>
  <wsdl:message name="GetEntitySpecificFinderSoapOut">
    <wsdl:part name="parameters" element="tns:GetEntitySpecificFinderResponse" />
  </wsdl:message>
  <wsdl:portType name="BDCServiceSoap">
    <wsdl:operation name="GetEntitySpecificFinder">
      <wsdl:input message="tns:GetEntitySpecificFinderSoapIn" />
      <wsdl:output message="tns:GetEntitySpecificFinderSoapOut" />
    </wsdl:operation>
  </wsdl:portType>
  <wsdl:binding name="BDCServiceSoap" type="tns:BDCServiceSoap">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="GetEntitySpecificFinder">
      <soap:operation soapAction="http://tempuri.org/GetEntitySpecificFinder" style="document" />
      <wsdl:input>
        <soap:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:binding name="BDCServiceSoap12" type="tns:BDCServiceSoap">
    <soap12:binding transport="http://schemas.xmlsoap.org/soap/http" />
    <wsdl:operation name="GetEntitySpecificFinder">
      <soap12:operation soapAction="http://tempuri.org/GetEntitySpecificFinder" style="document" />
      <wsdl:input>
        <soap12:body use="literal" />
      </wsdl:input>
      <wsdl:output>
        <soap12:body use="literal" />
      </wsdl:output>
    </wsdl:operation>
  </wsdl:binding>
  <wsdl:service name="BDCService">
    <wsdl:port name="BDCServiceSoap" binding="tns:BDCServiceSoap">
      <soap:address location=<% SPEncode.WriteHtmlEncodeWithQuote(Response, SPWeb.OriginalBaseUrl(Request), '"'); %> />
    </wsdl:port>
    <wsdl:port name="BDCServiceSoap12" binding="tns:BDCServiceSoap12">
      <soap12:address location=<% SPEncode.WriteHtmlEncodeWithQuote(Response, SPWeb.OriginalBaseUrl(Request), '"'); %> />
    </wsdl:port>
  </wsdl:service>
</wsdl:definitions>