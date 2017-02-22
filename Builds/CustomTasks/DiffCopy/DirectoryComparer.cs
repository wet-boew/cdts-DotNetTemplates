using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiffCopy
{
    /// <summary>
    /// Object that provides functionality to compare a source directory to a destination directory.
    /// </summary>
    public abstract class DirectoryComparer : IDirectoryComparer
    {
        #region constructors

        /// <summary>
        /// 	<para>Initializes an instance of the <see cref="DirectoryComparer"/> class.</para>
        /// </summary>
        public DirectoryComparer() { }

        #endregion

        #region protected properties

        /// <summary>
        /// Gets an <see cref="IEnumerable<string>"/> of files from the source directory.
        /// </summary>
        protected IEnumerable<string> SourceFiles { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable<string>"/> of files from the destination directory.
        /// </summary>
        protected IEnumerable<string> DestinationFiles { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable<string>"/> of files that need to be compared.
        /// </summary>
        protected IEnumerable<string> FilesToCompare { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable<string>"/> of files that are determined to be new.
        /// </summary>
        protected IEnumerable<string> NewFiles { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable<string>"/> of files that are not in the source directory, but are in the destination directory.
        /// </summary>
        protected IEnumerable<string> NotInSource { get; private set; }

        #endregion

        /// <summary>
        /// Scans the two directories to create the various file lists used.
        /// </summary>
        /// <param name="source">The source directory to compare to the destination directory. Must be an actual path on disk.</param>
        /// <param name="destination">The destination directory that the source directory will be compared to. Must be an actual path on disk.</param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        protected void Scan(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new DirectoryNotFoundException(string.Format("{0} was not found.", source));
            }
            if (string.IsNullOrEmpty(destination))
            {
                throw new DirectoryNotFoundException(string.Format("{0} was not found.", destination));
            }
            if (!Directory.Exists(source))
            {
                throw new DirectoryNotFoundException(string.Format("{0} was not found.", source));
            }
            if (!Directory.Exists(destination))
            {
                throw new DirectoryNotFoundException(string.Format("{0} was not found.", destination));
            }

            this.SourceFiles = DirectoryComparer.Traverse(source);
            this.DestinationFiles = DirectoryComparer.Traverse(destination);

            // need to strip the root directories from the file lists above. This will give us relative paths for comparison.
            var strippedSourceFiles = this.SourceFiles.Select(m => m = m.Replace(source, string.Empty));
            var strippedDestinationFiles = this.DestinationFiles.Select(m => m = m.Replace(destination, string.Empty));

            // these are the files that need to be compared. Anything from inside source that doesn't exist in here
            // needs to be copied, anything from destination could be deleted. Perhaps a "prune" argument.
            this.FilesToCompare = strippedSourceFiles.Intersect(strippedDestinationFiles);

            // anything not overlapping needs to be added.
            var newFiles = strippedSourceFiles.Except(strippedDestinationFiles);
            this.NewFiles = newFiles.Select(m => m = string.Concat(source, m));

            // anything in the destination directory that isn't in the source directory. This prunes files from
            // the destination that don't exist in the source.
            var toDeleteFiles = strippedDestinationFiles.Except(strippedSourceFiles);
            this.NotInSource = toDeleteFiles.Select(m => m = string.Concat(destination, m));
        }

        private static IEnumerable<string> Traverse(string root)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(string.Format("{0} was not found.", root));
            }

            var directories = new Stack<string>();
            directories.Push(root);

            string[] subDirectories = null;
            string[] files = null;
            while (directories.Count > 0)
            {
                var currentDirectory = directories.Pop();

                try
                {
                    subDirectories = Directory.GetDirectories(currentDirectory);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                try
                {
                    files = Directory.GetFiles(currentDirectory);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                foreach (var file in files)
                {
                    yield return file;
                }

                foreach (var directory in subDirectories)
                {
                    directories.Push(directory);
                }
            }
        }

        #region IDirectoryComparer Members

        /// <summary>
        /// Compares the file contents for two directories and returns information about the differences.
        /// </summary>
        /// <param name="source">The source directory to compare to the destination.</param>
        /// <param name="destination">The destination directory where, ultimately, files would be copied.</param>
        /// <returns>A <see cref="ComparisonResult"/> object that contains the result of the comparison operation.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public abstract ComparisonResult Compare(string source, string destination);

        #endregion
    }
}
