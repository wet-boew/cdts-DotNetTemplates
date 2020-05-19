$newVersion = $args[0]
$filePath = $args[1]

    $assemblyPatternCS ='\[assembly: AssemblyInformationalVersion\("(.*)"\)\]'
    $assemblyPatternVB ='\<Assembly: AssemblyInformationalVersion\("(.*)"\)\>'

    (Get-Content $filePath) | ForEach-Object{
    if($_ -match $assemblyPatternCS){
        '[assembly: AssemblyInformationalVersion("{0}")]' -f $newVersion
    } elseif($_ -match $assemblyPatternVB) {
        '<Assembly: AssemblyInformationalVersion("{0}")>' -f $newVersion    
    } else {
        # Output line as is
        $_
    }
} | Set-Content $filePath
