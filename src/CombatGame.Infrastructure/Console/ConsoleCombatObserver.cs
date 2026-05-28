using CombatGame.Application.Events;

namespace CombatGame.Infrastructure.Console;

public class ConsoleCombatObserver : ICombatObserver
{
    public void OnEvent(CombatEvent combatEvent)
    {
        System.Console.ForegroundColor = ConsoleColor.DarkGray;
        System.Console.WriteLine($"> {combatEvent.Message}");
        System.Console.ResetColor();
    }
}

