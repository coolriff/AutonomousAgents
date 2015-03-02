using UnityEngine;
using System.Collections;

namespace FSM
{
    public abstract class Agent
    {
        private static int agents = 0;

        // Every agent has a numerical id that is set when it is created
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Agent()
        {
            id = agents++;
        }

        // Any agent must implement these methods
        abstract public void Update();
        abstract public bool HandleMessage(Telegram telegram);
    }
}
