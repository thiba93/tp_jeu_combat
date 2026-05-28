using CombatGame.Domain.Enums;

namespace CombatGame.Infrastructure.Console;

public class ConsoleInput
{
    public string ReadHeroName()
    {
        System.Console.Write("Hero name: ");
        string? name = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
            return "Player";

        return name.Trim();
    }

    public HeroClass ReadHeroClass()
    {
        while (true)
        {
            System.Console.WriteLine("Choose a class:");
            System.Console.WriteLine("  1. Warrior");
            System.Console.WriteLine("  2. Mage");
            System.Console.WriteLine("  3. Rogue");
            System.Console.Write("Choice: ");

            string? input = System.Console.ReadLine();

            if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(HeroClass), choice))
                return (HeroClass)choice;

            System.Console.WriteLine("Invalid class.");
        }
    }

    public int ReadMenuChoice()
    {
        System.Console.Write("Your choice: ");
        string? input = System.Console.ReadLine();

        if (!int.TryParse(input, out int choice))
            return -1;

        return choice;
    }
}

