using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Sheriff : Agent
    {
        public bool FindOutlaw = false;
        public int patrolTime;
        public int maxPatrolOnEachLocation = 3;


        private StateMachine<Sheriff> stateMachine;
        public StateMachine<Sheriff> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        public int PatrolTime
        {
            get { return patrolTime; }
            set { patrolTime = value; }
        }

        public Sheriff()
            : base()
        {
            stateMachine = new StateMachine<Sheriff>(this);
            stateMachine.CurrentState = new ToPatrolLocation_1();
            stateMachine.GlobalState = new SheriffGlobalState();
        }

        // Update is called once per frame
        void Start()
        {
            StartCoroutine(PerformUpdate());
        }

        IEnumerator PerformUpdate()
        {
            while (true)
            {
                stateMachine.Update();
                yield return new WaitForSeconds(1.0f);
            }
        }

        public override bool HandleMessage(Telegram telegram)
        {
            return stateMachine.HandleMessage(telegram);
        }

        public override bool HandleSenseEvent(Sense sense)
        {
            return stateMachine.HandleSenseEvent(sense);
        }

        public bool ScannedEnough()
        {
            if (patrolTime >= maxPatrolOnEachLocation)
                return true;
            else
                return false;
        }
    }
}

