using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace TFG.Behaviour
{
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

        public RotationController rc;

        public Vector3 rotation;

        [SerializeField] GameObject cartel;
        [SerializeField] TMP_Text information;

        // Start is called before the first frame update
        void Start()
        {
            cartel = transform.GetChild(0).gameObject;
            information = cartel.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            information.text = name;

            // Create attach point
            if (!attachTransform)
            {
                GameObject grab = new GameObject("Grab Pivot");
                grab.transform.SetParent(transform, false);
                attachTransform = grab.transform;
            }

            initialAttachLocalPos = attachTransform.localPosition;
            initialAttachLocalRot = attachTransform.localRotation;
            rc = GameObject.Find("RotationController").GetComponent<RotationController>();
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            interactor = args.interactorObject;
            isGrabbed = true;
            GetComponent<JointComponent2>().isGrabbed = true;
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

            cartel.SetActive(true);
            offset = Quaternion.Inverse(interactor.transform.rotation) * transform.rotation;
            positionOffset = transform.position - interactor.transform.position;
            base.OnSelectEntering(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            isGrabbed = false;
            GetComponent<JointComponent2>().isGrabbed = false;
            cartel.SetActive(false);
            base.OnSelectExited(args);
        }
    }
}