using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace DiffCopy
{
    /// <summary>
    /// Provides an object that compares files based on the sha1 hash of the file.
    /// </summary>
    public class Sha1Comparer : DirectoryComparer
    {
        /// <summary>
        /// 	<para>Initializes an instance of the <see cref="Sha1Comparer"/> class.</para>
        /// </summary>
        public Sha1Comparer() { }

        /// <summary>
        /// Compares the file contents for two directories and returns information about the differences.
        /// </summary>
        /// <param name="source">The source directory to compare to the destination.</param>
        /// <param name="destination">The destination directory where, ultimately, files would be copied.</param>
        /// <returns>A <see cref="ComparisonResult"/> object that contains the result of the comparison operation.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public override ComparisonResult Compare(string source, string destination)
        {
            base.Scan(source, destination);

            var modifiedFiles = new List<string>();
            string sourceHash = null;
            string destinationHash = null;

            // compare the overlap files
            foreach (var file in base.FilesToCompare)
            {
                using (var sourceFile = new FileStream(string.Concat(source, file), FileMode.Open, FileAccess.Read))
                {
                    using (var destinationFile = new FileStream(string.Concat(destination, file), FileMode.Open, FileAccess.Read))
                    {
                        using (var cryptoProvider = new SHA1CryptoServiceProvider())
                        {
                            sourceHash = BitConverter.ToString(cryptoProvider.ComputeHash(sourceFile));
                            destinationHash = BitConverter.ToString(cryptoProvider.ComputeHash(destinationFile));

                            // if the hashes don't match, the files are different. add them to the return set.
                            if (!sourceHash.Equals(destinationHash, StringComparison.OrdinalIgnoreCase))
                            {
                                modifiedFiles.Add(string.Concat(source, file));
                            }
                        }
                    }
                }
            }

            var newFiles = new List<string>(base.NewFiles);
            var notInSourceFiles = new List<string>(base.NotInSource);
            var result = new ComparisonResult(newFiles, modifiedFiles, notInSourceFiles);

            return result;
        }
    }
}
