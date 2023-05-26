using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TFG.Behaviour.Hand
{
    public class IndexController : XRDirectInteractor
    {
        [SerializeField] HandController handController;

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            handController.grabbedObject = args.interactableObject.transform.gameObject;
            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            handController.grabbedObject = null;
            base.OnSelectExited(args);
        }
    }
}