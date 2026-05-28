using CombatGame.Application.Ai;
using CombatGame.Application.Events;
using CombatGame.Application.Factories;
using CombatGame.Application.States;
using CombatGame.Domain.Entities;

namespace CombatGame.Application;

public class CombatSession
{
    private readonly EnemyFactory _enemyFactory;
    private readonly IEnemyAi _enemyAi;

    public CombatSession(
        Hero hero,
        EnemyFactory enemyFactory,
        IEnemyAi enemyAi,
        CombatEventPublisher eventPublisher,
        JournalCombatObserver journal)
    {
        Hero = hero;
        _enemyFactory = enemyFactory;
        _enemyAi = enemyAi;
        EventPublisher = eventPublisher;
        Journal = journal;
        WaveNumber = 1;
        Enemies = _enemyFactory.CreateWave(WaveNumber);
        CurrentState = new PlayerTurnState();
    }

    public const int MaxWaveNumber = 3;

    public Hero Hero { get; }
    public List<Enemy> Enemies { get; private set; }
    public int WaveNumber { get; private set; }
    public ICombatState CurrentState { get; private set; }
    public CombatEventPublisher EventPublisher { get; }
    public JournalCombatObserver Journal { get; }

    public bool IsFinished =>
        CurrentState is VictoryState || CurrentState is DefeatState;

    public void ExecuteCurrentState()
    {
        CurrentState.Execute(this);
    }

    public void SetState(ICombatState state)
    {
        CurrentState = state;
        CurrentState.Enter(this);
    }

    public Enemy? GetFirstAliveEnemy()
    {
        return Enemies.FirstOrDefault(enemy => enemy.IsAlive);
    }

    public bool AreAllEnemiesDefeated()
    {
        return Enemies.All(enemy => !enemy.IsAlive);
    }

    public void CompletePlayerTurn()
    {
        if (!Hero.IsAlive)
        {
            SetState(new DefeatState());
            return;
        }

        if (AreAllEnemiesDefeated())
        {
            GoAfterWave();
            return;
        }

        SetState(new EnemyTurnState());
    }

    public void PlayEnemyTurns()
    {
        foreach (var enemy in Enemies.Where(enemy => enemy.IsAlive))
        {
            _enemyAi.PlayTurn(this, enemy);
        }
    }

    public void PrepareNextWave()
    {
        WaveNumber++;
        int healed = Hero.RestoreBetweenWaves();
        Enemies = _enemyFactory.CreateWave(WaveNumber);

        EventPublisher.Publish($"{Hero.Name} recovers {healed} health before the next wave.");
        EventPublisher.Publish($"Wave {WaveNumber} starts.");
    }

    public void GoAfterWave()
    {
        if (WaveNumber >= MaxWaveNumber)
            SetState(new VictoryState());
        else
            SetState(new BetweenWavesState());
    }
}

