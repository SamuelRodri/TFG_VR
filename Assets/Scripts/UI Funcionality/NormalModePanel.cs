using System;
using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.UI
{
    public class NormalModePanel : MonoBehaviour
    {
        private void Start()
        {
            SimulationController.OnMountMode += DesactivateChildren;
            SimulationController.OnNormalMode += ActivateChildren;
        }

        private void DesactivateChildren()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void ActivateChildren()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}