using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FSM{

    public class LocationManager : MonoBehaviour{

        public Dictionary<Location, Transform> allLocations = new Dictionary<Location, Transform>();

        void Awake(){

            var saloon = GameObject.FindGameObjectWithTag("Saloon");
            if (saloon == null){
                Debug.Log("There is no Saloon game object");
            }
            else{
                allLocations.Add(Location.saloon, saloon.transform);
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
            }

            var mine = GameObject.FindGameObjectWithTag("Mine");
            if (mine == null){
                Debug.Log("There is no Mine game object");
            }
            else{
                allLocations.Add(Location.goldMine, mine.transform);
            }


        }

    }
}
