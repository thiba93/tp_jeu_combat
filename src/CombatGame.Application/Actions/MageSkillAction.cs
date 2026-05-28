using CombatGame.Application;

namespace CombatGame.Application.Actions;

public class MageSkillAction : ICombatAction
{
    private const int MagicDamage = 28;

    public string Name => "Lightning";

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

        int halfArmor = target.Armor / 2;
        int damage = MagicDamage - halfArmor;

        if (damage < 1)
            damage = 1;

        target.TakeDamage(damage);
        session.Hero.StartSkillCooldown();

        string message = $"{session.Hero.Name} casts Lightning on {target.Name} for {damage} damage.";
        session.EventPublisher.Publish(message);

        if (!target.IsAlive)
            session.EventPublisher.Publish($"{target.Name} is defeated.");

        return new CombatResult(true, true, message);
    }
}
