$ErrorActionPreference = "Stop"

Set-Location -Path $PSScriptRoot

docker build -t salary-comparer .
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

docker run --rm salary-comparer # $args
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

exit 0
