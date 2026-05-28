using CombatGame.Application;
using CombatGame.Domain.Services;

namespace CombatGame.Application.Actions;

public class WarriorSkillAction : ICombatAction
{
    private readonly DamageCalculator _damageCalculator;

    public WarriorSkillAction(DamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public string Name => "Heavy Strike";

    public bool CanExecute(CombatSession session)
    {
        return session.Hero.CanUseSkill() && session.GetFirstAliveEnemy() is not null;
    }

    public CombatResult Execute(CombatSession session)
    {
        var target = session.GetFirstAliveEnemy();

        if (target is null)
            return new CombatResult(false, false, "There is no enemy.");

        if (!session.Hero.CanUseSkill())
            return new CombatResult(false, false, "Skill is still in cooldown.");

        int baseDamage = _damageCalculator.CalculatePhysicalDamage(session.Hero, target);
        int damage = (int)Math.Ceiling(baseDamage * 1.5);
        target.TakeDamage(damage);
        session.Hero.StartSkillCooldown();

        string message = $"{session.Hero.Name} uses Heavy Strike on {target.Name} for {damage} damage.";
        session.EventPublisher.Publish(message);

        if (!target.IsAlive)
            session.EventPublisher.Publish($"{target.Name} is defeated.");

        return new CombatResult(true, true, message);
    }
}
