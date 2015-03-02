using UnityEngine;
using System.Collections;

namespace FSM
{
    // This class implements normal states, global states and state blips for a given agent.
    // The agent should create its own StateMachine when its constructor is called.
    public class StateMachine<T>
    {
        private T owner;
        private State<T> currentState = null;
        private State<T> previousState = null;
        private State<T> globalState = null;

        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        // This holds the current state for the state machine
        // Unity properties
        public State<T> CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        // The agent's previous state is needed to implement state blips
        public State<T> PreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }

        // The agent's global state is always executed, if it exists
        public State<T> GlobalState
        {
            get { return globalState; }
            set { globalState = value; }
        }


        // Update is called once per frame
        public void Update()
        {
            if (globalState != null)
            {
                globalState.Execute(owner);
            }

            if (currentState != null)
            {
                currentState.Execute(owner);
            }
        }

        // This method attempts to deliver a message first via the global state, and if that fails
        // via the current state
        public bool HandleMessage(Telegram telegram)
        {
            if (globalState != null)
            {
                if (globalState.OnMesssage(owner, telegram))
                {
                    return true;
                }
            }

            if (currentState != null)
            {
                if (currentState.OnMesssage(owner, telegram))
                {
                    return true;
                }
            }

            return false;
        }

        // Switch to a new state and save the old one, so we can revert to it later if it's a state blip
        public void ChangeState(State<T> newState)
        {
            if (newState == null)
            {
                Debug.Log("Trying to change to a null State");
            }
            else
            {
                previousState = currentState;
                currentState.Exit(owner);
                currentState = newState;
                currentState.Enter(owner);
            }
        }

        // Invoked when a state blip is finished
        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }

        // Checks whether the machine is in a given state
        public bool IsInState(State<T> state)
        {
            return state.Equals(currentState);
        }
    }
}
