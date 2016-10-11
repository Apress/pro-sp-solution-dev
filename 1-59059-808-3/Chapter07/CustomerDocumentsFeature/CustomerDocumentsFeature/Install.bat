@SET TEMPLATEDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm"

Echo Deactivating feature
%STSADM% -o deactivatefeature -filename CustomerDocuments\feature.xml -url http://portal.sample.com/SiteDirectory/team1/

Echo Uninstalling feature
%STSADM% -o uninstallfeature -filename CustomerDocuments\feature.xml 

Echo Copying files
rd /s /q %TEMPLATEDIR%\Features\CustomerDocuments
xcopy /e /y TEMPLATE\* %TEMPLATEDIR%

Echo Installing feature
%STSADM% -o installfeature -filename  CustomerDocuments\feature.xml -force

IISRESET

Echo Activating features
%STSADM% -o activatefeature -filename CustomerDocuments\feature.xml -url http://portal.sample.com/SiteDirectory/team1/ -force

