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
        private Rigidbody rb;

        public Vector3 rotation;
        [SerializeField] GameObject cartel;
        [SerializeField] TMP_Text information;

        Vector3 relativePosition;
        Quaternion relativeRotation;

        public GameObject cube;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
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

            //var interactor = cube;
            //relativePosition = interactor.transform.InverseTransformPoint(transform.position);
            //relativeRotation = Quaternion.Inverse(interactor.transform.rotation) * transform.rotation;

            rc = GameObject.Find("RotationController").GetComponent<RotationController>();
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            Debug.Log("AGARRAS");
            interactor = args.interactorObject;
            isGrabbed = true;
            GetComponent<JointComponent3>().isGrabbed = true;
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

            relativePosition = interactor.transform.InverseTransformPoint(transform.position);
            relativeRotation = Quaternion.Inverse(interactor.transform.rotation) * transform.rotation;

            //base.OnSelectEntering(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            isGrabbed = false;
            GetComponent<JointComponent3>().isGrabbed = false;
            cartel.SetActive(false);
            base.OnSelectExited(args);
        }

        private void Update()
        {
            if (isGrabbed)
            {
                //var interactor = cube;
                Vector3 target = interactor.transform.TransformPoint(relativePosition);
                Quaternion targetRot = interactor.transform.rotation * relativeRotation;

                var prev = GetComponent<JointComponent3>().prevObject;
                var next = GetComponent<JointComponent3>().nextObject;

                float prevDistance = 0.01f, nextDistance = 0.01f;
                float prevRot = 0, nextRot = 0;

                if (prev)
                {
                    prevDistance = Vector3.Distance(prev.transform.position, interactor.transform.position);
                    prevRot = Quaternion.Angle(prev.transform.rotation, targetRot);
                }

                if (next)
                {
                    nextDistance = Vector3.Distance(next.transform.position, interactor.transform.position);
                    nextRot = Quaternion.Angle(next.transform.rotation, targetRot);
                }

                var firstDistancePrev = GetComponent<JointComponent3>().firstDistancePrev;
                var firstDistanceNext = GetComponent<JointComponent3>().firstDistanceNext;

                rb.MovePosition(target);

                transform.rotation = targetRot;
                //else
                //{
                //    isGrabbed = false;
                //}
            }
        }
    }
}