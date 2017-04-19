namespace GoC.WebTemplate.ConfigSections
{
    public interface ICDTSEnvironmentElementProxy
    {
        string LocalPath { get; }
        string Key { get; }
        string Path { get; }
        string Env { get; }
    }
}