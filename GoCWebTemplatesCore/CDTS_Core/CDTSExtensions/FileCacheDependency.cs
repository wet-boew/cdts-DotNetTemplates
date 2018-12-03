namespace CDTS_Core.CDTSExtensions
{
    public class FileCacheDependency
    {
        public FileCacheDependency(string filename)
        {
            FileName = filename;
        }

        public string FileName { get; }
    }
}
