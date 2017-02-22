using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;


namespace ProjectBuildTask
{
    public class GetNuGetPackagesList : Task
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        [Required]
        // The project file name
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="ITaskItem"/> that contains all the packages needed for the project.
        /// </summary>
        [Output]
        public ITaskItem[] PackageNameItems { get; set; }

        /// <summary>
        /// Execute is part of the Microsoft.Build.Framework.ITask interface.
        /// When it's called, any input parameters have already been set on the task's properties.
        /// It returns true or false to indicate success or failure.
        /// </summary>
        public override bool Execute()
        {
            try
            {
                var taskItems = new List<ITaskItem>();

                // open the project file
                var doc = XDocument.Load(FileName);
                var xElement = doc.Element(_ns + "Project");

                if (xElement != null)
                {
                    var itemGroupNodes = xElement.Elements(_ns + "ItemGroup").Elements(_ns + "Reference").Elements(_ns + "HintPath").ToList();

                    //Remove all ItemGroup that does not contains the Reference or Folder tag inside.
                    foreach (var item in itemGroupNodes)
                    {
                        var metadata = new Dictionary<string, string>();
                        metadata.Add("FilePath", item.Value.Substring(item.Value.LastIndexOf(@"packages\", StringComparison.Ordinal) + 9,item.Value.Substring(item.Value.LastIndexOf(@"packages\", StringComparison.Ordinal) + 9).IndexOf(@"\", StringComparison.Ordinal)));
                        var item1 = new TaskItem(item.Name.LocalName, metadata);
                        taskItems.Add(item1);

                    }
                }
                PackageNameItems = taskItems.ToArray();

            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

            // Log.HasLoggedErrors is true if the task logged any errors -- even if they were logged 
            // from a task's constructor or property setter. As long as this task is written to always log an error
            // when it fails, we can reliably return HasLoggedErrors.            
            return !Log.HasLoggedErrors;
        }
    }
}
