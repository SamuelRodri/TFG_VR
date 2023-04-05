using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

namespace TFG.UI
{
    public class JointToggleButton : MonoBehaviour
    {
        [SerializeField] SimulationController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                controller.BreakJointsPress();
            }
        }
    }
}