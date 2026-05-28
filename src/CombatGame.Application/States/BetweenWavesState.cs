using CombatGame.Application;

namespace CombatGame.Application.States;

public class BetweenWavesState : ICombatState
{
    public string Name => "Between waves";

    public void Enter(CombatSession session)
    {
        session.EventPublisher.Publish($"Wave {session.WaveNumber} is finished.");
    }

    public void Execute(CombatSession session)
    {
        session.PrepareNextWave();
        session.SetState(new PlayerTurnState());
    }
}
