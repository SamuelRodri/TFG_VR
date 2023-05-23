using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.UI
{
    // Button that initiates the event to restore the column
    public class RestoreColumnButton : MonoBehaviour
    {
        [SerializeField] SimulationController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                controller.RestoreColumn();
            }
        }
    }
}