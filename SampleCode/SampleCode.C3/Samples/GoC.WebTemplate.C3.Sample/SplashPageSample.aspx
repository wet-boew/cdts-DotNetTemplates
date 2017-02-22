<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SplashPageSample.aspx.cs" Inherits="SampleCode.C3.Samples.SplashPageSample" %>

<!--[if lt IE 9]><html class="no-js lt-ie9" lang="<%= WebTemplateCore.TwoLetterCultureLanguage %>"><![endif]-->
<!--[if gt IE 8]><!-->
<html  xmlns="http://www.w3.org/1999/xhtml" lang="<%= WebTemplateCore.TwoLetterCultureLanguage %>">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <title><%= WebTemplateCore.HeaderTitle%></title>
    <meta content="width=device-width,initial-scale=1" name="viewport">
    <!-- Load closure template scripts -->
    <script type="text/javascript" src="<%= string.Concat(WebTemplateCore.CDNPath, "soyutils.js")%>"></script>
    <script type="text/javascript" src="<%= string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}wet-{1}.js", WebTemplateCore.CDNPath, WebTemplateCore.TwoLetterCultureLanguage) %>"></script>
    <noscript>
        <!-- Write closure fall-back static file -->
        <%= WebTemplateCore.LoadStaticFile("splashTop.html")%>
    </noscript>
    <!-- Write closure template -->
    <script type="text/javascript">
        document.write(wet.builder.splashTop({
            cdnEnv: "<%= WebTemplateCore.CDNEnvironment%>"
        }));
    </script>
    <%= WebTemplateCore.RenderHtmlHeaderElements()%>
</head>
<body class="splash" vocab="http://schema.org/" typeof="WebPage">
    <div id="splashContent">
        <!-- Write closure fall-back static file -->
        <%= WebTemplateCore.LoadStaticFile("splash.html")%>
        <!-- Write closure template -->
        <script type="text/javascript">
            var contentSplash = document.getElementById("splashContent");
            contentSplash.innerHTML = wet.builder.splash({
                cdnEnv: "<%= WebTemplateCore.CDNEnvironment%>",
                indexEng: "http://www.canada.ca/en/index.html",
                indexFra: "http://www.canada.ca/fr/index.html",
                termsEng: "http://www.canada.ca/en/transparency/terms.html",
                termsFra: "http://www.canada.ca/fr/transparence/avis.html",
                nameEng: "[My web asset]",
                nameFra: "[Mon actif web]"
            });
        </script>
    </div>
</body>
</html>

