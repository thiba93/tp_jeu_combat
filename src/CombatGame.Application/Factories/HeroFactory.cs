using CombatGame.Domain.Entities;
using CombatGame.Domain.Enums;

namespace CombatGame.Application.Factories;

public class HeroFactory
{
    public Hero Create(string name, HeroClass heroClass)
    {
        return heroClass switch
        {
            HeroClass.Warrior => new Hero(name, heroClass, 120, 18, 4, "Heavy Strike", 2),
            HeroClass.Mage => new Hero(name, heroClass, 80, 12, 1, "Lightning", 3),
            HeroClass.Rogue => new Hero(name, heroClass, 90, 14, 2, "Critical Hit", 2),
            _ => throw new ArgumentException("Unknown hero class.")
        };
    }
}

