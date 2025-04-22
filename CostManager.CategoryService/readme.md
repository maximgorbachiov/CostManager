In Future:
    1) Add .http environments to test different azure function stagings

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