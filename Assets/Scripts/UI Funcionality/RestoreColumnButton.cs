using System.Collections;
using System.Collections.Generic;
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
                Debug.Log("Restore");
                controller.RestoreColumn();
            }
        }
    }
}