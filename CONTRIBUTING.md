# How to contribute

From all the developers currently working on this project, we are so happy your willing to help! We need it!

Please read the [wiki](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/home) to learn more about what this project is about.

## Getting involved

If you're new to GCCode or Git don't worry, open an issue asking for some help and what you want to do. We'll be happy to help.

### Reporting

Reporting new additions though issues is key. This helps us keep track of all the work that is needed and wanted for this project.

Please look for similar issues that you can add to, before opening a new one. When opening a new issue use the templates and give us as much detail as you can.

If you are reporting lots of good issues, you can request "reporter" access so you can tag your own issues.

### Developing

You can fork the project, and make a merge request with your changes.

After a few approved merge requests, you can request "developer" access. We'll take a quick look at your previous contributions and probably contact you personally before approving.

#### Development environment setup

Visual Studio 2015 or later with .NET Framework 4.6.1 or later is required. Access to NuGet.org is also required. If you are running the Samples project, you will need to have a copy or compile the Templates them selves as a NuGet package. You will also have to recreate any secrets.

#### Pipeline requirements for forks

You will need to register a [GitLab-Runner](https://docs.gitlab.com/runner/) with a `shell` executor and provide `environment` variables for the jobs you intend on running.
