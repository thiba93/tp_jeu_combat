using CombatGame.Application;

namespace CombatGame.Application.States;

public class VictoryState : ICombatState
{
    public string Name => "Victory";

    public void Enter(CombatSession session)
    {
        session.EventPublisher.Publish("Victory! All enemies are defeated.");
    }

    public void Execute(CombatSession session)
    {
    }
}
