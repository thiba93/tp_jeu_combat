using CombatGame.Application;
using CombatGame.Application.Actions;

namespace CombatGame.Application.Commands;

public class HealCommand : ICommand
{
    private readonly ICombatAction _action;

    public HealCommand(ICombatAction action)
    {
        _action = action;
    }

    public CombatResult Execute(CombatSession session)
    {
        CombatResult result = _action.Execute(session);

        if (result.Success && result.EndsTurn)
            session.CompletePlayerTurn();

        return result;
    }
}
