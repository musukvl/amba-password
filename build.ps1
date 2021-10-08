#build dotnet tool
$version = "1.0.1"

dotnet pack Amba.PasswordGenerator/Amba.PasswordGenerator.csproj --configuration Release --output ./publish/tool `
    -p:PackAsTool=true `
    -p:ToolCommandName=amba-password `
    -p:Version=$version

#build single file
dotnet publish Amba.PasswordGenerator/Amba.PasswordGenerator.csproj `
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

Move-Item -Path "./publish/exe/Amba.PasswordGenerator.exe" "./publish/exe/amba-password.exe" -Force