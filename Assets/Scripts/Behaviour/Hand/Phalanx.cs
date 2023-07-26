using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour.Hand
{
    public class Phalanx : MonoBehaviour
    {
        [SerializeField] string layerName;
        private HandController controller;
        public bool isGrabing;

        private Animator parentAnimator;
        private HandController hand;

        private int layerIndex;

        public float actualGrip;
        public float actualTrigger;
        public GameObject grabbedObject;

        [SerializeField] string gripParemeter;
        [SerializeField] string triggerParemeter;

        private void Awake()
        {
            controller = GetComponentInParent<HandController>();
            parentAnimator = GetComponentInParent<Animator>();
            layerIndex = parentAnimator.GetLayerIndex(layerName);
            hand = GetComponentInParent<HandController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == hand.grabbedObject || other.transform.parent.gameObject == hand.grabbedObject)
            {
                isGrabing = true;
                grabbedObject = hand.grabbedObject;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == hand.grabbedObject || other.transform.parent.gameObject == hand.grabbedObject)
            {
                isGrabing = true;
                grabbedObject = hand.grabbedObject;
            }
        }

        private void Update()
        {
            if (!hand.grabbedObject && grabbedObject)
            {
                isGrabing = false;
                grabbedObject = null;
            }
        }

        public void SetAnimFloats(float grip, float trigger)
        {
            if (!isGrabing || grip < actualGrip || trigger < actualTrigger)
            {
                var newGrip = Mathf.Lerp(actualGrip, grip, 0.1f);
                var newTrigger = Mathf.Lerp(actualTrigger, trigger, 0.1f);

                parentAnimator.SetFloat(gripParemeter, newGrip);
                parentAnimator.SetFloat(triggerParemeter, newTrigger);

                actualGrip = newGrip;
                actualTrigger = newTrigger;
            }
        }
    }
}