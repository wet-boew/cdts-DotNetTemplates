$projectFolder = $args[0]
$projectName = $args[1]
$workingFolder = Resolve-Path -Path "./"

$projectFolder = "$workingFolder" + "/" + "$projectFolder"

  
    $Version = ''

    #Get Package Version Number
    if ($ProjectExtension -eq "vbproj") { $content = "$projectFolder\My Project\AssemblyInfo.vb" }
    else { $content = "$projectFolder\Properties\AssemblyInfo.cs" }
    $pattern ='AssemblyInformationalVersion\("(.*?)"\)'
    (Get-Content $content) | ForEach-Object{
        if($_ -match $pattern){
            $Version = $matches[1]
        }
    }

    #Compare the packages listed in the config file and the dependencies in the nuspec file
    #If any depenedency is missing, it will be added in the nuspec file

    #Get all depenedencies
    $xmlFile = "$projectFolder\$projectName.nuspec"
    $xml = [xml](Get-Content -Path $xmlFile)
    $DependenciesNode = $xml.SelectSingleNode('//package/metadata/dependencies')
    $DependenciesNullCheck = $false
    if ($DependenciesNode -eq $null) { $DependenciesNullCheck = $true }
    
    #Get all packages
    $ConfigFile = "$projectFolder\packages.config"
    if (Test-Path -Path $ConfigFile -PathType Leaf) {
      $ConfigXML = [xml](Get-Content -Path $ConfigFile)
      $PackagesNode = $ConfigXML.SelectSingleNode('//packages')

      ForEach ($package in $PackagesNode.ChildNodes){
          $found = $false
          if ($DependenciesNode -ne $null)
          {
              ForEach ($dependency in $DependenciesNode.ChildNodes){
                  if ($package.id -eq $dependency.id)
                  {
                      $dependency.version = $package.version
                      $found = $true
                      break
                  }
              }
          }

          if (-Not $found) 
          {
              if ($package.developmentDependency -eq $null -or $package.developmentDependency -eq "false")
              {
                  #Create the dependency node
                  $AddDepenency = $xml.CreateNode("element", "dependency", "")

                  #Create the ID attribute
                  $IDAttribute = $xml.CreateAttribute("id")
                  $IDAttribute.Value = $package.id
                  $AddDepenency.Attributes.Append($IDAttribute)

                  #Create the version attribute
                  $VersionAttribute = $xml.CreateAttribute("version")
                  $VersionAttribute.Value = $package.version
                  $AddDepenency.Attributes.Append($VersionAttribute)

                  #Add new node to file
                  if ($DependenciesNullCheck -eq $false) { $xml.SelectSingleNode('//package/metadata/dependencies').AppendChild($AddDepenency) }
                  else 
                  { 
                      $MetadataNode = $xml.SelectSingleNode('//package/metadata')
                      $NewDependenciesNode = $xml.CreateNode("element", "dependencies", "")
                      $MetadataNode.InsertAfter($NewDependenciesNode, $MetadataNode.LastChild)
                      $xml.SelectSingleNode('//package/metadata/dependencies').AppendChild($AddDepenency)
                      $DependenciesNullCheck = $false
                  }

              }
         }
      }
    }

    #Update Version Number in Nuspec file
    $Node = $xml.SelectSingleNode('//package/metadata/version')
    $Node.InnerText = "$Version"
    $Nodes = $xml.SelectNodes("//package/metadata/dependencies/dependency[starts-with(@id, 'GoC.WebTemplate-')]")
    ForEach ($nd in $Nodes){
        $nd.version = "$Version"
    }
    $xml.Save($xmlFile)
