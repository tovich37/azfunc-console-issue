using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1;

public class Function1(ILogger<Function1> logger)
{
    private readonly ILogger _logger = logger;

    [Function("Function1")]
    public void Run([TimerTrigger("*/15 * * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {DateTime}", DateTime.Now);

        _logger.LogError(new Exception("This is an exception"), "This is an error message.");
    }
}