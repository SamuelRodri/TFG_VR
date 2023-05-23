using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using TFG.Behaviour.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.UI
{
    // Button that triggers the column break event
    public class JointToggleButton : MonoBehaviour
    {
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;

        private bool isEnabled = true;
        private Image buttonImage;
        [SerializeField] SimulationController controller;

        private void Awake()
        {
            buttonImage = GetComponent<Image>();
        }

        private void Start()
        {
            SimulationController.OnRestore += RestoreButtonImage;
        }

        private void RestoreButtonImage()
        {
            buttonImage.sprite = enabledSprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                isEnabled = !isEnabled;
                controller.BreakJointsPress();

                // Sprite change to indicate enabling
                if (isEnabled) buttonImage.sprite = enabledSprite;
                else buttonImage.sprite = disabledSprite;
            }
        }
    }
}