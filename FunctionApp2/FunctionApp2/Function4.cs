using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Azure.WebJobs;

namespace FunctionApp2;

public class Function4
{
    private readonly IMessageReceiver _receiver;
    private readonly IBusControl _bus;

    public Function4(IMessageReceiver receiver, IBusControl bus)
    {
        _receiver = receiver;
        _bus = bus;
    }

    [FunctionName("EConsumer4")]
    public async Task Run([ServiceBusTrigger("functionapp2/eventreceived2", "default", Connection = "ServiceBusConnectionString")] Azure.Messaging.ServiceBus.ServiceBusReceivedMessage command, CancellationToken cancellationToken)
    {
        try
        {
            await _receiver.Handle("functionapp2/eventreceived2", "default", command, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}