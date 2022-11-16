# DotNet Core Custom Sample

Sample the use of a .Net Core project that isn't explicit to MVC from the Components.Core.

## Do It Yourself

1. Create a new .NET Core project, your choice of type (Blazor, MVVC, MVC...). 
1. Add the [NuGet Package `GoC.WebTemplate-Components.Core`](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/Documentation/installation)
1. Remove any unneeded files (css/js) from the default template
1. Follow the [Core ToDo section](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/wikis/Documentation/installation#core-todo) of the installation
1. Integrate a page with the template
    1. Copy the [content of the `_Layout`](https://gccode.ssc-spc.gc.ca/iitb-dgiit/sds/GOCWebTemplates/DotNetTemplates/tree/master/CoreMVC/Views/GoC.WebTemplate) you would like to use, adding it to either the page itself or some "Layout" page.
    1. Access and manipulate the `ModelServices.Model` to your liking adding it to the page you copied (It should have gotten injected).

> Note: There is no guarantee all features will work, but you should be able to replicate those that don't.  
> In particular,
> 1. **StaticFallback** won't work as the project type picked will change the way layouts are handled therefor cannot be pre-provided.
> 1. **Automatic Translation** won't work if the project type handles routing diffrently than the original MVC.
