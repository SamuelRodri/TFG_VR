using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.UI
{
    public class BackNormalModeButton : MonoBehaviour
    {
        public float duration = 2f;
        public bool triggerEnter = false;

        [SerializeField] SimulationController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller") && triggerEnter)
            {
                controller.BackToNormalMode();
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