using CombatGame.Domain.Entities;
using CombatGame.Domain.Services;
using Xunit;

namespace CombatGame.Tests;

public class DamageCalculatorTests
{
    [Fact]
    public void CalculatePhysicalDamage_UsesTargetArmor()
    {
        var attacker = new Enemy("Test attacker", 30, 15, 0);
        var target = new Enemy("Test target", 30, 8, 4);
        var calculator = new DamageCalculator();

        int damage = calculator.CalculatePhysicalDamage(attacker, target);

        Assert.Equal(11, damage);
    }
}

