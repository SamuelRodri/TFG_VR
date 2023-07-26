using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    public class RigidLigaments : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SimulationController.OnMountMode += Hide;
            SimulationController.OnBreak += Show;
            SimulationController.OnRestore += Hide;

            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}