using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ProjectBuildTask
{    

    public class AddContentFilesSection : Task
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        [Required]
        // The project file name
        public string FileName { get; set; }
        
        // The directory name where the file is located
        public string DirectoryName { get; set; }

        [Required]
        // List of Content Files.
        public ITaskItem[] ContentFiles { get; set; }

        /// <summary>
        /// Execute is part of the Microsoft.Build.Framework.ITask interface.
        /// When it's called, any input parameters have already been set on the task's properties.
        /// It returns true or false to indicate success or failure.
        /// </summary>
        public override bool Execute()
        {
            try
            {                
                // Open the project file
                var doc = XDocument.Load(FileName);

                // Add Content Files to an ItemGroup Section
                CreateNewContent(doc);

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

        /// <summary>
        /// Create the new Content section
        /// </summary>
        /// <returns></returns>
        private void CreateNewContent(XDocument xProjectDoc)
        {
            var element = xProjectDoc.Element(_ns + "Project");
            if (element == null) return;

            var itemGroupElements = element.Elements(_ns + "ItemGroup");
            var xElement = new XElement(_ns + "ItemGroup");

            DirectoryName = DirectoryName != null ? @"\" + DirectoryName + @"\" : @"\";

            foreach (var contentFile in ContentFiles)
            {
                // Create the new Content Tag
                var xElementContent = new XElement(_ns + "Content");
                xElement.Add(xElementContent);

                // Add Attribute
                xElementContent.Add(new XAttribute("Include", contentFile.ItemSpec.Substring(contentFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1, contentFile.ItemSpec.Length - contentFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));         
            }

            //add node after the first ItemGroup.
            itemGroupElements.Last().AddAfterSelf(xElement);
        }
    }
}