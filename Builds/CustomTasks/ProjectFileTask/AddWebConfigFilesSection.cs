using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ProjectBuildTask
{

    public class AddWebConfigFilesSection : Task
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        [Required]
        // The project file name
        public string FileName { get; set; }
        
        // The directory name where the file is located
        public string DirectoryName { get; set; }

        [Required]
        // List of Content Files.
        public ITaskItem[] WebConfigFiles { get; set; }

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
                CreateNewWebConfigSection(doc);

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
        /// Create the new Web Config section
        /// </summary>
        /// <returns></returns>
        private void CreateNewWebConfigSection(XDocument xProjectDoc)
        {
            var element = xProjectDoc.Element(_ns + "Project");

            if (element == null) return;

            var itemGroupElements = element.Elements(_ns + "ItemGroup");

            var xElement = new XElement(_ns + "ItemGroup");

            DirectoryName = DirectoryName != null ? @"\" + DirectoryName + @"\" : @"\";

            foreach (var webConfigFile in WebConfigFiles)
            {
                var xElementWebConfig = new XElement(_ns + "None");
                xElement.Add(xElementWebConfig);
              
                if (webConfigFile.GetMetadata("FileName").Contains(".Debug"))
                {
                    xElementWebConfig.Add(new XAttribute("Include", webConfigFile.GetMetadata("FileName") + webConfigFile.GetMetadata("Extension")));
                    xElementWebConfig.Add(new XElement(_ns + "DependentUpon", webConfigFile.GetMetadata("FileName").Replace(".Debug", "") + webConfigFile.GetMetadata("Extension")));

                }
                else if (webConfigFile.GetMetadata("FileName").Contains(".Release"))
                {
                    xElementWebConfig.Add(new XAttribute("Include", webConfigFile.GetMetadata("FileName") + webConfigFile.GetMetadata("Extension")));
                    xElementWebConfig.Add(new XElement(_ns + "DependentUpon", webConfigFile.GetMetadata("FileName").Replace(".Release", "") + webConfigFile.GetMetadata("Extension")));

                }
                else
                {
                    xElementWebConfig.Name = _ns + "Content";
                    //xElementWebConfig.Add(new XAttribute("Include", webConfigFile.GetMetadata("FileName") + webConfigFile.GetMetadata("Extension")));
                    xElementWebConfig.Add(new XAttribute("Include", webConfigFile.ItemSpec.Substring(webConfigFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1, webConfigFile.ItemSpec.Length - webConfigFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));         
                }
            }

            //add node after the first ItemGroup.
            itemGroupElements.Last().AddAfterSelf(xElement);
        }
    }
}