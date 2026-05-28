namespace CombatGame.Application.Events;

public class CombatEventPublisher
{
    private readonly List<ICombatObserver> _observers = new();

    public void AddObserver(ICombatObserver observer)
    {
        _observers.Add(observer);
    }

    public void Publish(string message)
    {
        var combatEvent = new CombatEvent(message);

        foreach (var observer in _observers)
        {
            observer.OnEvent(combatEvent);
        }
    }
}

