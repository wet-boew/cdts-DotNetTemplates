$msbuild = 'C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe'
$TFSWorkingFolder = "C:\git"
$PackageDir = "c:\Temp\Release"

Function BuildWebTemplate
{
    $VersionNumber = Read-Host -Prompt 'Please enter the version number'
    & "$msbuild" $TFSWorkingFolder\GoCWebTemplates.sln /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=$VersionNumber

    #Update version number for GoC.WebTemplate-WebForms
    $xmlFile = "$TFSWorkingFolder\Applications\GoC.WebTemplate\GoC.WebTemplate-WebForms.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $FirstNode = $xml.selectSingleNode('//package/metadata/version')
    $FirstNode.InnerText = "$VersionNumber"
    $SecondNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency')
    $SecondNode.version = "$VersionNumber"
    $xml.Save($xmlFile)

    #Update version number for GoC.WebTemplate-MVC
    $xmlFilePackage = "$TFSWorkingFolder\SampleCode\GoC.WebTemplate-MVC.Sample\packages.config"
    $xmlPackage = [xml](Get-Content -Path $xmlFilePackage)
    $nodePackage = $xmlPackage.selectSingleNode('//packages/package[@id="Microsoft.AspNet.Mvc"]')
    $xmlFile = "$TFSWorkingFolder\Applications\GoC.WebTemplateMVC\GoC.WebTemplate-MVC.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $FirstNode = $xml.selectSingleNode('//package/metadata/version')
    $FirstNode.InnerText = "$VersionNumber" 
    $SecondNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency')
    $SecondNode.version = "$VersionNumber"
    $ThirdNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency[@id="Microsoft.AspNet.Mvc"]')
    $ThirdNode.version = $nodePackage.version
    $xml.Save($xmlFile)

    #Update version number for Components
    $xmlFilePackage = "$TFSWorkingFolder\Components\WebTemplateCore\packages.config"
    $xmlPackage = [xml](Get-Content -Path $xmlFilePackage)
    $nodePackage = $xmlPackage.selectSingleNode('//packages/package[@id="Newtonsoft.Json"]')
    $xmlFile = "$TFSWorkingFolder\Components\WebTemplateCore\GoC.WebTemplate-Components.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $FirstNode = $xml.selectSingleNode('//package/metadata/version')
    $FirstNode.InnerText = "$VersionNumber" 
    $SecondNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency')
    $SecondNode.version = $nodePackage.version
    $xml.Save($xmlFile)

    Copy-Item -Path "$TFSWorkingFolder\Applications\GoC.WebTemplate\bin\GoC.WebTemplate-WebForms.$VersionNumber.nupkg" -Destination $PackageDir
    Copy-Item -Path "$TFSWorkingFolder\Applications\GoC.WebTemplateMVC\bin\GoC.WebTemplate-MVC.$VersionNumber.nupkg" -Destination $PackageDir
    Copy-Item -Path "$TFSWorkingFolder\Components\WebTemplateCore\bin\Release\GoC.WebTemplate-Components.$VersionNumber.nupkg" -Destination $PackageDir
}

Function BuildWebTemplateSamples
{
    $VersionNumber = Read-Host -Prompt 'Please enter the version number'
    & "$msbuild" $TFSWorkingFolder\GoC.WebTemplate.Samples.sln /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=$VersionNumber

    #Update version number for MVC Samples
    $xmlFilePackage = "$TFSWorkingFolder\Components\WebTemplateCore\GoC.WebTemplate-Components.nuspec"
    $xmlPackage = [xml](Get-Content -Path $xmlFilePackage)
    $nodePackage = $xmlPackage.selectSingleNode('//package/metadata/version')
    $xmlFile = "$TFSWorkingFolder\SampleCode\GoC.WebTemplate-MVC.Sample\GoC.WebTemplate-MVC.Sample.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $FirstNode = $xml.selectSingleNode('//package/metadata/version')
    $FirstNode.InnerText = $nodePackage.InnerText
    $SecondNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency')
    $SecondNode.version = "$VersionNumber"
    $xml.Save($xmlFile)

    #Update version number for Webforms Samples
    $xmlFilePackage = "$TFSWorkingFolder\Components\WebTemplateCore\GoC.WebTemplate-Components.nuspec"
    $xmlPackage = [xml](Get-Content -Path $xmlFilePackage)
    $nodePackage = $xmlPackage.selectSingleNode('//package/metadata/version')
    $xmlFile = "$TFSWorkingFolder\SampleCode\GoC.WebTemplate-WebForms.Sample\GoC.WebTemplate-WebForms.Sample.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $FirstNode = $xml.selectSingleNode('//package/metadata/version')
    $FirstNode.InnerText = $nodePackage.InnerText 
    $SecondNode = $xml.selectSingleNode('//package/metadata/dependencies/dependency')
    $SecondNode.version = "$VersionNumber"
    $xml.Save($xmlFile)

    Copy-Item -Path "$TFSWorkingFolder\SampleCode\GoC.WebTemplate-MVC.Sample\bin\GoC.WebTemplate-MVC.Sample.$VersionNumber.nupkg" -Destination $PackageDir
    Copy-Item -Path "$TFSWorkingFolder\SampleCode\GoC.WebTemplate-WebForms.Sample\bin\GoC.WebTemplate-WebForms.Sample.$VersionNumber.nupkg" -Destination $PackageDir
}

Clear-Host
Write-Host .................................................
Write-Host PRESS 1, OR 2 to select your task, or 9 to EXIT.
Write-Host .................................................
Write-Host .
Write-Host 1 - Build Nuget package for GocWebTemplate Solution
Write-Host 2 - Build Nuget package for GocWebTemplate - Samples Solution
Write-Host 9 - EXIT
Write-Host .

$Option = Read-Host "Input Selection"

if ($Option -eq 1)
{
    BuildWebTemplate
}

if ($Option -eq 2)
{
    BuildWebTemplateSamples
}

if ($Option -eq 9)
{
    exit
}