using UnityEngine;
using System.Collections;

namespace FSM
{
    public class OutlawEnterMineAndDigForNugget : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw rob the goldmine");
            outlaw.targetLocation = Location.goldMine;

            if (outlaw.location != outlaw.targetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawMovingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.GoldCarrying += 1;
            outlaw.HowFatigued += 1;
            Debug.Log(outlaw.ID + "outlaw rob a nugget");

            if (outlaw.PocketsFull())
            {
                outlaw.StateMachine.ChangeState(new OutlawVisitBankAndDepositGold());
            }

            if (outlaw.Thirsty())
            {
                outlaw.StateMachine.ChangeState(new OutlawQuenchThirst());
            }
        }

        public override void Exit(Outlaw outlaw)
        {
            if (outlaw.location == outlaw.targetLocation)
                Debug.Log(outlaw.ID + "outlaw leaving the gold mine !!!!!!!!!!!!!!!!!!");
        }

        public override bool OnMessage(Outlaw agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Outlaw agent, Sense sense)
        {
            return false;
        }

    }

    // In this state, the miner goes to the bank and deposits gold
    public class OutlawVisitBankAndDepositGold : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw rob the bank........");

            outlaw.targetLocation = Location.bank;

            if (outlaw.location != outlaw.targetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawMovingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.MoneyInBank += outlaw.GoldCarrying;
            outlaw.GoldCarrying = 0;
            Debug.Log(outlaw.ID + " rob total now: " + outlaw.MoneyInBank);
            if (outlaw.Rich())
            {
                Debug.Log(outlaw.ID + "outlaw WooHoo! Rich enough for now. Back home");
                outlaw.StateMachine.ChangeState(new OutlawGoHomeAndSleepTillRested());
            }
            else
            {
                outlaw.StateMachine.ChangeState(new OutlawEnterMineAndDigForNugget());
            }
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw Leaving the Bank");
        }

        public override bool OnMessage(Outlaw agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Outlaw agent, Sense sense)
        {
            return false;
        }
    }

    // In this state, the miner goes home and sleeps
    public class OutlawGoHomeAndSleepTillRested : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + " outlaw Walking Home");
            outlaw.targetLocation = Location.outlawHome;

            if (outlaw.location != outlaw.targetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawMovingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            if (outlaw.HowFatigued < outlaw.TirednessThreshold)
            {
                Debug.Log(outlaw.ID + "outlaw Time to rob more gold!");
                outlaw.StateMachine.ChangeState(new OutlawEnterMineAndDigForNugget());
            }
            else
            {
                outlaw.HowFatigued--;
                Debug.Log(outlaw.ID + " XXXXXXXXX....");
            }
        }

        public override void Exit(Outlaw outlaw)
        {

        }

        public override bool OnMessage(Outlaw outlaw, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Outlaw agent, Sense sense)
        {
            return false;
        }
    }

    // In this state, the miner goes to the saloon to drink
    public class OutlawQuenchThirst : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            outlaw.targetLocation = Location.saloon;

            if (outlaw.location != outlaw.targetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawMovingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            // Buying whiskey costs 2 gold but quenches thirst altogether
            outlaw.HowThirsty = 0;
            outlaw.MoneyInBank -= 2;
            Debug.Log(outlaw.ID + " Rob everyone in saloon ");
            outlaw.StateMachine.ChangeState(new OutlawEnterMineAndDigForNugget());
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw Leaving the saloon");
        }

        public override bool OnMessage(Outlaw agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Outlaw agent, Sense sense)
        {
            return false;
        }
    }

    // In this state, the miner eats the food that Elsa has prepared
    public class OutlawEatStew : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw Smells Reaaal goood !!!!");
        }

        public override void Execute(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "outlaw Tastes real good too!");
            outlaw.StateMachine.RevertToPreviousState();
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + " outlaw Ah better get back to rob");
        }

        public override bool OnMessage(Outlaw agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Outlaw agent, Sense sense)
        {
            return false;
        }
    }

    public class OutlawMovingTo : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            var locationManager = Object.FindObjectOfType<LocationManager>();
            outlaw.MoveToNewLocation(locationManager.outLawLocations[outlaw.targetLocation].position);
        }

        public override void Execute(Outlaw outlaw)
        {
            var locationManager = Object.FindObjectOfType<LocationManager>();
            var targetLocation = locationManager.outLawLocations[outlaw.targetLocation].position;
            if (Vector3.Distance(targetLocation, outlaw.transform.position) <= 5.0f)
            {
                outlaw.location = outlaw.targetLocation;
                outlaw.StateMachine.RevertToPreviousState();
            }
        }

        public override void Exit(Outlaw outlaw) { }
        public override bool OnMessage(Outlaw outlaw, Telegram telegram) { return true; }

        public override bool OnSenseEvent(Outlaw outlaw, Sense sense)
        {
            return false;
        }
    }

    // If the agent has a global state, then it is executed every Update() cycle
    public class OutlawGlobalState : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw) { }
        public override void Execute(Outlaw outlaw) { }
        public override void Exit(Outlaw outlaw) { }
        public override bool OnMessage(Outlaw outlaw, Telegram telegram) { return false; }
        public override bool OnSenseEvent(Outlaw outlaw, Sense sense) { return false; }
    }
}

