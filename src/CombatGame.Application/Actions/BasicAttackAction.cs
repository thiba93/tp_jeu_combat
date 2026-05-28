using CombatGame.Application;
using CombatGame.Domain.Services;

namespace CombatGame.Application.Actions;

public class BasicAttackAction : ICombatAction
{
    private readonly DamageCalculator _damageCalculator;

    public BasicAttackAction(DamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public string Name => "Basic attack";

    public bool CanExecute(CombatSession session)
    {
        return session.GetFirstAliveEnemy() is not null;
    }

    public CombatResult Execute(CombatSession session)
    {
        var target = session.GetFirstAliveEnemy();

        if (target is null)
            return new CombatResult(false, false, "There is no enemy.");

        int damage = _damageCalculator.CalculatePhysicalDamage(session.Hero, target);
        target.TakeDamage(damage);

        string message = $"{session.Hero.Name} attacks {target.Name} for {damage} damage.";
        session.EventPublisher.Publish(message);

        if (!target.IsAlive)
            session.EventPublisher.Publish($"{target.Name} is defeated.");

        return new CombatResult(true, true, message);
    }
}
