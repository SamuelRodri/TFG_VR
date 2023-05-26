using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using TFG.Behaviour.Controllers;
using TFG.Behaviour.Extras;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TFG.Behaviour.VR
{
    [RequireComponent(typeof(InfoBoardEntity))]
    public class XROffsetGrabInteractable : XRGrabInteractable
    {
        private Vector3 initialAttachLocalPos;
        private Quaternion initialAttachLocalRot;
        public bool isGrabbed = false;
        public IXRSelectInteractor interactor;
        public Quaternion offset;
        private Vector3 positionOffset;

        private Vector3 controllerDiff;
        public Vector3 vertebraDiff;

        public Vector3 rotation;

        // Start is called before the first frame update
        void Start()
        {
            // Create attach point
            if (!attachTransform)
            {
                GameObject grab = new GameObject("Grab Pivot");
                grab.transform.SetParent(transform, false);
                attachTransform = grab.transform;
            }

            initialAttachLocalPos = attachTransform.localPosition;
            initialAttachLocalRot = attachTransform.localRotation;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            interactor = args.interactorObject;
            
            isGrabbed = true;
            if (interactor is XRDirectInteractor)
            {
                attachTransform.position = interactor.transform.position;
                attachTransform.rotation = interactor.transform.rotation;
            }
            else
            {
                attachTransform.localPosition = initialAttachLocalPos;
                attachTransform.localRotation = initialAttachLocalRot;
            }

            offset = Quaternion.Inverse(interactor.transform.rotation) * transform.rotation;
            positionOffset = transform.position - interactor.transform.position;

            if (!SimulationController.areJointsActivated)
            {
                GetComponent<InfoBoardEntity>().SetBoardActive();
            }

            base.OnSelectEntering(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            GetComponent<InfoBoardEntity>().SetBoardInactive();
            isGrabbed = false;
            base.OnSelectExited(args);
        }
    }
}