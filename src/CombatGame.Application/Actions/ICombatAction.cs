using CombatGame.Application;

namespace CombatGame.Application.Actions;

public interface ICombatAction
{
    string Name { get; }
    bool CanExecute(CombatSession session);
    CombatResult Execute(CombatSession session);
}
