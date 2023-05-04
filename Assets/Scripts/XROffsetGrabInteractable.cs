using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
        rc = GameObject.Find("RotationController").GetComponent<RotationController>();
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
        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        isGrabbed = false;
        base.OnSelectExited(args);
    }
}