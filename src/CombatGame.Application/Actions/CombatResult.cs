namespace CombatGame.Application.Actions;

public class CombatResult
{
    public CombatResult(bool success, bool endsTurn, string message)
    {
        Success = success;
        EndsTurn = endsTurn;
        Message = message;
    }

    public bool Success { get; }
    public bool EndsTurn { get; }
    public string Message { get; }
}

