In Future:
    1) Separate .http tests to another project

Using:
    1) For local run preferable way is running application through the Rider:
        1.1) Choose any function. Move mouse near the title and find Lightning icon and click it. Do it to all functions or that you just want to test
        1.2) Find .http api test file which is describing test case that you need. Find Run all requests in file and click it
    2) For Azure when you try to setup your infrastructure you should:

Potential Issues:
    1) On running CategoryService from CLI (NOT Rider -> Functions -> Debug all functions) like "func start -build" the line
        <FunctionsEnableWorkerIndexing>False</FunctionsEnableWorkerIndexing> (https://github.com/Azure/azure-functions-dotnet-worker/issues/2072) should be added to the CategoryService.csproj file
    2) To publish category service into azure function app firstly should be checked that:
        func app -> Configuration -> General settings -> SCM Basic Auth Publishing Credentials is ON
        (https://learn.microsoft.com/en-us/answers/questions/137869/publish-profile-publishurl-needs-to-be-adjusted-af)
    3) By default .net cli uses the MINOR version of SDK so if there are installed net6.0 and net8.0 it will use 6.0 even the project reference 8.0. So should be added global.json with 8.0 version to the project and it will force cli to build project under 8.0:
        3.1) dotnet new globaljson --sdk-version 8.0.303
        3.2) func start or func start -build
    4) Should not forget that local.settings.json is only for development purposes and will not copy to the service. Settings from it should be copied to either KeyVault, Appication Configuration or Environment variables
    5) Take care of https and http. If functions return 404 but every configuration is okay than should be checked https only option. If it is ON but no certificates were set than https only should be switched to OFF