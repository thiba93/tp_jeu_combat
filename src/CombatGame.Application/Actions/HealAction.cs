using CombatGame.Application;

namespace CombatGame.Application.Actions;

public class HealAction : ICombatAction
{
    public string Name => "Heal";

    public bool CanExecute(CombatSession session)
    {
        return session.Hero.HealsLeft > 0;
    }

    public CombatResult Execute(CombatSession session)
    {
        if (!session.Hero.TryUseHeal(out int healed))
            return new CombatResult(false, false, "No heal left.");

        string message = $"{session.Hero.Name} heals {healed} health.";
        session.EventPublisher.Publish(message);

        return new CombatResult(true, true, message);
    }
}
