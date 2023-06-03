using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.UI
{
    public class BoomButton : MonoBehaviour
    {
        [SerializeField] SimulationController controller;
        public float duration = 2f;
        public bool triggerEnter = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller") && triggerEnter)
            {
                controller.MountColumnMode();
            }
        }

        public void EnableTriggerEnter()
        {
            triggerEnter = true;
        }

        private void OnDisable()
        {
            triggerEnter = false;
        }

        private void OnEnable()
        {
            Invoke("EnableTriggerEnter", duration);
        }
    }
}