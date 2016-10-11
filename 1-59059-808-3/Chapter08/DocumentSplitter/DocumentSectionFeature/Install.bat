@SET TEMPLATEDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm"

Echo Deactivating feature
%STSADM% -o deactivatefeature -filename DocumentSectionFeature\feature.xml -url http://portal.sample.com/SiteDirectory/dev2

Echo Uninstalling feature
%STSADM% -o uninstallfeature -filename DocumentSectionFeature\feature.xml 

Echo Copying files
rd /s /q %TEMPLATEDIR%\Features\DocumentSectionFeature
xcopy /e /y TEMPLATE\* %TEMPLATEDIR%

Echo Installing feature
%STSADM% -o installfeature -filename  DocumentSectionFeature\feature.xml -force

IISRESET

Echo Activating features
%STSADM% -o activatefeature -filename DocumentSectionFeature\feature.xml -url http://portal.sample.com/SiteDirectory/dev2 -force

