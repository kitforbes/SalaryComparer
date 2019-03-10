$ErrorActionPreference = "Stop"

Set-Location -Path $PSScriptRoot

dotnet restore
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

dotnet build
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

dotnet run --project .\SalaryComparer # $args
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

exit 0
