using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FSM{

    public class LocationManager : MonoBehaviour{

        public Dictionary<Location, Transform> allLocations = new Dictionary<Location, Transform>();
        public Dictionary<Location, Transform> outLawLocations = new Dictionary<Location, Transform>();
        public Dictionary<Location, Transform> sheriffLocations = new Dictionary<Location, Transform>();

        void Awake(){

            var saloon = GameObject.FindGameObjectWithTag("Saloon");
            if (saloon == null){
                Debug.Log("There is no Saloon game object");
            }
            else{
                allLocations.Add(Location.saloon, saloon.transform);
                outLawLocations.Add(Location.saloon, saloon.transform);
            }

            var home = GameObject.FindGameObjectWithTag("Shack");
            if (home == null){
                Debug.Log("There is no Home game object");
            }
            else{
                allLocations.Add(Location.shack, home.transform);
            }

            var bank = GameObject.FindGameObjectWithTag("Bank");
            if (bank == null){
                Debug.Log("There is no Bank game object");
            }
            else{
                allLocations.Add(Location.bank, bank.transform);
                outLawLocations.Add(Location.bank, bank.transform);
            }

            var mine = GameObject.FindGameObjectWithTag("Mine");
            if (mine == null){
                Debug.Log("There is no Mine game object");
            }
            else{
                allLocations.Add(Location.goldMine, mine.transform);
                outLawLocations.Add(Location.goldMine, mine.transform);
            }

            var outlawHome = GameObject.FindGameObjectWithTag("OutlawHome");
            if (outlawHome == null)
            {
                Debug.Log("There is no outlawHome game object");
            }
            else
            {
                outLawLocations.Add(Location.outlawHome, outlawHome.transform);
            }

            var patrol = GameObject.FindGameObjectWithTag("Patrol_1");
            if (patrol == null)
            {
                Debug.Log("There is no Patrol_1 game object");
            }
            else
            {
                sheriffLocations.Add(Location.Patrol_1, patrol.transform);
            }

            var patrol1 = GameObject.FindGameObjectWithTag("Patrol_2");
            if (patrol1 == null)
            {
                Debug.Log("There is no Patrol_2 game object");
            }
            else
            {
                sheriffLocations.Add(Location.Patrol_2, patrol1.transform);
            }

            var patrol2 = GameObject.FindGameObjectWithTag("Patrol_3");
            if (patrol2 == null)
            {
                Debug.Log("There is no Patrol_3 game object");
            }
            else
            {
                sheriffLocations.Add(Location.Patrol_3, patrol2.transform);
            }
        }
    }
}
