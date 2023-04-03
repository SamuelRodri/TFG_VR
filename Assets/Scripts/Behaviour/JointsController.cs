using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    public class JointsController : MonoBehaviour
    {
        public bool areJointsActivated = true;
        
        public void BreakJoints()
        {
            areJointsActivated = false;

            var joints = Object.FindObjectsOfType<CartilaginousJoint>();

            foreach (var joint in joints)
            {
                joint.BreakLinks();
            }
        }
    }
}