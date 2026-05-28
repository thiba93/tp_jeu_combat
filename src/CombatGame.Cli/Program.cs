using CombatGame.Application;
using CombatGame.Application.Actions;
using CombatGame.Application.Ai;
using CombatGame.Application.Commands;
using CombatGame.Application.Events;
using CombatGame.Application.Factories;
using CombatGame.Application.States;
using CombatGame.Domain.Entities;
using CombatGame.Domain.Enums;
using CombatGame.Domain.Services;
using CombatGame.Infrastructure.Console;

var renderer = new ConsoleRenderer();
var input = new ConsoleInput();

renderer.ShowIntro();

string heroName = input.ReadHeroName();
HeroClass heroClass = input.ReadHeroClass();

var heroFactory = new HeroFactory();
Hero hero = heroFactory.Create(heroName, heroClass);

var damageCalculator = new DamageCalculator();
var eventPublisher = new CombatEventPublisher();
var journal = new JournalCombatObserver();

eventPublisher.AddObserver(journal);
eventPublisher.AddObserver(new ConsoleCombatObserver());

var enemyFactory = new EnemyFactory();
var enemyAi = new SimpleEnemyAi(damageCalculator, eventPublisher);
var session = new CombatSession(hero, enemyFactory, enemyAi, eventPublisher, journal);

ActionInvoker invoker = BuildInvoker(hero, damageCalculator);

eventPublisher.Publish("The fight starts.");

while (!session.IsFinished)
{
    if (session.CurrentState is PlayerTurnState)
    {
        renderer.ShowCombat(session);
        int choice = input.ReadMenuChoice();
        CombatResult result = invoker.Execute(choice, session);

        if (!result.EndsTurn || !result.Success)
            renderer.ShowMessage(result.Message);
    }
    else
    {
        session.ExecuteCurrentState();
    }
}

renderer.ShowEnd(session);

static ActionInvoker BuildInvoker(Hero hero, DamageCalculator damageCalculator)
{
    // Ici on branche les commandes du menu avec les actions.
    var actionFactory = new CombatActionFactory(damageCalculator);
    var invoker = new ActionInvoker();

    invoker.Register(1, new AttackCommand(actionFactory.CreateBasicAttack()));
    invoker.Register(2, new UseSkillCommand(actionFactory.CreateSkill(hero.Class)));
    invoker.Register(3, new HealCommand(actionFactory.CreateHeal()));
    invoker.Register(4, new ShowJournalCommand());

    return invoker;
}

