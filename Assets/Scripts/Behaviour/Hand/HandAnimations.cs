using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour.Hand
{
    // Script that controls the animations of the hand when interacting
    public class HandAnimations : MonoBehaviour
    {
        private Animator animator;
        private HandController controller;

        [Range(0, 1)]
        public float grip;
        [Range(0, 1)]
        public float trigger;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<HandController>();
        }

        private void Update()
        {
            //SetAnimFloats(grip, trigger);
            SetAnimFloats(controller.GripValue, controller.TriggerValue);
        }

        // Set floats to blendtree
        public void SetAnimFloats(float gripValue, float triggerValue)
        {
            Phalanx[] phalanxs = GetComponentsInChildren<Phalanx>();

            foreach (Phalanx p in phalanxs)
            {
                p.SetAnimFloats(gripValue, triggerValue);
            }
        }
    }
}