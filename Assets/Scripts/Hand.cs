using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    left, right
}

public class Hand : XRDirectInteractor
{
    public HandType handType;
    float gripValue, triggerValue;

    private void Update()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | 
            (handType.Equals(HandType.left) ? UnityEngine.XR.InputDeviceCharacteristics.Left : UnityEngine.XR.InputDeviceCharacteristics.Right) |
            UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        foreach (var device in leftHandedControllers)
        {
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out gripValue);
            device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerValue);
        }

        Anim();
    }

    private void Anim()
    {
        GetComponent<Animator>().SetFloat("Grip", gripValue);
        GetComponent<Animator>().SetFloat("Trigger", triggerValue);
    }
}