using CombatGame.Application;
using CombatGame.Application.Actions;
using CombatGame.Application.Ai;
using CombatGame.Application.Events;
using CombatGame.Application.Factories;
using CombatGame.Domain.Entities;
using CombatGame.Domain.Enums;
using CombatGame.Domain.Services;
using Xunit;

namespace CombatGame.Tests;

public class CooldownTests
{
    [Fact]
    public void Skill_CannotBeUsedTwiceBeforeCooldown()
    {
        var damageCalculator = new DamageCalculator();
        var events = new CombatEventPublisher();
        var journal = new JournalCombatObserver();
        events.AddObserver(journal);

        var hero = new Hero("Alice", HeroClass.Mage, 80, 12, 1, "Lightning", 3);
        var session = new CombatSession(
            hero,
            new EnemyFactory(),
            new SimpleEnemyAi(damageCalculator, events),
            events,
            journal);

        var skill = new MageSkillAction();

        CombatResult firstResult = skill.Execute(session);
        CombatResult secondResult = skill.Execute(session);

        Assert.True(firstResult.Success);
        Assert.False(secondResult.Success);
        Assert.Equal(3, hero.CurrentCooldown);
    }
}

