@rem======================================================================
@rem
@rem    setup.bat
@rem
@rem======================================================================

@echo off
setlocal
pushd .

goto LInitialize


@rem----------------------------------------------------------------------
@rem    LInitialize
@rem----------------------------------------------------------------------
:LInitialize
    set SPAdminTool=%CommonProgramFiles%\Microsoft Shared\web server extensions\12\BIN\stsadm.exe
    set Task=install
    set PackageFile=%~dp0ProjectsListDef.wsp
    set PackageName=ProjectsListDef.wsp
    set TargetUrl=http://portal.sample.com

    goto LParseArgs


@rem----------------------------------------------------------------------
@rem    LParseArgs
@rem----------------------------------------------------------------------
:LParseArgs
    @rem --- help ---
    if "%1" == "/?" goto LHelp
    if "%1" == "-?" goto LHelp
    if "%1" == "/h" goto LHelp
    if "%1" == "-h" goto LHelp
    if "%1" == "/help" goto LHelp
    if "%1" == "-help" goto LHelp

    @rem --- Fix execute task ---
    if "%1" == "/i"         (set Task=install)   & shift & goto LParseArgs
    if "%1" == "-i"         (set Task=install)   & shift & goto LParseArgs
    if "%1" == "/install"   (set Task=install)   & shift & goto LParseArgs
    if "%1" == "-install"   (set Task=install)   & shift & goto LParseArgs
    if "%1" == "/u"         (set Task=uninstall) & shift & goto LParseArgs
    if "%1" == "-u"         (set Task=uninstall) & shift & goto LParseArgs
    if "%1" == "/uninstall" (set Task=uninstall) & shift & goto LParseArgs
    if "%1" == "-uninstall" (set Task=uninstall) & shift & goto LParseArgs
    
    @rem --- Fix url ---
    if "%1" == "/url" (set TargetUrl=%2) & shift & goto LParseArgs
    if "%1" == "-url" (set TargetUrl=%2) & shift & goto LParseArgs

	goto LMain


@rem----------------------------------------------------------------------
@rem	LHelp
@rem----------------------------------------------------------------------
:LHelp
    echo Usage:
    echo setup.bat [/install][/uninstall][url]
    echo           [/help]
    echo.
    echo Options:
    echo  /install
    echo  Install specified Solution package (.wsp) to the SharePoint server.
    echo  /uninstall
    echo  Uninstall specified Solution from the SharePoint server.
    echo  /url
    echo  Specify a url of a site of the SharePoint server.
    echo  Default value: %TargetUrl%
    echo  /help
    echo  Show this information.
    echo.

	goto LTerminate


@rem----------------------------------------------------------------------
@rem    LMain
@rem----------------------------------------------------------------------
:LMain
	if %Task% == install   (call :LDeploy)
	if %Task% == uninstall (call :LRetract)

	goto LTerminate


@rem----------------------------------------------------------------------
@rem    LDeploy
@rem----------------------------------------------------------------------
:LDeploy
    echo Adding solution %PackageName% to the SharePoint ...
    "%SPAdminTool%" -o addsolution -filename "%PackageFile%"

    echo Deploying solution %PackageName% ...
    "%SPAdminTool%" -o deploysolution -name "%PackageName%" -local -allowGacDeployment

    echo Activating feature Projects ...
    "%SPAdminTool%" -o activatefeature -id d91c47d2-996a-470e-8c6d-bbfb7f33c666 -url %TargetUrl%

    goto :EOF


@rem----------------------------------------------------------------------
@rem    LRetract
@rem----------------------------------------------------------------------
:LRetract
    echo Deactivating feature Projects ...
    "%SPAdminTool%" -o deactivatefeature -id d91c47d2-996a-470e-8c6d-bbfb7f33c666 -url %TargetUrl%

    echo Uninstalling feature Projects ...
    "%SPAdminTool%" -o uninstallfeature -id d91c47d2-996a-470e-8c6d-bbfb7f33c666 -force

    echo Retracting solution %PackageName% ...
    "%SPAdminTool%" -o retractsolution -name "%PackageName%" -local

    echo Deleting solution %PackageName% from SharePoint ...
    "%SPAdminTool%" -o deletesolution -name "%PackageName%"

    goto :EOF


@rem----------------------------------------------------------------------
@rem    LTerminate
@rem----------------------------------------------------------------------
:LTerminate
    set UserInput=
    set /P UserInput=Hit enter key to quit.

    set SPAdminTool=
    set Task=
    set PackageFile=
    set PackageName=
    set TargetUrl=
    set UserInput=


popd
endlocal

