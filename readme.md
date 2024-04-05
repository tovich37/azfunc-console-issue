# azfunc-console-issue

## Instructions
Must add a `local.settings.json` file to the project root with the following content to run the project locally:
```
{
  "IsEncrypted": false,
  "Values": {
    "APPLICATIONINSIGHTS_CONNECTION_STRING": "[REDACTED]",
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  }
}
```