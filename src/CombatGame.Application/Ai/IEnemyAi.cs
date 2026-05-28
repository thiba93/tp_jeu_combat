using CombatGame.Application;
using CombatGame.Domain.Entities;

namespace CombatGame.Application.Ai;

public interface IEnemyAi
{
    void PlayTurn(CombatSession session, Enemy enemy);
}
