namespace CombatGame.Application.Events;

public class JournalCombatObserver : ICombatObserver
{
    private readonly List<string> _messages = new();

    public void OnEvent(CombatEvent combatEvent)
    {
        _messages.Add(combatEvent.Message);
    }

    public IReadOnlyList<string> GetLastMessages(int count)
    {
        if (count <= 0)
            return Array.Empty<string>();

        return _messages
            .TakeLast(count)
            .ToList();
    }
}

