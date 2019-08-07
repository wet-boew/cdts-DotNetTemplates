namespace GoC.WebTemplate.Components.Configs
{
    public interface ICdtsEnvironment
    {
        string Name { get; set; }

        /// <summary>
        /// URL from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// CDNEnv from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        string CDN { get; set; }
        string SubTheme { get; set; }
        bool IsVersionRNCombined { get; set; }
        bool IsEncryptionModifiable { get; set; }

        /// <summary>
        /// The local path to be used during local testing or perfomance testing
        /// Set by application via web.config
        /// </summary>
        string LocalPath { get; set; }

        /// <summary>
        /// Represents the Theme to use to build the age. ex: GCWeb
        /// Set by application via web.config or programmatically
        /// </summary>
        string Theme { get; set; }

        /// <summary>
        /// Text to append to HeaderTitle
        /// </summary>
        string AppendToTitle { get; set; }
        int FooterSectionLimit { get; set; }
        bool CanHaveMultipleContactLinks { get; set; }
        bool CanHaveContactLinkInAppTemplate { get; set; }
    }
}