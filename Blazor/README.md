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

  - Add the GoC.WebTemplate.Blazor nuget package to your Blazor project. This should automatically pull in the needed DotNetTemplates libraries as well.

  - In index.html, add the stylesheet element to the HEAD section corresponding to the desired template/environment:

    | Version                   | Theme                          | Environment | Type       | CSS URL                                                                                                                  |
    | ------------------------- | ------------------------------ | ----------- | ---------- | ------------------------------------------------------------------------------------------------------------------------ |
    | specific<br>(e.g. v5_0_0) | gcweb                          | prod        | app        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/v5_0_0/cdts/cdts-app-styles.css">`     |
    | specific<br>(e.g. v5_0_0) | gcweb                          | prod        | common     | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/v5_0_0/cdts/cdts-styles.css">`         |
    | specific<br>(e.g. v5_0_0) | gcweb                          | esdcprod    | app        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/cdts-app-styles.css">`        |
    | specific<br>(e.g. v5_0_0) | gcweb                          | esdcprod    | common     | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/cdts-styles.css">`            |
    | specific<br>(e.g. v5_0_0) | gcintranet                     | prod        | app/common | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-styles.css">`       |
    | specific<br>(e.g. v5_0_0) | gcintranet                     | esdcprod    | app/common | `<link rel="stylesheet" href="https://templates.service.gc.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-styles.css">`      |
    | specific<br>(e.g. v5_0_0) | gcintranet<br>(ESDC sub-theme) | prod        | app/common | `<link rel="stylesheet" href="https://cdts.service.canada.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-esdc-styles.css">`  |
    | specific<br>(e.g. v5_0_0) | gcintranet<br>(ESDC sub-theme) | esdcprod    | app/common | `<link rel="stylesheet" href="https://templates.service.gc.ca/app/cls/WET/gcintranet/v5_0_0/cdts/cdts-esdc-styles.css">` |
    | rolling                   | gcweb                          | prod        | app        | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/rn/cdts/cdts-app-styles.css">`         |
    | rolling                   | gcweb                          | prod        | common     | `<link rel="stylesheet" href="https://www.canada.ca/etc/designs/canada/cdts/gcweb/rn/cdts/cdts-styles.css">`             |
    | rolling                   | gcweb                          | esdcprod    | app        | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcweb/cdts/cdts-app-styles.css">`                |
    | rolling                   | gcweb                          | esdcprod    | common     | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcweb/cdts/cdts-styles.css">`                    |
    | rolling                   | gcintranet                     | prod        | app/common | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcintranet/cdts/cdts-styles.css">`               |
    | rolling                   | gcintranet                     | esdcprod    | app/common | `<link rel="stylesheet" href="https://templates.service.gc.ca/rn/cls/WET/gcintranet/cdts/cdts-styles.css">`              |
    | rolling                   | gcintranet<br>(ESDC sub-theme) | prod        | app/common | `<link rel="stylesheet" href="https://cdts.service.canada.ca/rn/cls/WET/gcintranet/cdts/cdts-esdc-styles.css">`          |
    | rolling                   | gcintranet<br>(ESDC sub-theme) | esdcprod    | app/common | `<link rel="stylesheet" href="https://templates.service.gc.ca/rn/cls/WET/gcintranet/cdts/cdts-esdc-styles.css">`         |

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

    await CDTSMain.InstallCDTS(js, CultureInfo.CurrentCulture.Parent.IetfLanguageTag);

    await host.RunAsync();
```

  - When calling InstallCDTS (above), there are two optional parameters:
  - First optional parameter is a SetupBase object which you can use to define the RefTop and RefFooter properties such as ExitScript
  - Second optional parameter is a boolean that needs to be set to true if using the application template

```
    //OPTIONAL: Custom SetupBase object
    SetupBase setupBase = new SetupBase()
    {
        ExitSecureSite = new ExitSecureSite()
        {
            ExitScript = true,
            DisplayModal = true
        }
    };

    await CDTSMain.InstallCDTS(js, CultureInfo.CurrentCulture.Parent.IetfLanguageTag, setupBase, true);
```

  - In App.razor, add the following on the Router tag:
  
    `AdditionalAssemblies="new[] { typeof(ChangeLang).Assembly }"`

  - In the .csproj file, add the following inside `<PropertyGroup>`: 

    `<BlazorWebAssemblyLoadAllGlobalizationData>true</BlazorWebAssemblyLoadAllGlobalizationData>`

## MainLayout

  - The main CDTS component required is `<CDTSLayout></CDTSLayout>`
  
  - This component requires three parameters which are the sections that need to be configured in any CDTS layout with the fourth being optional
  
 ``` 
    Top
    PreFooter
    Footer
    LeftMenu (optional)
```
    
  - Create your objects, set the optional properties, and pass it to the component

```
<CDTSLayout Top="@topObj" PreFooter="@obj" Footer="@footerObj" LeftMenu="@leftMenuObj">
    @Body
</CDTSLayout>

@code {
    //Create your objects
    GoC.WebTemplate.Blazor.Model.Top topObj = new GoC.WebTemplate.Blazor.Model.Top()
    {
        ...//Top properties
    };

    GoC.WebTemplate.Blazor.Model.PreFooter preFooterObj = new GoC.WebTemplate.Blazor.Model.PreFooter()
    {
        ...//PreFooter properties
    };

    GoC.WebTemplate.Blazor.Model.Footer footerObj = new GoC.WebTemplate.Blazor.Model.Footer()
    {
        ...//Footer properties
    };

    List<GoC.WebTemplate.Components.Entities.MenuSection> leftMenuObj = new List<MenuSection>()
    {
        ...//Define your menu sections
    }

    ...    
```
  - If using the application template, simply use instances of `appTop` and `appFooter` instead
  - Note: When calling InstallCDTS in Program.cs, one of the optional parameters is a boolean that needs to be set to true if using the application template

```
<CDTSLayout Top="@appTopObj" PreFooter="@obj" Footer="@appFooterObj" LeftMenu="@leftMenuObj">
    @Body
</CDTSLayout>

@code {
    //Create your objects
    GoC.WebTemplate.Blazor.Model.AppTop appTopObj = new GoC.WebTemplate.Blazor.Model.AppTop()
    {
        ...//AppTop properties
    };

    GoC.WebTemplate.Blazor.Model.PreFooter preFooterObj = new GoC.WebTemplate.Blazor.Model.PreFooter()
    {
        ...//PreFooter properties
    };

    GoC.WebTemplate.Blazor.Model.AppFooter appFooterObj = new GoC.WebTemplate.Blazor.Model.AppFooter()
    {
        ...//AppFooter properties
    };

    List<GoC.WebTemplate.Components.Entities.MenuSection> leftMenuObj = new List<MenuSection>()
    {
        ...//Define your menu sections
    }

    ...
```

## Handling CDTS Language Switching

The CDTS and the WET library support both English and French languages but only one can be active at any given time. 

Currently, there is a Blazor component called 'ChangeLang' that takes care of language handling by default.

The component stores and retrieves the page name, changes the language and reloads that same page the user was on in the other language. 

OPTIONAL: You can override the language link if you would like handle the language switching on your end by setting the `LngLinks` property of the `top` section:
  - If left `undefined` or set to `lngLinks: null` : The CDTS Blazor component will install its default event. This is the recommended option.
  - If set to an empty array (i.e. `lngLinks: []`) : Disables the generation of the standard CDTS language link.
  - If set to any other valid value (e.g. `lngLinks: [{"lang": "fr", "href": "/to_french",    "text": "Français"}]`) : A "normal" language link will be generated accordingly and the CDTS language switch event will NOT be attached.
  The user will be responsible for handling the language switch or the language link will need to add `ChnageLang` as part of the href

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

If the [exitSecureSite functionality](https://cdts.service.canada.ca/app/cls/WET/gcweb/v5_0_0/cdts/samples/exitscript-en.html) of CDTS is enabled in the `SetupBase` object, the CDTS Blazor component will ensure any external links it has under its control are properly handled.
To make sure any external link that are within the application's components are handled in a consistent manner, the `WetExternalLink` component should be used.
(**If normal `<a>` elements are used, the exit popup may show up inconsistently depending on timing**)

Example:

```
<div>
    <WetExternalLink href="https://google.ca">Test External Link</WetExternalLink>
</div>
```

### ResetWetComponents Function

This function is mainly used by the `WetContainer` component and typically would not be called directly, but is made available should there be a need.

This function triggers a re-initialization of all instances of WET components with the specified name(s) currently on the page.

NOTE: WET components should not be confused with Blazor components, WET components are identified by the CSS class used to mark HTML elements they should apply to (e.g. "wb-frmvld")

Parameters:

  | Name              | Type        | Description                                                                     |
  | ----------------- | ----------- | ------------------------------------------------------------------------------- |
  | js                | IJSRuntime  | Instance of JavaScript Runtime                                                  |
  | wetComponentNames | {...string} | Series of string parameters specifiying the WET components to be re-initialized |

```
var js = host.Services.GetRequiredService<IJSRuntime>();
List<string> wetNames = new List<string>()
{
    "wb-frmvld",
    "wb-lbx"
};

await CDTSMain.ResetWetComponents(js, wetNames);
```

## Test Project

  - The cdts-DotNetTemplates repository contains the Blazor.Test project

  - This is a sample project that shows how a user can setup their Blazor project with CDTS