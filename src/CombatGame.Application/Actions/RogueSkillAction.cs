using CombatGame.Application;
using CombatGame.Domain.Services;

namespace CombatGame.Application.Actions;

public class RogueSkillAction : ICombatAction
{
    private readonly DamageCalculator _damageCalculator;
    private readonly Random _random;

    public RogueSkillAction(DamageCalculator damageCalculator, Random? random = null)
    {
        _damageCalculator = damageCalculator;
        _random = random ?? new Random();
    }

    public string Name => "Critical Hit";

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

        int damage = _damageCalculator.CalculatePhysicalDamage(session.Hero, target);
        bool critical = _random.NextDouble() <= 0.30;

        if (critical)
            damage *= 2;

        target.TakeDamage(damage);
        session.Hero.StartSkillCooldown();

        string word = critical ? "critical " : "";
        string message = $"{session.Hero.Name} uses {word}hit on {target.Name} for {damage} damage.";
        session.EventPublisher.Publish(message);

        if (!target.IsAlive)
            session.EventPublisher.Publish($"{target.Name} is defeated.");

        return new CombatResult(true, true, message);
    }
}
