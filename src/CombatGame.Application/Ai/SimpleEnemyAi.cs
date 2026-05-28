using CombatGame.Application;
using CombatGame.Application.Events;
using CombatGame.Domain.Entities;
using CombatGame.Domain.Services;

namespace CombatGame.Application.Ai;

public class SimpleEnemyAi : IEnemyAi
{
    private readonly DamageCalculator _damageCalculator;
    private readonly CombatEventPublisher _eventPublisher;

    public SimpleEnemyAi(
        DamageCalculator damageCalculator,
        CombatEventPublisher eventPublisher)
    {
        _damageCalculator = damageCalculator;
        _eventPublisher = eventPublisher;
    }

    public void PlayTurn(CombatSession session, Enemy enemy)
    {
        if (!enemy.IsAlive || !session.Hero.IsAlive)
            return;

        int damage = _damageCalculator.CalculatePhysicalDamage(enemy, session.Hero);
        session.Hero.TakeDamage(damage);

        _eventPublisher.Publish($"{enemy.Name} attacks {session.Hero.Name} for {damage} damage.");

        if (!session.Hero.IsAlive)
            _eventPublisher.Publish($"{session.Hero.Name} is defeated.");
    }
}
