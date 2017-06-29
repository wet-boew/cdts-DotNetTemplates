echo "Copy Static files, views, and controllers "
xcopy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\GoC.WebTemplate c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\GoC.WebTemplate /Y /S
xcopy C:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\Samples c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\Samples /Y /S
xcopy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\Views c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\Views /Y /S
xcopy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\Controllers c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\Controllers /Y /S

echo "Copy web.configs"
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\Web.config c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\Web.config /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\Web.config c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\Web.config /Y
echo "copy dlls"
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\bin\GoC.WebTemplate.dll c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\Libraries\ /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\bin\WebTemplateCore.dll c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\Libraries\ /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\bin\WebTemplateCore.dll c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\Libraries\ /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\bin\GoC.WebTemplateMVC.dll c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\Libraries /Y
echo "Copy CDTSEnvironments.json files"
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\CDTSEnvironments.json c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\ /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\CDTSEnvironments.json c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\ /Y
echo "Copy XSD"
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3\WebTemplateWebForms.xsd c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterASPX\ /Y
copy c:\workspaces\DotNetTemplates\SampleCode\SampleCode.C3.MVC\WebTemplateMVC.xsd c:\workspaces\DotNetTemplates\Applications\GoCWebTemplateStarterMVC\ /Y
