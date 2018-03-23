:: Hide Command and Set Scope
ECHO OFF
setlocal EnableExtensions

setlocal EnableDelayedExpansion
for /F "tokens=1,2 delims=#" %%a in ('"prompt #$H#$E# & echo on & for %%b in (1) do rem"') do (
  set "DEL=%%a"
)

::set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
set msBuildDir=%PROGRAMFILES(X86)%\MSBuild\14.0\Bin\
set TFSWorkingFolder=c:\git\DotNetTemplates\
set NuGetDir=%TFSWorkingFolder%Builds\.nuget
:: Customize Window
title Nuget and GCPedia Packages


CLS
:MENU
ECHO.
ECHO ...............................................
ECHO PRESS 1, 2, 3, 4, 5, 6 OR 7 to select your task, or 8 to EXIT.
ECHO ...............................................
ECHO.
ECHO 1 - Build Nuget package for GocWebTemplate for ASPX
ECHO 2 - Build Nuget package for GocWebTemplate Samples for ASPX
ECHO 3 - Build GCPedia package for GocWebTemplate for ASPX
ECHO 4 - Build both package for GocWebTemplate for ASPX
ECHO 5 - Build Nuget package for GocWebTemplate for MVC
ECHO 6 - Build Nuget package for GocWebTemplate Samples for MVC
ECHO 7 - Build GCPedia package for GocWebTemplate for MVC
ECHO 8 - Build both package for GocWebTemplate for MVC
ECHO 9 - Build all packages
ECHO 10 - EXIT
ECHO.
SET /P M=Type 1, 2, 3, 4, 5, 6, 7, 8 or 9 then press ENTER:
IF %M%==1 GOTO NUGET_ASPX
IF %M%==2 GOTO NUGET_SAMPLES_ASPX
IF %M%==3 GOTO GCPEDIA_ASPX
IF %M%==4 GOTO BOTH_ASPX
IF %M%==5 GOTO NUGET_MVC
IF %M%==6 GOTO NUGET_SAMPLES_MVC
IF %M%==7 GOTO GCPEDIA_MVC
IF %M%==8 GOTO BOTH_MVC
IF %M%==9 GOTO ALL_PACKAGES
IF %M%==11 GOTO BUILD_SOLUTION
IF %M%==10 GOTO EOF

:NUGET_ASPX
echo Building WebForms solution
call :BUILD_SOLUTION
echo navigating to %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\
call :ASKING_QUESTIONS
echo Building NuGet Package
call "%msBuildDir%\msbuild.exe" NugetPackageASPX.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageASPX.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:NUGET_SAMPLES_ASPX
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplateSamples\
call :ASKING_QUESTIONS_SAMPLES
call "%msBuildDir%\msbuild.exe"  NuGetPackageSamplesASPX.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";DependencyVersionNumber="%DependencyVersionNumber%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NuGetPackageSamplesASPX.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:GCPEDIA_ASPX
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GCPedia\
call "%msBuildDir%\msbuild.exe"  GCPediaASPXProject.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaASPXProject.log
call :RETURN_TO_MENU

:BOTH_ASPX
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\
call "%msBuildDir%\msbuild.exe"  NugetPackageASPX.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageASPX.log
cd %TFSWorkingFolder%\Builds\BuildScripts\GCPedia\
call "%msBuildDir%\msbuild.exe"  GCPediaASPXProject.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaASPXProject.log
call :RETURN_TO_MENU

:NUGET_MVC
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\
call :ASKING_QUESTIONS
call "%msBuildDir%\msbuild.exe"  NugetPackageMVC.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageMVC.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:NUGET_SAMPLES_MVC
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplateSamples\
call :ASKING_QUESTIONS_SAMPLES
call "%msBuildDir%\msbuild.exe"  NugetPackageSamplesMVC.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";DependencyVersionNumber="%DependencyVersionNumber%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NuGetPackageSamplesMVC.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:GCPEDIA_MVC
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GCPedia\
call "%msBuildDir%\msbuild.exe"  GCPediaMVCProject.proj /p:VisualStudioVersion=14.0;TFSWorkingFolder=%TFSWorkingFolder% /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaMVCProject.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:BOTH_MVC
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\
call :RESET_VARIABLES
call :ASKING_QUESTIONS
call "%msBuildDir%\msbuild.exe"  NugetPackageMVC.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageMVC.log
cd %TFSWorkingFolder%\Builds\BuildScripts\GCPedia\
call "%msBuildDir%\msbuild.exe"  GCPediaMVCProject.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaMVCProject.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:ALL_PACKAGES
call :BUILD_SOLUTION
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplates\

call :ASKING_QUESTIONS
call "%msBuildDir%\msbuild.exe"  NugetPackageASPX.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageASPX.log
call :RESET_VARIABLES

call :ASKING_QUESTIONS
call "%msBuildDir%\msbuild.exe"  NugetPackageMVC.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NugetPackageMVC.log
call :RESET_VARIABLES

cd %TFSWorkingFolder%\Builds\BuildScripts\GCPedia\
call "%msBuildDir%\msbuild.exe"  GCPediaASPXProject.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaASPXProject.log
call "%msBuildDir%\msbuild.exe"  GCPediaMVCProject.proj /p:VisualStudioVersion=14.0 /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\GCPediaMVCProject.log
cd %TFSWorkingFolder%\Builds\BuildScripts\GoCWebTemplateSamples\

call :ASKING_QUESTIONS_SAMPLES
call "%msBuildDir%\msbuild.exe"  NuGetPackageSamplesASPX.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";DependencyVersionNumber="%DependencyVersionNumber%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NuGetPackageSamplesASPX.log
call :RESET_VARIABLES

call :ASKING_QUESTIONS_SAMPLES
call "%msBuildDir%\msbuild.exe"  NugetPackageSamplesMVC.proj /p:VisualStudioVersion=14.0;PreRelease="%PreRelease%";DependencyVersionNumber="%DependencyVersionNumber%";ReleaseNotes="%ReleaseNotes%" /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\NuGetPackageSamplesMVC.log
call :RESET_VARIABLES
call :RETURN_TO_MENU

:EXITSPOT
echo The version number need to be changed in AssemblyInfo.cs and the ChangeLog.txt
pause
cls
GOTO MENU
exit

:BUILD_SOLUTION
echo Building solution
echo navigating to %TFSWorkingFolder%\
cd %TFSWorkingFolder%\
echo restoring packages using nuget at %NuGetDir%\Nuget.exe
call %NuGetDir%\Nuget.exe restore "GoCWebTemplates.sln" -ConfigFile "%NuGetDir%\Nuget.config"
echo calling msbuild
call "%msBuildDir%\msbuild.exe"  GoCWebTemplates.sln /verbosity:quiet /t:Rebuild /p:VisualStudioVersion=14.0;Configuration=Release /l:FileLogger,Microsoft.Build.Engine;logfile=c:\Temp\BuildSolution.log
GOTO :EOF

:RESET_VARIABLES
set "VersionNumber="
set "PreRelease="
set "DependencyVersionNumber="
set "ReleaseNotes="
GOTO :EOF

:ASKING_QUESTIONS
set /p IsVersionNumberUpdated= Did you update the version # in the AssemblyInfo and ChangeLog files (Y-Yes N-No)? 
if /I "%IsVersionNumberUpdated%" NEQ "Y" goto :EXITSPOT
if /I "%IsVersionNumberUpdated%" == "" goto :EXITSPOT
set /p PreRelease= If this is a pre-release, enter build # :
set /p ReleaseNotes= Enter Release Notes :
GOTO :EOF

:ASKING_QUESTIONS_SAMPLES
set /p IsVersionNumberUpdated= Did you update the version # in the AssemblyInfo and ChangeLog files (Y-Yes N-No)? 
if /I "%IsVersionNumberUpdated%" NEQ "Y" goto :EXITSPOT
if /I "%IsVersionNumberUpdated%" == "" goto :EXITSPOT
set /p PreRelease= If this is a pre-release, enter build # :
set /p DependencyVersionNumber= Enter dependency version # of the  WebTemplate :
set /p ReleaseNotes= Enter Release Notes :
GOTO :EOF

:RETURN_TO_MENU
explorer c:\Temp\Release
pause
cls
GOTO MENU

endlocal
