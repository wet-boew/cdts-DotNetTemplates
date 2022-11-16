$newVersion = $args[0]
$filePath = $args[1]

Write-Host "core-version: Change version of ${filePath} to : [${newVersion}]"

(Get-Content $filePath) | ForEach-Object{
    if($_ -match '<Version>(.*)</Version>'){
        '<Version>{0}</Version>' -f $newVersion
    } else {
        # Output line as is
        $_
    }
} | Set-Content $filePath
