using System;
using System.Collections.Generic;

namespace DiffCopy
{
    /// <summary>
    /// Provides an interface that Directory comparison implementations can implement.
    /// </summary>
    public interface IDirectoryComparer
    {
        /// <summary>
        /// Compares the file contents for two directories and returns information about the differences.
        /// </summary>
        /// <param name="source">The source directory to compare to the destination.</param>
        /// <param name="destination">The destination directory where, ultimately, files would be copied.</param>
        /// <returns>A <see cref="ComparisonResult"/> object that contains the result of the comparison operation.</returns>
        ComparisonResult Compare(string source, string destination);
    }
}
