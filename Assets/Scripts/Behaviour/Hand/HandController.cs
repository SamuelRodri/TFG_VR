using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace TFG.Behaviour.Hand
{
    public enum HandType
    {
        left, right
    }

    public class HandController : XRDirectInteractor
    {
        public HandType handType;
        private float _gripValue, _triggerValue;
        private bool _isGrabbing;
        public GameObject grabbedObject;

        // Getters
        public float GripValue { get => _gripValue; }
        public float TriggerValue { get => _triggerValue; }
        public bool IsGrabbing { get => _isGrabbing; }

        [SerializeField] Text information;

        private void Update()
        {
            var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand |
                (handType.Equals(HandType.left) ? UnityEngine.XR.InputDeviceCharacteristics.Left : UnityEngine.XR.InputDeviceCharacteristics.Right) |
                UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

            foreach (var device in leftHandedControllers)
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out _gripValue);
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out _triggerValue);
            }

            _isGrabbing = (_gripValue > 0 || _triggerValue > 0);
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            //grabbedObject = args.interactableObject.transform.gameObject;
            information.text = args.interactableObject.transform.name;
            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            //grabbedObject = null;
            base.OnSelectExited(args);
        }
    }
}