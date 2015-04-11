using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Outlaw : Agent
    {
        public int MaxNuggets = 1;
        public int ThirstLevel = 1;
        public int ComfortLevel = 1;
        public int TirednessThreshold = 1;

        private int goldCarrying;
        private int moneyInBank;
        private int howThirsty;
        private int howFatigued;

        public int GoldCarrying { get { return goldCarrying; } set { goldCarrying = value; } }
        public int MoneyInBank { get { return moneyInBank; } set { moneyInBank = value; } }
        public int HowThirsty { get { return howThirsty; } set { howThirsty = value; } }
        public int HowFatigued { get { return howFatigued; } set { howFatigued = value; } }

        private StateMachine<Outlaw> stateMachine;
        public StateMachine<Outlaw> StateMachine { get { return stateMachine; } set { stateMachine = value; } }

        public Outlaw()
            : base()
        {
            stateMachine = new StateMachine<Outlaw>(this);
            stateMachine.CurrentState = new OutlawGoHomeAndSleepTillRested();
            stateMachine.GlobalState = new OutlawGlobalState();
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
                howThirsty += 1;
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


        public bool PocketsFull()
        {
            if (goldCarrying >= MaxNuggets)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is thirsty or not, depending on the predefined level
        public bool Thirsty()
        {
            if (howThirsty >= ThirstLevel)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is fatigued or not, depending on the predefined level
        public bool Fatigued()
        {
            if (howFatigued >= TirednessThreshold)
                return true;
            else
                return false;
        }

        // This method checks whether the agent feels rich enough, depending on the predefined level
        public bool Rich()
        {
            if (moneyInBank >= ComfortLevel)
                return true;
            else
                return false;
        }
    }
}