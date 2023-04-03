using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    public class SimulationController : MonoBehaviour
    {
        public bool areJointsActivated = true;
        
        private void Update()
        {
            if(areJointsActivated == false)
            {
                var a = Object.FindObjectsOfType<CartilaginousJoint>();
                foreach(var b in a)
                {
                    b.BreakJoints();
                }
            }
        }
    }
}