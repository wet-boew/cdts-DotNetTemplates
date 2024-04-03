# Blazor template for the Centrally Deployed Templates Solution (CDTS)

A template solution for Blazor applications. The templates provided are running [Centrally Deployed Templates Solution (CDTS)](https://cenw-wscoe.github.io/sgdc-cdts/docs/index-en.html) to implement [wet-boew, the Web Experience Toolkit](https://github.com/wet-boew/wet-boew).


## Disclaimer

The current version of the CDTS Blazor component is considered a prototype and has not been tested for production use.
The CDTS and particularily the WET library were initialy designed to be used on static pages.
This project is an attempt to bridge the gaps to make CDTS and WET usable on a dynamic Blazor web application.  Unforeseen side-effects or problems may remain to be found and applications should be tested with care on different browsers.

In other words:

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE

Issues can be reported on the [Github repository](https://github.com/wet-boew/cdts-DotNetTemplates/issues).

## Basic Usage

To use, in your project: 

  - Add the GoC.WebTemplate.Blazor nuget package to your Blazor project. This should automatically pull in the GoC.WebTEmplate.Entities package as well.

  - In index.html, add the stylesheet element to the HEAD section corresponding to the desired template/environment:

    | Version                   | Theme                          | Environment | CSS URL                                                                                                                  |
    | ------------------------- | ------------------------------ | ----------- | ------------------------------------------------------------------------------------------------------------------------ |
    | specific<br>(e.g. v4_1_0) | gcweb                          | prod        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/v5_0_0/cdts/cdts-app-styles.css">`     |
    | specific<br>(e.g. v4_1_0) | gcweb                          | prod        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/v5_0_0/cdts/cdts-styles.css">`         |
    | specific<br>(e.g. v4_1_0) | gcweb                          | esdcprod    | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/cdts-app-styles.css">`        |
    | specific<br>(e.g. v4_1_0) | gcweb                          | esdcprod    | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/cdts-styles.css">`            |
    | specific<br>(e.g. v4_1_0) | gcintranet                     | prod        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-styles.css">`       |
    | specific<br>(e.g. v4_1_0) | gcintranet                     | esdcprod    | `<link rel="stylesheet" href="https://templates.service.gc.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-styles.css">`      |
    | specific<br>(e.g. v4_1_0) | gcintranet<br>(ESDC sub-theme) | prod        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-esdc-styles.css">`  |
    | specific<br>(e.g. v4_1_0) | gcintranet<br>(ESDC sub-theme) | esdcprod    | `<link rel="stylesheet" href="https://templates.service.gc.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-esdc-styles.css">` |
    | rolling                   | gcweb                          | prod        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/rn/cdts/cdts-app-styles.css">`         |
    | rolling                   | gcweb                          | prod        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/rn/cdts/cdts-styles.css">`             |
    | rolling                   | gcweb                          | esdcprod    | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcweb/cdts/cdts-app-styles.css">`                |
    | rolling                   | gcweb                          | esdcprod    | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcweb/cdts/cdts-styles.css">`                    |
    | rolling                   | gcintranet                     | prod        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcintranet/cdts/cdts-styles.css">`               |
    | rolling                   | gcintranet                     | esdcprod    | `<link rel="stylesheet" href="https://templates.service.gc.ca/rn/cls/WET/gcintranet/cdts/cdts-styles.css">`              |
    | rolling                   | gcintranet<br>(ESDC sub-theme) | prod        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcintranet/cdts/cdts-esdc-styles.css">`          |
    | rolling                   | gcintranet<br>(ESDC sub-theme) | esdcprod    | `<link rel="stylesheet" href="https://templates.service.gc.ca/rn/cls/WET/gcintranet/cdts/cdts-esdc-styles.css">`         |

  - In index.html, add the following script element that points to the CDTS Blazor JavaScript file

     `<script src="_content/GoC.WebTemplate.Blazor/cdts-blazor.min.js"></script>`

  - In _Imports.razor, add the following using statements: 

```
    @using GoC.WebTemplate.Blazor.Components
    @using GoC.WebTemplate.Blazor.Layouts
    @using GoC.WebTemplate.Components.Entities
```

  - In Program.cs, add the following using statements:
  
```
    using Microsoft.JSInterop;
    using System.Globalization;
```

  - In Program.cs, replace `await builder.Build().RunAsync();` with the following: 

``` 
    var host = builder.Build();

    CultureInfo culture;
    var js = host.Services.GetRequiredService<IJSRuntime>();
    var result = await js.InvokeAsync<string>("blazorCulture.get");

    if (result != null)
    {
        culture = new CultureInfo(result);
    }
    else
    {
        culture = new CultureInfo("en-CA");
        await js.InvokeVoidAsync("blazorCulture.set", "en-CA");
    }

    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;

    await js.InvokeVoidAsync("installCDTS", CultureInfo.CurrentCulture.Parent.IetfLanguageTag);

    await host.RunAsync();
```

  - In App.razor, add the following on the Router tag:
  
    `AdditionalAssemblies="new[] { typeof(ChangeLang).Assembly }"`

  - In the .csproj file, add the following inside `<PropertyGroup>`: 

    `<BlazorWebAssemblyLoadAllGlobalizationData>true</BlazorWebAssemblyLoadAllGlobalizationData>`

## MainLayout

  - The main CDTS component required is `<CDTSLayout></CDTSLayout>`
  
  - This component requires five parameters which are the sections that need to be configured in any CDTS layout with the sixth being optional
  
 ``` 
    RefTop
    Top
    PreFooter
    Footer
    RefFooter
    LeftMenu (optional)
```
    
  - Create your objects, set the optional properties, and pass it to the component

```
<CDTSLayout RefTop="@refTopObj" Top="@topObj" PreFooter="@obj" Footer="@footerObj" RefFooter="@refFooterObj" LeftMenu="@leftMenuObj">
    @Body
</CDTSLayout>

@code {
    //Create your objects
    GoC.WebTemplate.Blazor.Model.RefTop refTopObj = new GoC.WebTemplate.Blazor.Model.RefTop(){};
    
    ...    
```
  - If using the application template, simply use `appTop` and `appFooter` instead

## Handling CDTS Language Switching

The CDTS and the WET library support both English and French languages but only one can be active at any given time. 

Currently, there is a Blazor component called 'ChangeLang' that can take care of language handling when called. 

The way it works is on the initial page load AND on page change, you have to call the 'SetCurrentPage' function and pass the current page name (the function temporarily stores the passed page name). 

You have to create and assign the language link and set the href of the link to the BaseUri + 'ChangeLang'. 
The component, when called, retrieves the stored page name, changes the language and reloads that same page the user was on in the other language. 

```
protected override Task OnInitializedAsync()
{
    JS.InvokeVoidAsync("SetCurrentPage", Navigation.ToBaseRelativePath(Navigation.Uri));

    List<LanguageLink> links = new List<LanguageLink>()
    {
        new LanguageLink(){Href=Navigation.BaseUri + "ChangeLang"}
    };

    topObj.LngLinks = links;

    //Detect when the user has clicked on another page
    Navigation.LocationChanged += (o, e) =>
    {
        JS.InvokeVoidAsync("SetCurrentPage", Navigation.ToBaseRelativePath(Navigation.Uri));
    };

    return base.OnInitializedAsync();
}
```

## Using WET Components

The CDTS and WET libraries were designed with the assumption that the web page remains static, which obvisouly present challenges when interacting with dynamic pages such as a Blazor application.

The CDTS Blazor package provides the WetContainer component to help wrap HTML elements using WET "components" and make sure they are properly re-initialized.

### WetContainer

The `WetContainer` component is used to surround HTML element(s) making use of WET "component(s)". 

For example, if using the WET "form" "component", wrap your code in the WetContainer component and provide the WetComponentName parameter with the name of the wet component. 

```
<WetContainer WetComponentName = "@wetComponentToRefresh">
    <div class="wb-frmvld">
        <form action="#" method="get" id="validation-example">
            <div class="form-group">
                <label for="fname1" class="required"><span class="field-name">First name</span> <strong class="required">(required)</strong></label>
                <input class="form-control" id="fname1" name="fname1" type="text" autocomplete="given-name" required="required" data-rule-minlength="2" />
                <label for="lname1" class="required"><span class="field-name">Last name</span> <strong class="required">(required)</strong></label>
                <input class="form-control" id="lname1" name="lname1" type="text" autocomplete="family-name" required="required" data-rule-minlength="2" />
                <label for="title1" class="required"><span class="field-name">Title</span> <strong class="required">(required)</strong></label>
                <select class="form-control" id="title1" name="title1" autocomplete="honorific-prefix" required="required">
                    <option label="Select a title"></option>
                    <option value="dr">Dr.</option>
                    <option value="esq">Esq.</option>
                    <option value="mr">Mr.</option>
                    <option value="ms">Ms.</option>
                </select>
            </div>
            <input type="submit" value="Submit" class="btn btn-primary"> <input type="reset" value="Reset page to defaults" class="btn btn-link btn-sm show p-0 mrgn-tp-md">
        </form>
    </div>
</WetContainer>

@code {
    string wetComponentToRefresh = "wb-frmvld" ;
}
```

Parameters:

  | Name              | Type        | Description                                                                     |
  | ----------------- | ----------- | ------------------------------------------------------------------------------- |
  | WetComponentName  | {...string} | A string parameter specifiying the WET components to be re-initialized          |
  | WetComponentNames | {...string} | Series of string parameters specifiying the WET components to be re-initialized |

### WetExternalLink

If the [exitSecureSite functionality](https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/samples/exitscript-en.html) of CDTS is enabled in the `refFooter` object, the CDTS Blazor component will ensure any external links it has under its control are properly handled.
To make sure any external link that are within the application's components are handled in a consistent manner, the `WetExternalLink` component should be used.
(**If normal `<a>` elements are used, the exit popup may show up inconsistently depending on timing**)

Example:

```
<div>
    <WetExternalLink href="https://google.ca">Test External Link</WetExternalLink>
</div>
```

### resetWetComponents Function

This function is mainly used by the `WetContainer` component and typically would not be called directly, but is begin made available should there be a need.

This function triggers a re-initialization of all instances of WET components with the specified name(s) currently on the page.
NOTE: WET components should not be confused with Blazor components, WET components are identified by the CSS class used to mark HTML elements they should apply to (e.g. "wb-frmvld")

Parameters:

  | Name              | Type        | Description                                                                     |
  | ----------------- | ----------- | ------------------------------------------------------------------------------- |
  | wetComponentNames | {...string} | Series of string parameters specifiying the WET components to be re-initialized |
