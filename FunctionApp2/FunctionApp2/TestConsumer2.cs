using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FunctionApp2;

public class TestConsumer2 : IConsumer<Command2>
{
    private readonly ILogger<TestConsumer2> _logger;

    public TestConsumer2(ILogger<TestConsumer2> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<Command2> context)
    {
        _logger.LogWarning($"TestConsumer2 {context.Message.Test}");
    }
}