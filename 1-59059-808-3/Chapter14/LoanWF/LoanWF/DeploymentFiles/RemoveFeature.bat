
SET STSADM="%CommonProgramFiles%\Microsoft Shared\Web Server Extensions\12\BIN\STSADM.EXE"
%STSADM% -o deactivatefeature -filename feature.xml -url http://portal.sample.com
%STSADM% -o uninstallfeature  -filename feature.xml
CALL iisreset

