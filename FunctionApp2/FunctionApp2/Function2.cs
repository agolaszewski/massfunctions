using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Azure.WebJobs;

namespace FunctionApp2;

public class Function2
{
    private readonly IMessageReceiver _receiver;
    private readonly IBusControl _bus;

    public Function2(IMessageReceiver receiver, IBusControl bus)
    {
        _receiver = receiver;
        _bus = bus;
    }

    [FunctionName("EConsumer2")]
    public async Task Run([ServiceBusTrigger("functionapp2/eventreceived", "default", Connection = "ServiceBusConnectionString")] Azure.Messaging.ServiceBus.ServiceBusReceivedMessage command, CancellationToken cancellationToken)
    {
        try
        {
            await _receiver.Handle("functionapp2/eventreceived", "default", command, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}