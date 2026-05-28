using CombatGame.Domain.Entities;
using CombatGame.Domain.Enums;
using Xunit;

namespace CombatGame.Tests;

public class HeroTests
{
    [Fact]
    public void Heal_DoesNotGoOverMaxHealth()
    {
        var hero = new Hero("Bob", HeroClass.Warrior, 120, 18, 4, "Heavy Strike", 2);

        hero.TakeDamage(10);
        int healed = hero.Heal(25);

        Assert.Equal(10, healed);
        Assert.Equal(120, hero.Health);
    }
}

