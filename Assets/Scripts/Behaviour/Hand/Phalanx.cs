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
        private int layerIndex;

        [SerializeField] string parameterToStop;
        [SerializeField] string gripParemeter;
        [SerializeField] string triggerParemeter;

        private void Awake()
        {
            controller = GetComponentInParent<HandController>();
            parentAnimator = GetComponentInParent<Animator>();
            layerIndex = parentAnimator.GetLayerIndex(layerName);
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (!isGrabing && parentAnimator.GetLayerWeight(layerIndex) == 0)
            {
                Debug.Log("resetea");
                //parentAnimator.SetLayerWeight(layerIndex, 1f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggerea");
            Animator parentAnimator = GetComponentInParent<Animator>();
            AnimatorStateInfo stateInfo = parentAnimator.GetCurrentAnimatorStateInfo(0);

            isGrabing = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isGrabing = false;
        }

        public void SetAnimFloats(float grip, float trigger)
        {
            if (!isGrabing)
            {
                parentAnimator.SetFloat(gripParemeter, grip);
                parentAnimator.SetFloat(triggerParemeter, trigger);
            }
        }
    }
}