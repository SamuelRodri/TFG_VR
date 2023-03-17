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

    private void Update()
    {
        //if (isGrabbed)
        //{
        //    #region Rotation
        //    var rot = interactor.transform.rotation * offset;
        //    rotation = rot.eulerAngles;
        //    //transform.localRotation = Quaternion.Euler(rot);
        //    #endregion
        //}


        #region Comments
        /*if (isGrabbed) // Si el objeto está siendo agarrado
        {
            #region Rotation
            var rotation = interactor.transform.rotation * offset;
            var rot = rotation.eulerAngles;  // Rotacion hacia donde tiene que ir la vértebra

            Vertebra ver = GetComponent<Vertebra>();

            if (ver.HasNext())
            {
                Vertebra next = ver.nextVert;

                /* --------------- DECOMENTAR PARA SEGUIR POR AQUI ---------------------
                //var x = Mathf.DeltaAngle(transform.eulerAngles.x, rot.x);
                //var y = Mathf.DeltaAngle(transform.eulerAngles.y, rot.y);
                //var z = Mathf.DeltaAngle(transform.eulerAngles.z, rot.z);

                //var x2 = Mathf.DeltaAngle(next.transform.eulerAngles.x, transform.eulerAngles.x);
                //var y2 = Mathf.DeltaAngle(next.transform.eulerAngles.y, transform.eulerAngles.y);
                //var z2 = Mathf.DeltaAngle(next.transform.eulerAngles.z, transform.eulerAngles.z);
                
                //if(Mathf.Abs(x) + Mathf.Abs(x2) > 15)
                //{
                //    Debug.Log("Se Pasa para Next");
                //    rot.x = transform.eulerAngles.x;
                //}
                /* ----------------------------------------------------------------------------------*
                //rot.y = ClampAngle(rot.y, transform.eulerAngles.y, next.transform.eulerAngles.y + 12);
                /*rotation = Quaternion.AngleAxis(toMove.x, transform.right) * rotation; // x
                rotation = Quaternion.AngleAxis(toMove.y, transform.up) * rotation;  // y
                rotation = Quaternion.AngleAxis(toMove.z, transform.forward) * rotation; // z
            }
            transform.localRotation = Quaternion.Euler(rot);
            #endregion
        }*/
        #endregion
    }
}