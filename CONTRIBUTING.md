# How to contribute

From all the developers currently working on this project, we are so happy your willing to help! We need it!

Please read the [wiki](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/home) to learn more about what this project is about.

**Get involved!**
If you're new to GCCode or Git don't worry, open an issue asking for some help and what you want to do. We'll be happy to help.

## Reporting

Reporting new additions though issues is key. This helps us keep track of all the work that is needed and wanted for this project.

Please look for similar issues that you can add to, before opening a new one. When opening a new issue use the templates and give us as much detail as you can.

If you are reporting lots of good issues, you can request "reporter" access so you can tag your own issues.

## Developing

You can fork the project, and make a merge request with your changes.

After a few approved merge requests, you can request "developer" access. We'll take a quick look at your previous contributions and probably contact you personally before approving.

### Development environment setup

Visual Studio 2015 or later with .NET Framework 4.6.1 or later is required. Access to NuGet.org is also required. If you are running the Samples project, you will need to have a copy or compile the Templates them selves as a NuGet package. You will also have to recreate any secrets.

#### Pipeline requirements for forks

You will need to register a [GitLab-Runner](https://docs.gitlab.com/runner/) with a `shell` executor and provide `environment` variables for the jobs you intend on running.

### Changing Code

#### General guidance

* All code changes must be reviewed though a merge request. 
* Keep changes under 100 lines of edits.

#### Testing guidance

* Tests must be added for any fixed ~bug or any complex function. 
* When fixing broken ~tests, don't change the _assert_ line. If you do, provide justification in the MR details.

### Merge Requests Reviews

#### Requesting:

* The [General](.gitlab/merge_request_templates/General.md) merge request template (or another) must be used.
* The request description should outline and detail the scope of changes
* The request must have less than 250 lines of edits.

#### Reviewing:

* Only review changed lines.
* When reviewing ~tests, as long as the _assert_ line is not changed, it can be assumed the test has not _really_ changed.
* Discussions can be used to gain clarification on changes. This is as much for the reviewer as it is for the author.
* Anyone on the project can raise discussions on a PR/MR, even if they are not assigned to it.
* When approving a merge, you are just as responsible for the changes as the person who made them. So you should understand them just as well.
