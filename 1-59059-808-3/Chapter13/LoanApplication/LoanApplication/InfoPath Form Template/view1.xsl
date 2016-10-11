<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dfs="http://schemas.microsoft.com/office/infopath/2003/dataFormSolution" xmlns:tns="http://tempuri.org/" xmlns:s1="http://schemas.microsoft.com/office/infopath/2003/myXSD/2007-01-21T14:35:58" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2007-01-21T17:56:39" xmlns:xd="http://schemas.microsoft.com/office/infopath/2003" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:xdExtension="http://schemas.microsoft.com/office/infopath/2003/xslt/extension" xmlns:xdXDocument="http://schemas.microsoft.com/office/infopath/2003/xslt/xDocument" xmlns:xdSolution="http://schemas.microsoft.com/office/infopath/2003/xslt/solution" xmlns:xdFormatting="http://schemas.microsoft.com/office/infopath/2003/xslt/formatting" xmlns:xdImage="http://schemas.microsoft.com/office/infopath/2003/xslt/xImage" xmlns:xdUtil="http://schemas.microsoft.com/office/infopath/2003/xslt/Util" xmlns:xdMath="http://schemas.microsoft.com/office/infopath/2003/xslt/Math" xmlns:xdDate="http://schemas.microsoft.com/office/infopath/2003/xslt/Date" xmlns:sig="http://www.w3.org/2000/09/xmldsig#" xmlns:xdSignatureProperties="http://schemas.microsoft.com/office/infopath/2003/SignatureProperties" xmlns:ipApp="http://schemas.microsoft.com/office/infopath/2006/XPathExtension/ipApp" xmlns:xdEnvironment="http://schemas.microsoft.com/office/infopath/2006/xslt/environment" xmlns:xdUser="http://schemas.microsoft.com/office/infopath/2006/xslt/User">
	<xsl:output method="html" indent="no"/>
	<xsl:template match="my:Loan">
		<html>
			<head>
				<meta http-equiv="Content-Type" content="text/html"></meta>
				<style controlStyle="controlStyle">@media screen 			{ 			BODY{margin-left:21px;background-position:21px 0px;} 			} 		BODY{color:windowtext;background-color:window;layout-grid:none;} 		.xdListItem {display:inline-block;width:100%;vertical-align:text-top;} 		.xdListBox,.xdComboBox{margin:1px;} 		.xdInlinePicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) } 		.xdLinkedPicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) url(#default#urn::controls/Binder) } 		.xdSection{border:1pt solid #FFFFFF;margin:6px 0px 6px 0px;padding:1px 1px 1px 5px;} 		.xdRepeatingSection{border:1pt solid #FFFFFF;margin:6px 0px 6px 0px;padding:1px 1px 1px 5px;} 		.xdMultiSelectList{margin:1px;display:inline-block; border:1pt solid #dcdcdc; padding:1px 1px 1px 5px; text-indent:0; color:windowtext; background-color:window; overflow:auto; behavior: url(#default#DataBindingUI) url(#default#urn::controls/Binder) url(#default#MultiSelectHelper) url(#default#ScrollableRegion);} 		.xdMultiSelectListItem{display:block;white-space:nowrap}		.xdMultiSelectFillIn{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:left;}		.xdBehavior_Formatting {BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting);} 	 .xdBehavior_FormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting);} 	.xdExpressionBox{margin: 1px;padding:1px;word-wrap: break-word;text-overflow: ellipsis;overflow-x:hidden;}.xdBehavior_GhostedText,.xdBehavior_GhostedTextNoBUI{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#TextField) url(#default#GhostedText);}	.xdBehavior_GTFormatting{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_GTFormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_Boolean{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#BooleanHelper);}	.xdBehavior_Select{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#SelectHelper);}	.xdBehavior_ComboBox{BEHAVIOR: url(#default#ComboBox)} 	.xdBehavior_ComboBoxTextField{BEHAVIOR: url(#default#ComboBoxTextField);} 	.xdRepeatingTable{BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word;}.xdScrollableRegion{BEHAVIOR: url(#default#ScrollableRegion);} 		.xdLayoutRegion{display:inline-block;} 		.xdMaster{BEHAVIOR: url(#default#MasterHelper);} 		.xdActiveX{margin:1px; BEHAVIOR: url(#default#ActiveX);} 		.xdFileAttachment{display:inline-block;margin:1px;BEHAVIOR:url(#default#urn::xdFileAttachment);} 		.xdPageBreak{display: none;}BODY{margin-right:21px;} 		.xdTextBoxRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:right;word-wrap:normal;} 		.xdRichTextBoxRTL{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:right;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTTextRTL{height:100%;width:100%;margin-left:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButtonRTL{margin-right:-21px;height:18px;width:20px;behavior: url(#default#DTPicker);} 		.xdMultiSelectFillinRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:right;}.xdTextBox{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:left;word-wrap:normal;} 		.xdRichTextBox{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:left;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTPicker{;display:inline;margin:1px;margin-bottom: 2px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-indent:0} 		.xdDTText{height:100%;width:100%;margin-right:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButton{margin-left:-21px;height:18px;width:20px;behavior: url(#default#DTPicker);} 		.xdRepeatingTable TD {VERTICAL-ALIGN: top;}</style>
				<style tableEditor="TableStyleRulesID">TABLE.xdLayout TD {
	BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none
}
TABLE.msoUcTable TD {
	BORDER-RIGHT: 1pt solid; BORDER-TOP: 1pt solid; BORDER-LEFT: 1pt solid; BORDER-BOTTOM: 1pt solid
}
TABLE {
	BEHAVIOR: url (#default#urn::tables/NDTable)
}
</style>
				<style languageStyle="languageStyle">BODY {
	FONT-SIZE: 10pt; FONT-FAMILY: Verdana
}
TABLE {
	FONT-SIZE: 10pt; FONT-FAMILY: Verdana
}
SELECT {
	FONT-SIZE: 10pt; FONT-FAMILY: Verdana
}
.optionalPlaceholder {
	PADDING-LEFT: 20px; FONT-WEIGHT: normal; FONT-SIZE: xx-small; BEHAVIOR: url(#default#xOptional); COLOR: #333333; FONT-STYLE: normal; FONT-FAMILY: Verdana; TEXT-DECORATION: none
}
.langFont {
	FONT-FAMILY: Verdana
}
.defaultInDocUI {
	FONT-SIZE: xx-small; FONT-FAMILY: Verdana
}
.optionalPlaceholder {
	PADDING-RIGHT: 20px
}
</style>
				<style themeStyle="urn:office.microsoft.com:themeBlue">BODY {
	COLOR: black; BACKGROUND-COLOR: white
}
TABLE {
	BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; BORDER-COLLAPSE: collapse
}
TD {
	BORDER-LEFT-COLOR: #517dbf; BORDER-BOTTOM-COLOR: #517dbf; BORDER-TOP-COLOR: #517dbf; BORDER-RIGHT-COLOR: #517dbf
}
TH {
	BORDER-LEFT-COLOR: #517dbf; BORDER-BOTTOM-COLOR: #517dbf; COLOR: black; BORDER-TOP-COLOR: #517dbf; BACKGROUND-COLOR: #cbd8eb; BORDER-RIGHT-COLOR: #517dbf
}
.xdTableHeader {
	COLOR: black; BACKGROUND-COLOR: #ebf0f9
}
P {
	MARGIN-TOP: 0px
}
H1 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #1e3c7b
}
H2 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #1e3c7b
}
H3 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #1e3c7b
}
H4 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #1e3c7b
}
H5 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #517dbf
}
H6 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #ebf0f9
}
.primaryVeryDark {
	COLOR: #ebf0f9; BACKGROUND-COLOR: #1e3c7b
}
.primaryDark {
	COLOR: white; BACKGROUND-COLOR: #517dbf
}
.primaryMedium {
	COLOR: black; BACKGROUND-COLOR: #cbd8eb
}
.primaryLight {
	COLOR: black; BACKGROUND-COLOR: #ebf0f9
}
.accentDark {
	COLOR: white; BACKGROUND-COLOR: #517dbf
}
.accentLight {
	COLOR: black; BACKGROUND-COLOR: #ebf0f9
}
</style>
			</head>
			<body style="COLOR: #000000; BACKGROUND-COLOR: #ffffff">
				<div><xsl:apply-templates select="my:myFields" mode="_2"/>
				</div>
				<div>
					<table class="msoUcTable" style="TABLE-LAYOUT: fixed; WIDTH: 219px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word; BORDER-BOTTOM-STYLE: none" border="1">
						<colgroup>
							<col style="WIDTH: 74px"></col>
							<col style="WIDTH: 145px"></col>
						</colgroup>
						<tbody>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>SSN</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL2" xd:xctname="PlainText" xd:binding="my:SSN" style="WIDTH: 100%">
											<xsl:value-of select="my:SSN"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>First Name</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL3" xd:xctname="PlainText" xd:binding="my:FirstName" style="WIDTH: 100%">
											<xsl:value-of select="my:FirstName"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>Last Name</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL4" xd:xctname="PlainText" xd:binding="my:LastName" style="WIDTH: 100%">
											<xsl:value-of select="my:LastName"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>Street</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL5" xd:xctname="PlainText" xd:binding="my:Street" style="WIDTH: 100%">
											<xsl:value-of select="my:Street"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>City</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL6" xd:xctname="PlainText" xd:binding="my:City" style="WIDTH: 100%">
											<xsl:value-of select="my:City"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>State</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL7" xd:xctname="PlainText" xd:binding="my:State" style="WIDTH: 100%">
											<xsl:value-of select="my:State"/>
										</span>
									</div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>Zip</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox" hideFocus="1" title="" tabIndex="0" xd:CtrlId="CTRL8" xd:xctname="PlainText" xd:binding="my:Zip" style="WIDTH: 100%">
											<xsl:value-of select="my:Zip"/>
										</span>
									</div>
								</td>
							</tr>
							<tr style="MIN-HEIGHT: 24px">
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div>Income</div>
								</td>
								<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
									<div><span class="xdTextBox xdBehavior_Formatting" hideFocus="1" title="" contentEditable="true" tabIndex="0" xd:CtrlId="CTRL9" xd:xctname="PlainText" xd:binding="my:Income" xd:datafmt="&quot;number&quot;,&quot;numDigits:auto;negativeOrder:1;&quot;" xd:boundProp="xd:num" style="WIDTH: 100%">
											<xsl:attribute name="xd:num">
												<xsl:value-of select="my:Income"/>
											</xsl:attribute>
											<xsl:choose>
												<xsl:when test="function-available('xdFormatting:formatString')">
													<xsl:value-of select="xdFormatting:formatString(my:Income,&quot;number&quot;,&quot;numDigits:auto;negativeOrder:1;&quot;)"/>
												</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="my:Income"/>
												</xsl:otherwise>
											</xsl:choose>
										</span>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div>Loan Amt: <span class="xdTextBox xdBehavior_Formatting" hideFocus="1" title="" contentEditable="true" tabIndex="0" xd:CtrlId="CTRL14" xd:xctname="PlainText" xd:binding="my:LoanAmt" xd:datafmt="&quot;number&quot;,&quot;numDigits:auto;negativeOrder:1;&quot;" xd:boundProp="xd:num" style="WIDTH: 142px">
						<xsl:attribute name="xd:num">
							<xsl:value-of select="my:LoanAmt"/>
						</xsl:attribute>
						<xsl:choose>
							<xsl:when test="function-available('xdFormatting:formatString')">
								<xsl:value-of select="xdFormatting:formatString(my:LoanAmt,&quot;number&quot;,&quot;numDigits:auto;negativeOrder:1;&quot;)"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="my:LoanAmt"/>
							</xsl:otherwise>
						</xsl:choose>
					</span>
				</div>
				<div> </div>
				<div/>
				<div>
					<input class="langFont" title="" style="BEHAVIOR: url(#default#ActionButton)" type="button" value="Submit" xd:CtrlId="CTRL11_5" xd:xctname="Button" xd:action="submit" tabIndex="0"/>   <input class="langFont" title="" type="button" value="Managed" xd:CtrlId="btnManaged" xd:xctname="Button" tabIndex="0"/>
				</div>
				<div> </div>
			</body>
		</html>
	</xsl:template>
	<xsl:template match="my:myFields" mode="_2">
		<div class="xdSection xdRepeating" title="" style="MARGIN-BOTTOM: 6px; WIDTH: 651px; DIRECTION: ltr" align="left" xd:ictID="urn:schemas-microsoft-com:office:infopath:peoplesbankheader:-myxsd-2006-12-26t17-51-55" xd:ictVersion="1.0.0.2" xd:CtrlId="CTRL12_5" xd:xctname="Section" tabIndex="-1">
			<div> </div>
			<div>
				<table class="xdFormLayout xdLayout" style="TABLE-LAYOUT: fixed; WIDTH: 651px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word; BORDER-BOTTOM-STYLE: none" border="1">
					<colgroup>
						<col style="WIDTH: 651px"></col>
					</colgroup>
					<tbody vAlign="top">
						<tr class="primaryVeryDark">
							<td style="BORDER-TOP-STYLE: none; BORDER-BOTTOM: 5pt solid; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none">
								<div>
									<font size="4">Peoples Bank</font>
								</div>
							</td>
						</tr>
						<tr class="primarylight" style="MIN-HEIGHT: 0.31in">
							<td vAlign="top" style="BORDER-TOP: 5pt solid; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: left; BORDER-BOTTOM-STYLE: none">
								<div>
									<font size="2">1111 InfoPath Way, Redmond Washington</font>
								</div>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
			<div> </div>
		</div>
	</xsl:template>
</xsl:stylesheet>
