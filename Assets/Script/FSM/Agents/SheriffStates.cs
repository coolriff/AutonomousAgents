using UnityEngine;
using System.Collections;

namespace FSM
{
    public class ToPatrolLocation_1 : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "On my way to Patrolling location 1");

            sheriff.targetLocation = Location.Patrol_1;

            if (sheriff.location != sheriff.targetLocation)
            {
                sheriff.StateMachine.ChangeState(new SheriffMovingTo());
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            sheriff.patrolTime += 1;

            Debug.Log(sheriff.ID + " Patrolling location 1 now! ");

            if (sheriff.ScannedEnough())
            {
                sheriff.patrolTime = 0;
                Debug.Log(sheriff.ID + " Moving to Patrolling location 2");
                sheriff.StateMachine.ChangeState(new ToPatrolLocation_2());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Nothing happen location 1...hm");
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.hearing:
                    return false;
                case SenseType.smell:
                    return false;
                case SenseType.sight:
                    Debug.Log("<color=red> Hey you! </color>");
                    sheriff.StateMachine.ChangeState(new ChaseOutLaw());
                    return true;
                default:
                    return false;
            }
        }
    }

    public class ToPatrolLocation_2 : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "On my way to Patrolling location 2");

            sheriff.targetLocation = Location.Patrol_2;

            if (sheriff.location != sheriff.targetLocation)
            {
                sheriff.StateMachine.ChangeState(new SheriffMovingTo());
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            sheriff.patrolTime += 1;

            Debug.Log(sheriff.ID + " Patrolling location 2 now! ");

            if (sheriff.ScannedEnough())
            {
                sheriff.patrolTime = 0;
                Debug.Log(sheriff.ID + " Moving to Patrolling location 3");
                sheriff.StateMachine.ChangeState(new ToPatrolLocation_3());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Nothing happen location 2...hm");
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.hearing:
                    return false;
                case SenseType.smell:
                    return false;
                case SenseType.sight:
                    Debug.Log("<color=red> Hey you! </color>");
                    sheriff.StateMachine.ChangeState(new ChaseOutLaw());
                    return true;
                default:
                    return false;
            }
        }
    }

    public class ToPatrolLocation_3 : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "On my way to Patrolling location 3");

            sheriff.targetLocation = Location.Patrol_3;

            if (sheriff.location != sheriff.targetLocation)
            {
                sheriff.StateMachine.ChangeState(new SheriffMovingTo());
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            sheriff.patrolTime += 1;

            Debug.Log(sheriff.ID + " Patrolling location 3 now! ");

            if (sheriff.ScannedEnough())
            {
                sheriff.patrolTime = 0;
                Debug.Log(sheriff.ID + " Moving to Patrolling location 1");
                sheriff.StateMachine.ChangeState(new ToPatrolLocation_1());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Nothing happen location 3...hm");
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.hearing:
                    return false;
                case SenseType.smell:
                    return false;
                case SenseType.sight:
                    Debug.Log("<color=red> Hey you! </color>");
                    sheriff.StateMachine.ChangeState(new ChaseOutLaw());
                    return true;
                default:
                    return false;
            }
        }
    }

    public class ChaseOutLaw : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            sheriff.targetLocation = Location.outlawPosition;
            var outlaw = GameObject.FindGameObjectWithTag("Outlaw");

            if (sheriff.location != sheriff.targetLocation)
            {
                sheriff.MoveToNewLocation(outlaw.transform.position);
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "I got you! Don't run!!!!!!!!!!!!!!!!!!!!!!!");

            if (sheriff.location != sheriff.targetLocation)
            {
                sheriff.StateMachine.ChangeState(new ChaseOutLaw());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.hearing:
                    return false;
                case SenseType.smell:
                    return false;
                case SenseType.sight:
                    Debug.Log("<color=red> Hey you! </color>");
                    sheriff.StateMachine.ChangeState(new ChaseOutLaw());
                    return true;
                default:
                    return false;
            }
        }
    }

    public class SheriffMovingTo : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            var locationManager = Object.FindObjectOfType<LocationManager>();
            sheriff.MoveToNewLocation(locationManager.sheriffLocations[sheriff.targetLocation].position);
        }

        public override void Execute(Sheriff sheriff)
        {
            var locationManager = Object.FindObjectOfType<LocationManager>();
            var targetLocation = locationManager.sheriffLocations[sheriff.targetLocation].position;
            if (Vector3.Distance(targetLocation, sheriff.transform.position) <= 5.0f)
            {
                sheriff.location = sheriff.targetLocation;
                sheriff.StateMachine.RevertToPreviousState();
            }
        }

        public override void Exit(Sheriff sheriff) { }
        public override bool OnMessage(Sheriff sheriff, Telegram telegram) { return true; }

        public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
        {
            return false;
        }
    }

    // If the agent has a global state, then it is executed every Update() cycle
    public class SheriffGlobalState : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff) { }
        public override void Execute(Sheriff sheriff) { }
        public override void Exit(Sheriff sheriff) { }
        public override bool OnMessage(Sheriff sheriff, Telegram telegram) { return false; }
        public override bool OnSenseEvent(Sheriff sheriff, Sense sense) { return false; }
    }
}
