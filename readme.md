# DotNetTemplates

A template solution for application written in .NET (DotNet) Framework with MVC or WebForms. These templates are running [Centerall Deployed Templates Solution (CDTS)](http://www.gcpedia.gc.ca/wiki/Centrally_Deployed_Templates_Solution_(CDTS)) to implement [wet-boew, the Web Experince Toolkit](https://github.com/wet-boew/wet-boew).

New work on a .NET Core template is being done, [check it out in the wiki](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/home#building-net-core)

The `DotNetTemplates` has a sister template for the Java applications [JavaTemplates](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/JavaTemplates)

## Instalation

https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/Documentation/Installation

## Documenation

See the wiki: https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/home  
Or install the Samples project

## Contributing

Please see the [CONTRIBUTING](CONTRIBUTING.md) guide.

### Development enviornment setup

Visual Studio 2015 or later with .NET Framework 4.6.1 or later is required. Access to NuGet.org is also required. If you are running the Samples project, you will need to have a copy or compile the Templates them selfves as a NuGet package.

#### Pipline requirments for forks

You will need to register a [GitLab-Runner](https://docs.gitlab.com/runner/) with a `shell` executor and provide an `enviornment` variable to a local `msbuild.exe` with the name `MSBUILD_PATH`.