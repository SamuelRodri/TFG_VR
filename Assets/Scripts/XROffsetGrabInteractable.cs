using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    private bool isGrabbed;
    IXRSelectInteractor interactor;
    private Quaternion offset;
    private Vector3 controllerDiff;
    public Vector3 vertebraDiff;
    public Transform cube;
    public Vector3 cubeOffset;

    Vector3 a, b, c;

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
        cubeOffset = transform.localRotation.eulerAngles - cube.rotation.eulerAngles;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        interactor = args.interactorObject;
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
        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        isGrabbed = false;
        base.OnSelectExited(args);
    }

    private void Update()
    {
        //if (gameObject.name.Equals("T2")) { return; }
        //var rotate = cube.rotation.eulerAngles;
        //var cubeRotation = cube.rotation * cubeOffset;
        //Debug.Log(cubeRotation.eulerAngles);
        //cubeRotation = Quaternion.AngleAxis(0, transform.right) * cubeRotation; // x
        //cubeRotation = Quaternion.AngleAxis(cubeRotation.eulerAngles.y, transform.up) * cubeRotation;  // y
        //cubeRotation = Quaternion.AngleAxis(0, transform.forward) * cubeRotation; // z
        //var next2 = GetComponent<Vertebra>().nextVert.transform;
        //Debug.Log(rotate.y); 
        //rotate.y = ClampAngle(rotate.y, transform.eulerAngles.y, AddAngles(next2.eulerAngles.y, 12)); // C, V, N + limit
        //transform.rotation = Quaternion.Euler(rotate);

        if (isGrabbed) // Si el objeto está siendo agarrado
        {
            #region Rotation
            var rotation = interactor.transform.rotation * offset;
            var rot = rotation.eulerAngles;  // Rotacion hacia donde tiene que ir la vértebra

            Vertebra ver = GetComponent<Vertebra>();

            if (ver.HasNext())
            {
                Vertebra next = ver.nextVert;

                /* --------------- DECOMENTAR PARA SEGUIR POR AQUI ---------------------*/
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
                /* ----------------------------------------------------------------------------------*/
                //rot.y = ClampAngle(rot.y, transform.eulerAngles.y, next.transform.eulerAngles.y + 12);
                /*rotation = Quaternion.AngleAxis(toMove.x, transform.right) * rotation; // x
                rotation = Quaternion.AngleAxis(toMove.y, transform.up) * rotation;  // y
                rotation = Quaternion.AngleAxis(toMove.z, transform.forward) * rotation; // z*/
            }
            transform.localRotation = Quaternion.Euler(rot);
            #endregion
        }
    }

    static float MathMod(float a, float b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle <= 180)
        {
            if (angle > min)
            {
                return Mathf.Clamp(angle, min, max);
            }
            else
            {
                return angle;
            }
        }
        else
        {
            return Mathf.Clamp(angle - 360, -max, min);
        }
    }

    private float AddAngles(float a, float b)
    {
        return (a + b) % 360;
    }

    private Vector3 SubstractAngles(Vector3 a, Vector3 b)
    {
        Vector3 result;

        result.x = SignedDistanceBetweenAngles(b.x, a.x);
        result.y = SignedDistanceBetweenAngles(b.y, a.y);
        result.z = SignedDistanceBetweenAngles(b.z, a.z);
        return result;
    }

    public static float SignedDistanceBetweenAngles(float angle1, float angle2)
    {
        float diff = (angle2 - angle1 + 180) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }
}