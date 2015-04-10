using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FSM{

    public class AgentManager : MonoBehaviour{

        public List<Agent> agents;
        public Agent defaultAgent;

        public int AddAgent(Agent agent){
            agents.Add(agent);
            return agents.IndexOf(agent);
        }

        public Agent GetAgent(int id){

            return agents.First(agent => agent.ID == id);
 
            //foreach (Agent agent in agents){
            //    if (agent.ID == id){
            //        defaultAgent = agent;
            //        return defaultAgent;
            //    }
            //    else{
            //        continue;
            //    }
            //}
            //return defaultAgent;
        }

        public void DeleteAgent(Agent agent){
            agents.Remove(agent);
        }
    }
}