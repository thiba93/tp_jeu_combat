using CombatGame.Application;
using CombatGame.Domain.Entities;

namespace CombatGame.Infrastructure.Console;

public class ConsoleRenderer
{
    public void ShowIntro()
    {
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("        COMBAT GAME");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine();
    }

    public void ShowCombat(CombatSession session)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("========================================");
        System.Console.WriteLine($"  WAVE {session.WaveNumber}/3 - {session.CurrentState.Name}");
        System.Console.WriteLine("========================================");

        ShowHero(session.Hero);
        ShowEnemies(session.Enemies);
        ShowActions(session.Hero);
    }

    public void ShowMessage(string message)
    {
        System.Console.WriteLine(message);
    }

    public void ShowEnd(CombatSession session)
    {
        System.Console.WriteLine();
        System.Console.WriteLine("========================================");
        System.Console.WriteLine(session.CurrentState.Name.ToUpperInvariant());
        System.Console.WriteLine("========================================");
    }

    private void ShowHero(Hero hero)
    {
        System.Console.WriteLine($"Hero: {hero.Name} ({hero.Class})");
        System.Console.WriteLine($"HP: {hero.Health}/{hero.MaxHealth}");
        System.Console.WriteLine($"Skill cooldown: {hero.CurrentCooldown}");
        System.Console.WriteLine($"Heals left: {hero.HealsLeft}");
        System.Console.WriteLine();
    }

    private void ShowEnemies(List<Enemy> enemies)
    {
        System.Console.WriteLine("Enemies:");

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            string status = enemy.IsAlive ? $"{enemy.Health}/{enemy.MaxHealth} HP" : "dead";
            System.Console.WriteLine($"  [{i + 1}] {enemy.Name} - {status}");
        }

        System.Console.WriteLine();
    }

    private void ShowActions(Hero hero)
    {
        // Petit menu simple pour ne pas trop compliquer le TP.
        System.Console.WriteLine("Actions:");
        System.Console.WriteLine("  1. Basic attack");
        System.Console.WriteLine($"  2. {hero.SkillName}");
        System.Console.WriteLine("  3. Heal");
        System.Console.WriteLine("  4. Journal");
    }
}

