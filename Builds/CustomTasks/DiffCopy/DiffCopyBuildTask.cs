using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DiffCopy
{
    /// <summary>
    /// A custom MSBuild Task object that is used to compare two directories for efficient deployment of large numbers of files.
    /// </summary>
    public class DiffCopyBuildTask : Task
    {
        #region constructors

        #endregion

        #region public properties

        /// <summary>
        /// Gets or Sets the source directory to compare to the destination directory. Must be an actual path on disk.
        /// </summary>
        [Required]
        public string SourceDirectory { get; set; }

        /// <summary>
        /// Gets or Sets the destination directory that the source directory will be compared to. Must be an actual path on disk.
        /// </summary>
        [Required]
        public string DestinationDirectory { get; set; }

        [Output]
        public int NewFilesCount { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="ITaskItem"/> that contains the files that exist in the source but not the destination directory.
        /// </summary>
        [Output]
        public ITaskItem[] NewFiles { get; set; }

        /// <summary>
        /// Gets or Sets the number of modified files that exist in both the source and destination directories that are different.
        /// </summary>
        [Output]
        public int ModifiedFilesCount { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="ITaskItem[]"/> that contains the files that exist in both the source and destination directories that are different.
        /// </summary>
        [Output]
        public ITaskItem[] ModifiedFiles { get; set; }

        /// <summary>
        /// Gets or Sets the number of files that exist in the destination, but not the source directory.
        /// </summary>
        [Output]
        public int NotInSourceFilesCount { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="ITaskItem[]"/> that contains the files that exist in the destination, but not the source directory.
        /// </summary>
        [Output]
        public ITaskItem[] NotInSourceFiles { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="ITaskItem"/> that contains the files that exist in the source but not the destination directory.
        /// </summary>
        public string[] ExcludeExtensions { get; set; }

        #endregion

        #region public methods

        /// <summary>
        ///     Executes the Task.
        /// </summary>
        /// <returns>true if the task successfully executed; otherwise, false.</returns>
        public override bool Execute()
        {

            if (!Directory.Exists(SourceDirectory))
            {
                // can't run if the directory doesn't exist
                Log.LogError("The source directory specified does not exist.");
                return false;
            }
            if (!Directory.Exists(DestinationDirectory))
            {
                // can't run if the directory doesn't exist
                Log.LogError("The destination directory specified does not exist.");
                return false;
            }

            Log.LogMessage(MessageImportance.Normal, "Comparing {0} to {1}", SourceDirectory, DestinationDirectory);

            var comparer = new ByteStreamComparer();
            var result = comparer.Compare(SourceDirectory, DestinationDirectory);

            try
            {
                HandleNewFiles(result, SourceDirectory);
                HandleModifiedFiles(result, SourceDirectory);
                HandleNotInSourceFiles(result, DestinationDirectory);
            }
            catch (Exception e)
            {
                Log.LogError(string.Format("The build task failed. The message was '{0}'", e.StackTrace));
                return false;
            }

            return true;
        }

        #endregion

        #region private methods

        private void HandleNewFiles(ComparisonResult result, string rootPath)
        {
            var newFiles = new List<ITaskItem>();
            Log.LogMessage(MessageImportance.High, "New Files", null);

            HandleFiles(result.NewFiles, newFiles, rootPath);

            NewFiles = newFiles.ToArray();

            NewFilesCount = newFiles.Count();
        }

        private void HandleModifiedFiles(ComparisonResult result, string rootPath)
        {
            var modifiedFiles = new List<ITaskItem>();
            Log.LogMessage(MessageImportance.High, "Modified Files", null);

            HandleFiles(result.ModifiedFiles, modifiedFiles, rootPath);

            ModifiedFiles = modifiedFiles.ToArray();

            ModifiedFilesCount = result.ModifiedFiles.Count();
        }

        private void HandleNotInSourceFiles(ComparisonResult result, string rootPath)
        {
            var notInSourceFiles = new List<ITaskItem>();
            Log.LogMessage(MessageImportance.High, "Files that exist on destination but not source", null);

            HandleFiles(result.NotInSource, notInSourceFiles, rootPath);

            NotInSourceFiles = notInSourceFiles.ToArray();

            NotInSourceFilesCount = result.NotInSource.Count();
        }

        private void HandleFiles(IEnumerable<string> results, List<ITaskItem> destination, string rootPath)
        {
            if (!results.Count().Equals(0))
            {
                foreach (var file in results)
                {                 
                    var info = new FileInfo(file);
                    var modified = info.LastWriteTime;
                    var created = info.CreationTime;
                    var accessed = info.LastAccessTime;
                    var fullPath = Path.GetFullPath(file);

                    if (ExcludeExtensions != null && ExcludeExtensions.Contains(info.Extension)) continue;

                    Log.LogMessage(MessageImportance.Normal, "\t{0}", file);

                    var metadata = new Dictionary<string, string>();
                    metadata.Add("FullPath", fullPath);
                    metadata.Add("RootDir", Path.GetPathRoot(file));
                    if (info.Directory != null) metadata.Add("DirectoryName", info.Directory.Name);
                    metadata.Add("Filename", Path.GetFileNameWithoutExtension(file));
                    metadata.Add("Extension", Path.GetExtension(file));
                    metadata.Add("ModifiedTime", modified.ToString("yyyy-MM-dd hh:mm:ss.FFFFFFF"));
                    metadata.Add("CreatedTime", created.ToString("yyyy-MM-dd hh:mm:ss.FFFFFFF"));
                    metadata.Add("AccessedTime", accessed.ToString("yyyy-MM-dd hh:mm:ss.FFFFFFF"));

                    var recursiveDirectory = file.Replace(rootPath, string.Empty)
                        .Replace(Path.GetFileName(file), string.Empty);
                    metadata.Add("RecursiveDir", recursiveDirectory);
                    metadata.Add("TFSFormatPath", recursiveDirectory.Replace(@"\", @"/"));

                    // NOTE: The following well-known meta data aren't implemented at this time.
                    // RelativeDir, Directory, Identity

                    var item = new TaskItem(file, metadata);
                    destination.Add(item);
                }
            }
                
            if (destination.Count == 0)
                {
                    Log.LogMessage(MessageImportance.Normal, "\tNo files found.");
                }                
            }

        #endregion
        }
    }
