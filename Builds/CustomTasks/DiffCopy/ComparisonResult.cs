using System;
using System.Collections.Generic;

namespace DiffCopy
{
    /// <summary>
    /// Provides an object that represents the state of a directory comparison operation.
    /// </summary>
    public class ComparisonResult
    {
        /// <summary>
        /// 	<para>Initializes an instance of the <see cref="ComparisonResult"/> class.</para>
        /// </summary>
        /// <param name="newFiles">The <see cref="ITaskItem[]"/> that contains the files that exist in the source but not the destination directory.</param>
        /// <param name="modifiedFiles">The <see cref="ITaskItem[]"/> that contains the files that exist in both the source and destination directories that are different.</param>
        /// <param name="notInSource">The <see cref="ITaskItem[]"/> that contains the files that exist in the destination, but not the source directory.</param>
        public ComparisonResult(IEnumerable<string> newFiles, IEnumerable<string> modifiedFiles, IEnumerable<string> notInSource)
        {
            this.NewFiles = newFiles;
            this.ModifiedFiles = modifiedFiles;
            this.NotInSource = notInSource;
        }

        /// <summary>
        /// Gets the <see cref="ITaskItem[]"/> that contains the files that exist in the source but not the destination directory.
        /// </summary>
        public IEnumerable<string> NewFiles { get; private set; }

        /// <summary>
        /// Gets the <see cref="ITaskItem[]"/> that contains the files that exist in both the source and destination directories that are different.
        /// </summary>
        public IEnumerable<string> ModifiedFiles { get; private set; }

        /// <summary>
        /// Gets the <see cref="ITaskItem[]"/> that contains the files that exist in the destination, but not the source directory.
        /// </summary>
        public IEnumerable<string> NotInSource { get; private set; }
    }
}
