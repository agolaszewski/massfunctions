using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FunctionApp2;

public class TestConsumer : IConsumer<Command>
{
    private readonly ILogger<TestConsumer> _logger;

    public TestConsumer(ILogger<TestConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<Command> context)
    {
        _logger.LogInformation($"TestConsumer {context.Message.Test}");
    }
}