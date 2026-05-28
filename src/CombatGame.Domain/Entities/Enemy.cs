namespace CombatGame.Domain.Entities;

public class Enemy : Character
{
    public Enemy(string name, int maxHealth, int attack, int armor)
        : base(name, maxHealth, attack, armor)
    {
    }
}

