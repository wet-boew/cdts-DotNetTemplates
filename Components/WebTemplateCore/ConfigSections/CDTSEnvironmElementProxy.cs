namespace GoC.WebTemplate.ConfigSections
{
    public class CDTSEnvironmElementProxy : ICDTSEnvironmentElementProxy
    {
        private readonly CDTSEnvironmentElement _cdtsEnvironmentElement;

        public CDTSEnvironmElementProxy(CDTSEnvironmentElement cdtsEnvironmentElement)
        {
            _cdtsEnvironmentElement = cdtsEnvironmentElement;
        }

        public string LocalPath => _cdtsEnvironmentElement.LocalPath;

        public string Key => _cdtsEnvironmentElement.Key;
        public string Path => _cdtsEnvironmentElement.Path;
        public string Env => _cdtsEnvironmentElement.Env;
    }
}