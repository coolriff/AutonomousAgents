using UnityEngine;
using System.Collections;

namespace FSM
{
    public abstract class State<T>
    {
        // This will be executed when the state is entered
        abstract public void Enter(T agent);

        // This is called by the Agent's update function each update step
        abstract public void Execute(T agent);

        // This will be executed when the state is exited
        abstract public void Exit(T agent);

        // This will be executed when the agent receives a message
        abstract public bool OnMesssage(T agent, Telegram telegram);
    }
}
