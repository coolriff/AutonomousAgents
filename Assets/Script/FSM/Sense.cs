using UnityEngine;
using System.Collections;

namespace FSM{

    public enum SenseType{
        sight,
        hearing,
        smell
    };

    public struct Sense{
        public int sender;
        public int receiver;
        public SenseType senseType;

        public Sense(int _sender, int _receiver, SenseType _senseType){
            sender = _sender;
            receiver = _receiver;
            senseType = _senseType;
        }
    }
}
