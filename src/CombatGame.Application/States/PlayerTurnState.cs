using CombatGame.Application;

namespace CombatGame.Application.States;

public class PlayerTurnState : ICombatState
{
    public string Name => "Player turn";

    public void Enter(CombatSession session)
    {
        // Rien de special ici, c'est surtout pour montrer le pattern State.
    }

    public void Execute(CombatSession session)
    {
        // Le joueur choisit dans la console, donc l'etat attend juste une commande.
    }
}
