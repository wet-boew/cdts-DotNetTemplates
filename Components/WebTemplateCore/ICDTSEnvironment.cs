namespace GoC.WebTemplate
{
    public interface ICDTSEnvironment
    {
        string Name { get; set; }
        string Path { get; set; }
        string CDN { get; set; }
        string SubTheme { get; set; }
        bool IsVersionRNCombined { get; set; }
        bool IsThemeModifiable { get; set; }
        bool IsSSLModifiable { get; set; }
        string LocalPath { get; set; }
    }
}