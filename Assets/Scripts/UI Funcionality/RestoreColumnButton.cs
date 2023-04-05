using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

namespace TFG.UI
{
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