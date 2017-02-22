using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ProjectBuildTask
{

    public class AddCompileFilesSection : Task
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/developer/msbuild/2003";

        [Required]
        // The project file name
        public string FileName { get; set; }
        
        // The directory name where the file is located
        public string DirectoryName { get; set; }

        [Required]
        // The project type - MVC or ASPX
        public string ProjectType { get; set; }

        [Required]
        // List of Content Files.
        public ITaskItem[] CompileFiles { get; set; }

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

                switch (ProjectType.ToUpper())
                {
                    case "ASPX":
                        // Add Compile Files to an ItemGroup Section
                        CreateNewCompileSection(doc);
                        break;
                    case "MVC":
                        CreateNewCompileSectionForMvc(doc);
                        break;
                    default:
                        Log.LogError("Project Type not recognized");                        
                        break;
                }

                // Save the Project XML File
                if (!Log.HasLoggedErrors) doc.Save(FileName);

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
        /// Create the new section for the compilation section
        /// </summary>
        /// <returns></returns>
        private void CreateNewCompileSection(XDocument xProjectDoc)
        {
            var element = xProjectDoc.Element(_ns + "Project");

            if (element == null) return;

            var itemGroupElements = element.Elements(_ns + "ItemGroup");
            var xElement = new XElement(_ns + "ItemGroup");

            DirectoryName = DirectoryName != null ? @"\" + DirectoryName + @"\" : @"\";

            foreach (var compileFile in CompileFiles)
            {
                var xElementCompile = new XElement(_ns + "Compile");
                xElement.Add(xElementCompile);
                if (compileFile.GetMetadata("FileName").Contains(".aspx"))
                {
                    // Start element for the aspx file
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));
                    xElementCompile.Add(new XElement(_ns + "DependentUpon",
                        compileFile.GetMetadata("FileName")
                            .Substring(0,
                                compileFile.GetMetadata("FileName").LastIndexOf(".aspx", StringComparison.Ordinal)) +
                        ".aspx"));
                    if (!compileFile.GetMetadata("FileName").Contains(".designer"))
                    {
                        xElementCompile.Add(new XElement(_ns + "SubType", "ASPXCodeBehind"));    
                    }                    
                }
                else if (compileFile.GetMetadata("FileName").Contains(".master"))
                {
                    // Start element for the aspx file
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));
                    xElementCompile.Add(new XElement(_ns + "DependentUpon",
                        compileFile.GetMetadata("FileName")
                            .Substring(0,
                                compileFile.GetMetadata("FileName").LastIndexOf(".master", StringComparison.Ordinal)) + ".master"));
                    xElementCompile.Add(new XElement(_ns + "SubType", "ASPXCodeBehind"));

                }
                else if (compileFile.GetMetadata("FileName") == "AssemblyInfo")
                {
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));                    
                }
                else 
                {
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));
                    xElementCompile.Add(new XElement(_ns + "SubType", "ASPXCodeBehind"));

                }
            }

            //add node after the first ItemGroup.
            itemGroupElements.Last().AddAfterSelf(xElement);
        }

        /// <summary>
        /// Create the new section for the compilation section
        /// </summary>
        /// <returns></returns>
        private void CreateNewCompileSectionForMvc(XDocument xProjectDoc)
        {
            var element = xProjectDoc.Element(_ns + "Project");

            if (element == null) return;

            var itemGroupElements = element.Elements(_ns + "ItemGroup");
            var xElement = new XElement(_ns + "ItemGroup");

            DirectoryName = DirectoryName != null ? @"\" + DirectoryName + @"\" : @"\";

            foreach (var compileFile in CompileFiles)
            {
                var xElementCompile = new XElement(_ns + "Compile");
                xElement.Add(xElementCompile);
                if (compileFile.GetMetadata("FileName").Contains(".asax"))
                {
                    // Start element for the aspx file
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));
                    xElementCompile.Add(new XElement(_ns + "DependentUpon",
                        compileFile.GetMetadata("FileName")
                            .Substring(0,
                                compileFile.GetMetadata("FileName").LastIndexOf(".asax", StringComparison.Ordinal)) + ".asax"));
                }                
                else
                {
                    xElementCompile.Add(new XAttribute("Include",
                        compileFile.ItemSpec.Substring(
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) + 1,
                            compileFile.ItemSpec.Length -
                            compileFile.ItemSpec.LastIndexOf(DirectoryName, StringComparison.Ordinal) - 1)));                    
                }
            }
            //add node after the first ItemGroup.
            itemGroupElements.Last().AddAfterSelf(xElement);
        }
    }
}
