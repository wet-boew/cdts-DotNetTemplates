using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ProjectBuildTask
{
    public class UpdateNuGetHintPath : Task
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
                try
                {
                    // Open the project file
                    var doc = XDocument.Load(FileName);

                    // Add Content Files to an ItemGroup Section
                    ModifyHintPath(doc);

                    // Save the Project XML File
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
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

            // Log.HasLoggedErrors is true if the task logged any errors -- even if they were logged 
            // from a task's constructor or property setter. As long as this task is written to always log an error
            // when it fails, we can reliably return HasLoggedErrors.            
            return !Log.HasLoggedErrors;
        }

        /// <summary>
        /// Create the new Web Config section
        /// </summary>
        /// <returns></returns>
        private void ModifyHintPath(XDocument xProjectDoc)
        {
            var itemGroupNodes = xProjectDoc.Elements(_ns + "Project").Elements(_ns + "ItemGroup").Elements(_ns + "Reference").Elements(_ns + "HintPath").ToList();

            foreach (var item in itemGroupNodes)
            {
                item.Value = item.Value.Replace(@"..\..\", "");
            }

            //foreach (var contentFile in ContentFiles)
            //{
            //    // Create the new Content Tag
            //    var xElementContent = new XElement(_ns + "Content");
            //    xElement.Add(xElementContent);

            //    // Add Attribute
            //    xElementContent.Add(new XAttribute("Include", contentFile.ItemSpec.Substring(contentFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1, contentFile.ItemSpec.Length - contentFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));
            //}

            ////add node after the first ItemGroup.
            //itemGroupElements.Last().AddAfterSelf(xElement);
        }
    }
}
