using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.UI
{
    public class VisibilityButton : MonoBehaviour
    {
        [SerializeField] Sprite visibleSprite;
        [SerializeField] Sprite hiddenSprite;

        private bool isVisible = true;

        [SerializeField] BodyVisibilityController controller;
        [SerializeField] BodyLayer bodyLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                isVisible = !isVisible;

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

                // Sprite change to indicate visibility
                if (isVisible) GetComponent<Image>().sprite = visibleSprite;
                else GetComponent<Image>().sprite = hiddenSprite;
            }
        }
    }
}