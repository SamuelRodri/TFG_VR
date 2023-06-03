using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.UI
{
    public class BoomButton : MonoBehaviour
    {
        [SerializeField] SimulationController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                controller.MountColumnMode();
            }
        }
    }
}