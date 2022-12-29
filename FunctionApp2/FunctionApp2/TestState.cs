using System;
using MassTransit;

namespace FunctionApp2;

public class TestState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public int CurrentState { get; set; }
}