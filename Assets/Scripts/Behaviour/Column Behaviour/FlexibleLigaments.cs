using System;
using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    public class FlexibleLigaments : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SimulationController.OnMountMode += Hide;
            SimulationController.OnNormalMode += Show;
            BodyVisibilityController.ToggleFibrousLigaments += ToggleVisibility;
        }

        private void ToggleVisibility()
        {
            if (GetComponent<MeshRenderer>())
            {
                GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
            }
            else if (GetComponent<SkinnedMeshRenderer>())
            {
                GetComponent<SkinnedMeshRenderer>().enabled = !GetComponent<SkinnedMeshRenderer>().enabled;
            }
            GetComponent<MeshCollider>().enabled = !GetComponent<MeshCollider>().enabled;
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
