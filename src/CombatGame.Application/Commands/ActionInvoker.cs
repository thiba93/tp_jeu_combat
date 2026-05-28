using CombatGame.Application;
using CombatGame.Application.Actions;

namespace CombatGame.Application.Commands;

public class ActionInvoker
{
    private readonly Dictionary<int, ICommand> _commands = new();

    public void Register(int choice, ICommand command)
    {
        _commands[choice] = command;
    }

    public CombatResult Execute(int choice, CombatSession session)
    {
        if (!_commands.TryGetValue(choice, out var command))
            return new CombatResult(false, false, "Invalid choice.");

        return command.Execute(session);
    }
}
