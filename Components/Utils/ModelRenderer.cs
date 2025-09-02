using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using GoC.WebTemplate.Components.Entities;
#if NETCOREAPP
    using Microsoft.AspNetCore.Html;
#elif NETFRAMEWORK
    using System.Web;
#endif

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Utils
{
    public class ModelRenderer
    {
        private readonly IModel _model;
        internal ModelRenderer(IModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used 
        /// as parameter for the CDTS "setup" function for a basic web page
        /// </summary>
        public HtmlString Setup()
        {
            return JsonSerializationHelper.SerializeToJson(_model.Builder.BuildCommonSetup(false, false));
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used
        /// as parameter for the CDTS "setup" function for a transactional web page
        /// </summary>
        public HtmlString TransactionalSetup()
        {
            return JsonSerializationHelper.SerializeToJson(_model.Builder.BuildCommonSetup(true, false));
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used 
        /// as parameter for the CDTS "setup" function for an unilingual error web pagee
        /// </summary>
        public HtmlString UnilingualErrorSetup()
        {
            return JsonSerializationHelper.SerializeToJson(_model.Builder.BuildCommonSetup(false, true));
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used 
        /// as parameter for the CDTS "setup" function for a "application" web page
        /// </summary>
        public HtmlString AppSetup()
        {
            return JsonSerializationHelper.SerializeToJson(new Setup
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                Mode = Mode.APP,
                Base = _model.Builder.BuildSetupBase(),
                Top = _model.Builder.BuildAppTop(),
                PreFooter = _model.Builder.BuildPreFooter(false, false),
                Footer = _model.Builder.BuildAppFooter(),
                SecMenu = _model.LeftMenuItems.Any() ? SectionMenuBuilder.BuildLeftMenu(_model.LeftMenuItems) : null,
                Splash = null,
                OnCDTSPageFinalized = _model.HTMLBodyElements
            });
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used 
        /// as parameter for the CDTS "setup" function for a "server" web page (ie used for error pages)
        /// </summary>
        public HtmlString ServerSetup()
        {
            return JsonSerializationHelper.SerializeToJson(new Setup
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                Mode = Mode.SERVER,
                Base = _model.Settings.SRIEnabled ? null : new SetupBase { SRIEnabled = false}, //base left null for defaults if SRI is true
                Top = null,
                PreFooter = null,
                Footer = null,
                SecMenu = null,
                Splash = null,
                OnCDTSPageFinalized = _model.HTMLBodyElements
            });
        }

        /// <summary>
        /// Builds a string with the format required by the closure template to represent the JSON object used 
        /// as parameter for the CDTS "setup" function for a "splash" web page
        /// </summary>
        public HtmlString SplashSetup()
        {
            return JsonSerializationHelper.SerializeToJson(new Setup
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                Mode = Mode.SPLASH,
                Base = _model.Builder.BuildSetupBase(),
                Top = null,
                PreFooter = null,
                Footer = null,
                SecMenu = null,
                Splash = new Splash
                {
                    CdnEnv = null,
                    IndexEng = _model.SplashPageInfo.EnglishHomeUrl,
                    IndexFra = _model.SplashPageInfo.FrenchHomeUrl,
                    TermsEng = ModelBuilder.GetStringForJson(_model.SplashPageInfo.EnglishTermsUrl),
                    TermsFra = ModelBuilder.GetStringForJson(_model.SplashPageInfo.FrenchTermsUrl),
                    NameEng = _model.SplashPageInfo.EnglishName,
                    NameFra = _model.SplashPageInfo.FrenchName,
                    LanguagePrecedence = _model.SplashPageInfo.LanguagePrecedence

                },
                OnCDTSPageFinalized = _model.HTMLBodyElements
            });
        }

        public HtmlString SessionTimeoutControl()
        {
            if (_model.Settings.SessionTimeout.Enabled)
            {
                HtmlString jsonSessionTimeout = JsonSerializationHelper.SerializeToJson(_model.Settings.SessionTimeout);
                return new HtmlString($"<span class='wb-sessto' data-wb-sessto='{jsonSessionTimeout}'></span>");
            }

            return null;
        }

        /// <summary>
        /// Adds a string(html tag) to be included in the page
        /// This is our way off letting the developer add metatags, css and js to their pages.
        /// </summary>
        /// <remarks>we are accepting a string with no validation, therefore it is up to the developer to provide a valid string/html tag</remarks>
        /// <returns>
        /// string hopefully a valid html tag
        /// </returns>
        public static HtmlString HtmlElements(List<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var sb = new StringBuilder();
            foreach (var tag in tags)
            {
                sb.AppendLine(tag);
            }
            return new HtmlString(sb.ToString());
        }

        public HtmlString HtmlHeaderElements() => HtmlElements(_model.HTMLHeaderElements);

        public HtmlString HeaderTitle() => new HtmlString(_model.HeaderTitle);
    }
}
