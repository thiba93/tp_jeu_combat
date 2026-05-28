using CombatGame.Domain.Entities;

namespace CombatGame.Application.Factories;

public class EnemyFactory
{
    public List<Enemy> CreateWave(int waveNumber)
    {
        if (waveNumber == 1)
            return new List<Enemy> { CreateGoblin() };

        if (waveNumber == 2)
            return new List<Enemy> { CreateGoblin(), CreateArcher() };

        return new List<Enemy> { CreateBoss() };
    }

    private Enemy CreateGoblin()
    {
        return new Enemy("Goblin", 40, 9, 1);
    }

    private Enemy CreateArcher()
    {
        return new Enemy("Goblin Archer", 35, 11, 0);
    }

    private Enemy CreateBoss()
    {
        return new Enemy("Orc Boss", 110, 17, 3);
    }
}

