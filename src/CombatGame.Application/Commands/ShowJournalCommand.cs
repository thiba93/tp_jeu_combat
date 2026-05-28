using CombatGame.Application;
using CombatGame.Application.Actions;

namespace CombatGame.Application.Commands;

public class ShowJournalCommand : ICommand
{
    public CombatResult Execute(CombatSession session)
    {
        var messages = session.Journal.GetLastMessages(8);

        if (messages.Count == 0)
            return new CombatResult(true, false, "Journal is empty.");

        string text = string.Join(Environment.NewLine, messages);
        return new CombatResult(true, false, text);
    }
}
