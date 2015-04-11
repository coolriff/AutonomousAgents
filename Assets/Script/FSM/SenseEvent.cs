using UnityEngine;
using System.Collections;

namespace FSM
{
    public class SenseEvent : MonoBehaviour
    {
        static float SenseRange = 50.0f;
        float defaultAttenuation = 50.0f;

        public void UpdateSensor()
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>().transform.position, GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>().transform.position) < SenseRange)
            {
                Debug.Log(" I can feel outlaw around!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                if (GameObject.FindGameObjectWithTag("AStar").GetComponent<Pathfinding>().PropogateSense(GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>().transform.position, GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>().transform.position, defaultAttenuation))
                {
                    Debug.Log(" PropogateSensePropogateSensePropogateSensePropogateSensePropogateSensePropogateSense!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Sense sTo = new Sense(GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>().ID, GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>().ID, SenseType.sight);
                    Sense oTs = new Sense(GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>().ID, GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>().ID, SenseType.sight);
                    GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>().HandleSenseEvent(sTo);
                    GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>().HandleSenseEvent(oTs);
                }
            }
        }
    }
}

