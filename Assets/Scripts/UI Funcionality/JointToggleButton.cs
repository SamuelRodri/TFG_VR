using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.UI
{
    public class JointToggleButton : MonoBehaviour
    {
        [SerializeField] Sprite enabledSprite;
        [SerializeField] Sprite disabledSprite;

        private bool isEnabled = true;

        [SerializeField] SimulationController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                isEnabled = !isEnabled;

                controller.BreakJointsPress();

                // Sprite change to indicate enabling
                if (isEnabled) GetComponent<Image>().sprite = enabledSprite;
                else GetComponent<Image>().sprite = disabledSprite;
            }
        }
    }
}