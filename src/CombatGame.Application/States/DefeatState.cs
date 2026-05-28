using CombatGame.Application;

namespace CombatGame.Application.States;

public class DefeatState : ICombatState
{
    public string Name => "Defeat";

    public void Enter(CombatSession session)
    {
        session.EventPublisher.Publish("Defeat. The hero is dead.");
    }

    public void Execute(CombatSession session)
    {
    }
}
