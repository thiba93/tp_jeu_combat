using CombatGame.Domain.Constants;
using CombatGame.Domain.Enums;

namespace CombatGame.Domain.Entities;

public class Hero : Character
{
    public Hero(
        string name,
        HeroClass heroClass,
        int maxHealth,
        int attack,
        int armor,
        string skillName,
        int skillCooldown)
        : base(name, maxHealth, attack, armor)
    {
        Class = heroClass;
        SkillName = skillName;
        SkillCooldown = skillCooldown;
        HealsLeft = GameRules.MaxHealCount;
    }

    public HeroClass Class { get; }
    public string SkillName { get; }
    public int SkillCooldown { get; }
    public int CurrentCooldown { get; private set; }
    public int HealsLeft { get; private set; }

    public bool CanUseSkill()
    {
        return CurrentCooldown == 0;
    }

    public void StartSkillCooldown()
    {
        CurrentCooldown = SkillCooldown;
    }

    public void ReduceCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }

    public bool TryUseHeal(out int healed)
    {
        healed = 0;

        if (HealsLeft <= 0)
            return false;

        HealsLeft--;
        healed = Heal(GameRules.HealValue);
        return true;
    }

    public int RestoreBetweenWaves()
    {
        int value = (int)Math.Ceiling(MaxHealth * GameRules.WaveHealPercent);
        return Heal(value);
    }
}

