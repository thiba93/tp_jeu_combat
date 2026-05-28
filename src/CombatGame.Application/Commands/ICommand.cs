using CombatGame.Application;
using CombatGame.Application.Actions;

namespace CombatGame.Application.Commands;

public interface ICommand
{
    CombatResult Execute(CombatSession session);
}
