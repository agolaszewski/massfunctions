using MassTransit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionApp2.Startup))]

namespace FunctionApp2

{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<Function3>();
            builder.Services.AddScoped<Function2>();
            builder.Services.AddScoped<Function>();
            builder.Services.AddMassTransitForAzureFunctions(x =>
            {
                x.AddSagaStateMachine<TestSaga, TestState>().InMemoryRepository();
                x.AddConsumer<TestConsumer>();
                x.AddConsumer<TestConsumer2>();
                
            }, "ServiceBusConnectionString", (context, cfg) =>
            {
                cfg.ReceiveEndpoint("testCommands", x =>
                {
                    x.ConfigureConsumer<TestConsumer>(context);
                    x.ConfigureConsumer<TestConsumer2>(context);
                    x.ConfigureConsumeTopology = false;
                });

                cfg.SubscriptionEndpoint("default", "functionapp2/eventreceived", x =>
                {
                    x.ConfigureSaga<TestState>(context);
                });

                cfg.SubscriptionEndpoint("default", "functionapp2/eventreceived2", x =>
                {
                    x.ConfigureSaga<TestState>(context);
                });
            });
        }
    }
}