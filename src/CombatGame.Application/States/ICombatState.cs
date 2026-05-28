using CombatGame.Application;

namespace CombatGame.Application.States;

public interface ICombatState
{
    string Name { get; }
    void Enter(CombatSession session);
    void Execute(CombatSession session);
}
