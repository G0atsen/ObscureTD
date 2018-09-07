using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState)
    {
        if (this.currentState != null)
        {
            this.previousState = this.currentState;
            this.currentState.Exit();
        }
        this.currentState = newState;
        this.currentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        if (this.currentState != null)
        {
            IState runningState = this.currentState;
            runningState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        ChangeState(this.previousState);
    }

}
