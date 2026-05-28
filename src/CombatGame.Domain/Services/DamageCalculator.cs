using CombatGame.Domain.Entities;

namespace CombatGame.Domain.Services;

public class DamageCalculator
{
    public int CalculatePhysicalDamage(Character attacker, Character target)
    {
        int damage = attacker.Attack - target.Armor;

        if (damage < 1)
            damage = 1;

        return damage;
    }

    public int CalculateDamageWithArmorPart(
        Character attacker,
        Character target,
        double armorPart)
    {
        int usedArmor = (int)Math.Round(target.Armor * armorPart);
        int damage = attacker.Attack - usedArmor;

        if (damage < 1)
            damage = 1;

        return damage;
    }
}

