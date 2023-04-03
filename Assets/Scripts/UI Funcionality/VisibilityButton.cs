using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

namespace TFG.UI
{
    public class VisibilityButton : MonoBehaviour
    {
        [SerializeField] BodyVisibilityController controller;
        [SerializeField] BodyLayer bodyLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                switch (bodyLayer)
                {
                    case BodyLayer.skeleton:
                        controller.ToggleSkeletonVisibility();
                        break;
                    case BodyLayer.ligaments:
                        controller.ToggleLigamentsVisibility();
                        break;
                    case BodyLayer.nervs:
                        controller.ToggleNervsVisibility();
                        break;
                    case BodyLayer.cardio:
                        controller.ToggleCardioVisibility();
                        break;
                }
            }
        }
    }
}