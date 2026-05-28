namespace CombatGame.Application.Events;

public interface ICombatObserver
{
    void OnEvent(CombatEvent combatEvent);
}

