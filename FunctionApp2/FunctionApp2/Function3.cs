using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Azure.WebJobs;

namespace FunctionApp2;

public class Function3
{
    private readonly IMessageReceiver _receiver;
    private readonly IBusControl _bus;

    public Function3(IMessageReceiver receiver, IBusControl bus)
    {
        _receiver = receiver;
        _bus = bus;
    }

    [FunctionName("EConsumer3")]
    public async Task Run([ServiceBusTrigger("testCommands", Connection = "ServiceBusConnectionString")] Azure.Messaging.ServiceBus.ServiceBusReceivedMessage command, CancellationToken cancellationToken)
    {
        try
        {
            await _receiver.Handle("testCommands", command, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}