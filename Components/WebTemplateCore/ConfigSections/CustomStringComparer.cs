using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.ConfigSections
{
    public class CustomStringComparer : IComparer<string>
    {
        private readonly IComparer<string> _baseComparer;
        public CustomStringComparer(IComparer<string> baseComparer)
        {
            _baseComparer = baseComparer;
        }

        public int Compare(string x, string y)
        {
            if (_baseComparer.Compare(x, y) == 0)
                return 0;

            // "b" comes before everything else
            if (_baseComparer.Compare(x, "b") == 0)
                return -1;
            if (_baseComparer.Compare(y, "b") == 0)
                return 1;

            // "c" comes next
            if (_baseComparer.Compare(x, "c") == 0)
                return -1;
            if (_baseComparer.Compare(y, "c") == 0)
                return 1;

            return _baseComparer.Compare(x, y);
        }
    }
}