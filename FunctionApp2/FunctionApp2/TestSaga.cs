using MassTransit;
using Microsoft.Extensions.Logging;

namespace FunctionApp2;

public class TestSaga : MassTransitStateMachine<TestState>
{
    public TestSaga(ILogger<TestSaga> logger)
    {
        InstanceState(x => x.CurrentState, ProcessingState);

        Event(() => EventReceived, x => { x.CorrelateById(x => x.Message.Test); });
        Event(() => EventReceived2, x => { x.CorrelateById(x => x.Message.Test); });

        Initially(When(EventReceived)
            .TransitionTo(ProcessingState)
            .Then(_ => logger.LogInformation("XD")));

        During(ProcessingState,
            When(EventReceived2)
                .Finalize()
                .Then(_ => logger.LogInformation(":0")));

        SetCompletedWhenFinalized();
    }

    public State ProcessingState { get; }

    public Event<EventReceived> EventReceived { get; }

    public Event<EventReceived2> EventReceived2 { get; }
}