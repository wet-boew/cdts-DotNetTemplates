ECHO OFF
set msBuildDir=%PROGRAMFILES(X86)%\MSBuild\14.0\Bin\
set TFSWorkingFolder=c:\git\
set PackageDir=c:\Temp\Release

CLS
:MENU
CLS
ECHO.
ECHO .................................................
ECHO PRESS 1, OR 2 to select your task, or 10 to EXIT.
ECHO .................................................
ECHO.
ECHO 1 - Build Nuget package for GocWebTemplate Solution
ECHO 2 - Build Nuget package for GocWebTemplate - Samples Solution
ECHO 10 - EXIT
ECHO.
SET /P M=Type 1, 2 then press ENTER:
IF %M%==1 GOTO OCTOPACK_WebTemplate
IF %M%==2 GOTO OCTOPACK_WebTemplateSamples
IF %M%==10 GOTO EOF

:OCTOPACK_WebTemplate
CLS
SET /P VersionNumber= Please enter the version number:
call "%msBuildDir%\msbuild.exe"  "%TFSWorkingFolder%\GoCWebTemplates.sln" /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=%VersionNumber%

xcopy /s /y "%TFSWorkingFolder%Applications\GoC.WebTemplate\bin\GoC.WebTemplate-WebForms.%VersionNumber%.nupkg" "%PackageDir%"
xcopy /s /y "%TFSWorkingFolder%Applications\GoC.WebTemplateMVC\bin\GoC.WebTemplate-MVC.%VersionNumber%.nupkg" "%PackageDir%"
xcopy /s /y "%TFSWorkingFolder%Components\WebTemplateCore\bin\Release\GoC.WebTemplate-Components.%VersionNumber%.nupkg" "%PackageDir%"
pause
GOTO MENU

:OCTOPACK_WebTemplateSamples
CLS
SET /P VersionNumber= Please enter the version number:
call "%msBuildDir%\msbuild.exe"  "%TFSWorkingFolder%\GoC.WebTemplate.Samples.sln" /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=%VersionNumber%

xcopy /s /y "%TFSWorkingFolder%\SampleCode\GoC.WebTemplate-MVC.Sample\bin\GoC.WebTemplate-MVC.Sample.%versionNumber%.nupkg" "%PackageDir%"
xcopy /s /y "%TFSWorkingFolder%\SampleCode\GoC.WebTemplate-WebForms.Sample\bin\GoC.WebTemplate-WebForms.Sample.%versionNumber%.nupkg" "%PackageDir%"
pause
GOTO MENU

endlocal
