namespace CombatGame.Domain.Entities;

public abstract class Character
{
    protected Character(string name, int maxHealth, int attack, int armor)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Attack = attack;
        Armor = armor;
    }

    public string Name { get; }
    public int MaxHealth { get; }
    public int Health { get; private set; }
    public int Attack { get; }
    public int Armor { get; }
    public bool IsAlive => Health > 0;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            damage = 0;

        Health -= damage;

        if (Health < 0)
            Health = 0;
    }

    public int Heal(int value)
    {
        if (!IsAlive || value <= 0)
            return 0;

        int oldHealth = Health;
        Health += value;

        if (Health > MaxHealth)
            Health = MaxHealth;

        return Health - oldHealth;
    }
}

