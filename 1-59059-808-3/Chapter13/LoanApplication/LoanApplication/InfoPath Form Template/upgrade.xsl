<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dfs="http://schemas.microsoft.com/office/infopath/2003/dataFormSolution" xmlns:tns="http://tempuri.org/" xmlns:s1="http://schemas.microsoft.com/office/infopath/2003/myXSD/2007-01-21T14:35:58" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2007-01-21T17:56:39" xmlns:xd="http://schemas.microsoft.com/office/infopath/2003" version="1.0">
	<xsl:output encoding="UTF-8" method="xml"/>
	<xsl:template match="/">
		<xsl:copy-of select="processing-instruction() | comment()"/>
		<xsl:choose>
			<xsl:when test="my:Loan">
				<xsl:apply-templates select="my:Loan" mode="_0"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="var">
					<xsl:element name="my:Loan"/>
				</xsl:variable>
				<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_0"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="my:Loan" mode="_0">
		<xsl:copy>
			<xsl:element name="my:SSN">
				<xsl:copy-of select="my:SSN/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:FirstName">
				<xsl:copy-of select="my:FirstName/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:LastName">
				<xsl:copy-of select="my:LastName/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:Street">
				<xsl:copy-of select="my:Street/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:City">
				<xsl:copy-of select="my:City/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:State">
				<xsl:copy-of select="my:State/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:Zip">
				<xsl:copy-of select="my:Zip/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:Income">
				<xsl:choose>
					<xsl:when test="my:Income/text()[1]">
						<xsl:copy-of select="my:Income/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="xsi:nil">true</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="my:LoanAmt">
				<xsl:choose>
					<xsl:when test="my:LoanAmt/text()[1]">
						<xsl:copy-of select="my:LoanAmt/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="xsi:nil">true</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="my:myFields"/>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>