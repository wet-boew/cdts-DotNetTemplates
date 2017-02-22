using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ProjectBuildTask
{

    public class CleanProject : Task
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        [Required]
        // The project file name
        public string FileName { get; set; }

        /// <summary>
        /// Execute is part of the Microsoft.Build.Framework.ITask interface.
        /// When it's called, any input parameters have already been set on the task's properties.
        /// It returns true or false to indicate success or failure.
        /// </summary>
        public override bool Execute()
        {
            try
            {                
                // open the project file
                var doc = XDocument.Load(FileName);
                var xElement = doc.Element(_ns + "Project");

                if (xElement != null)
                {
                    var itemGroupNodes = xElement.Elements(_ns + "ItemGroup").Where(x => x.Element(_ns + "Reference") == null && 
                                                                                         x.Element(_ns + "Folder") == null)
                                                                                         .ToList();
                    
                    //Remove all ItemGroup that does not contains the Reference or Folder tag inside.
                    foreach (var item in itemGroupNodes)
                    {
                        item.Remove();
                    }
                }

                doc.Save(FileName);

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