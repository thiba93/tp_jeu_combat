namespace CombatGame.Application.Events;

public class CombatEvent
{
    public CombatEvent(string message)
    {
        Message = message;
        CreatedAt = DateTime.Now;
    }

    public string Message { get; }
    public DateTime CreatedAt { get; }
}

