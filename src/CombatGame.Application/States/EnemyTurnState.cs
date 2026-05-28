using CombatGame.Application;

namespace CombatGame.Application.States;

public class EnemyTurnState : ICombatState
{
    public string Name => "Enemy turn";

    public void Enter(CombatSession session)
    {
        session.EventPublisher.Publish("Enemies are playing.");
    }

    public void Execute(CombatSession session)
    {
        session.PlayEnemyTurns();

        if (!session.Hero.IsAlive)
        {
            session.SetState(new DefeatState());
            return;
        }

        session.Hero.ReduceCooldown();
        session.SetState(new PlayerTurnState());
    }
}
