<#
.SYNOPSIS
	Project tasks
.PARAMETER Compose
	Runs docker-compose.
.PARAMETER Build
	Builds the solution projects.
.PARAMETER Clean
	Cleans all solution project files
.PARAMETER IntegrationTests
    Builds the projects and executes the integration tests.
.PARAMETER NuGetPublish
	Deploys the nuget projects.
.PARAMETER UnitTests
	Builds the projects and runs the unit tests.
.PARAMETER Environment
	The envirponment to build for (Debug or Release), defaults to Debug
.EXAMPLE
	C:\PS> .\project-tasks.ps1 -Build -Debug
#>

# #############################################################################
# Params
#
[CmdletBinding(PositionalBinding = $false)]
Param(
    [switch]$Build,
    [switch]$Clean,
    [switch]$NuGetPublish,
    [switch]$UnitTests,
    [switch]$Quiet,
    [ValidateNotNullOrEmpty()]
    [String]$Environment = "Debug"
)

# #############################################################################
# Settings
#
$Environment = $Environment.ToLowerInvariant()
$Framework = "netstandard2.0"
$NugetFeedUri = "https://www.myget.org/F/envoice/api/v3/index.json"
$NugetKey = $Env:MYGET_KEY_ENVOICE
$NugetVersionSuffix = ""
$ROOT_DIR = (Get-Item -Path ".\" -Verbose).FullName


# #############################################################################
# Welcome message
#
Function Welcome () {

    Write-Host "                     _         " -ForegroundColor "Blue"
    Write-Host "  ___ ___ _  _____  (_)______  " -ForegroundColor "Blue"
    Write-Host " / -_) _ \ |/ / _ \/ / __/ -_) " -ForegroundColor "Blue"
    Write-Host " \__/_//_/___/\___/_/\__/\__/  " -ForegroundColor "Blue"
    Write-Host ""

}


# #############################################################################
# Builds the project
#
Function BuildProject () {

    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"
    Write-Host "+ Building $ProjectName                         " -ForegroundColor "Green"
    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"

    $pubFolder = "bin\$Environment\$Framework\publish"
    Write-Host "Building the project ($Environment) into $pubFolder." -ForegroundColor "Yellow"

    dotnet restore
    dotnet publish -c $Environment -o $pubFolder -v quiet
}


# #############################################################################
# Cleans the project
#
Function CleanAll () {

    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"
    Write-Host "+ Cleaning project                              " -ForegroundColor "Green"
    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"

    dotnet clean
}


# #############################################################################
# Deploys nuget packages to nuget feed
#
Function NugetPublish () {

    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"
    Write-Host "+ Deploying to NuGet feed                       " -ForegroundColor "Green"
    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"

    Write-Host "Using Key: $NugetKey" -ForegroundColor "Yellow"

    Set-Location src

    Get-ChildItem -Filter "*.nuspec" -Recurse -Depth 1 |
        ForEach-Object {

            $packageName = $_.BaseName
            Set-Location $_.BaseName

            If ($NugetVersionSuffix) {

                dotnet pack `
                    -c $Environment `
                    -o ../../.artifacts/nuget `
                    --include-source `
                    --include-symbols `
                    --version-suffix $NugetVersionSuffix

            } Else {

            dotnet pack `
                -c $Environment `
                -o ../../.artifacts/nuget `
                --include-source `
                --include-symbols
        }

        Set-Location ..
    }

    Set-Location $ROOT_DIR
    Set-Location ./.artifacts/nuget


    Write-Host "Publishing Packages to $NugetFeedUri" -ForegroundColor "Yellow"

    dotnet nuget push *.nupkg `
        -k $NugetKey `
        -s $NugetFeedUri

    Set-Location $ROOT_DIR

    Remove-Item .\.artifacts\nuget -Force -Recurse
}

# #############################################################################
# Runs the unit tests
#
Function UnitTests () {

    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"
    Write-Host "+ Running unit tests                            " -ForegroundColor "Green"
    Write-Host "++++++++++++++++++++++++++++++++++++++++++++++++" -ForegroundColor "Green"

    Set-Location test

    Get-ChildItem -Directory -Filter "*.Tests*" |
        ForEach-Object {
        Set-Location $_.FullName # or whatever
        dotnet test
        Set-Location ..
    }

}

# #############################################################################
# Call the correct Function for the switch
#

If(!$Quiet) { Welcome }

If ($Build) {
    BuildProject
}
ElseIf ($Clean) {
    CleanAll
}
ElseIf ($NuGetPublish) {
    NugetPublish
}
ElseIf ($UnitTests) {
    UnitTests
}

# #############################################################################
