/// <summary>
/// Functions for state machine
/// </summary>
public interface IState
{
    void Enter();

    void Execute();

    void Exit();
}
