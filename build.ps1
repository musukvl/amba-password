#build dotnet tool
$version = "1.0.1"
$appName = "Amba.PasswordGenerator"
$toolName = "amba-password"
$csprojPath = "$appName/$appName.csproj"

dotnet pack $csprojPath --configuration Release --output ./publish/tool `
    -p:PackAsTool=true `
    -p:ToolCommandName=$toolName `
    -p:Version=$version

#build single file
dotnet publish $csprojPath `
    --configuration Release `
    -r win10-x64 `
    --output ./publish/exe  `
    --self-contained true `
    -p:Version=$version  `
    -p:PublishSingleFile=true `
    -p:PublishTrimmed=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:DebugSymbols=false `
    -p:CopyOutputSymbolsToPublishDirectory=false

Move-Item -Path "./publish/exe/$appName.exe" "./publish/exe/$toolName.exe" -Force