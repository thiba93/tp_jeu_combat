using CombatGame.Domain.Enums;
using CombatGame.Domain.Services;

namespace CombatGame.Application.Actions;

public class CombatActionFactory
{
    private readonly DamageCalculator _damageCalculator;

    public CombatActionFactory(DamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public ICombatAction CreateBasicAttack()
    {
        return new BasicAttackAction(_damageCalculator);
    }

    public ICombatAction CreateHeal()
    {
        return new HealAction();
    }

    public ICombatAction CreateSkill(HeroClass heroClass)
    {
        return heroClass switch
        {
            HeroClass.Warrior => new WarriorSkillAction(_damageCalculator),
            HeroClass.Mage => new MageSkillAction(),
            HeroClass.Rogue => new RogueSkillAction(_damageCalculator),
            _ => throw new ArgumentException("Unknown hero class.")
        };
    }
}

