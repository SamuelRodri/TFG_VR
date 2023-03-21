using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

// Clase que representa una unica vertebra
public class Vertebra : MonoBehaviour
{
    [Header("Angular X Limits")]
    [SerializeField, Range(-180, 0)]
    private float xLower;
    [SerializeField, Range(-180, 180)]
    private float xUpper;


    [Header("Angular Y Limits")]
    [SerializeField, Range(3, 177)]
    private float yLimit;

    [Header("Angular Z Limits")]
    [SerializeField, Range(3, 177)]
    private float zLimit;

    private ConfigurableJoint joint;
    public bool isGrabbed = false;
    public Quaternion initialRotation;
    public Vector3 initialPosition;

    public Vertebra prevVert;
    public Vertebra nextVert;

    public RotationController rc;

    public Transform cube;
    public Quaternion cubeOffset;
    public Vector3 cubePosOffset;

    public Quaternion rotation;
    public Vector3 position;
    private Vector3 offsetPrev;
    private Vector3 offsetNext;

    private Vector3 initialCubeUp;
    private Vector3 initialCubeRight;
    private Vector3 initialCubeForward;

    private Quaternion initialCubeRotation;

    public Material actualMaterial;
    public Material realMaterial;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();

        if (joint)
        {
            nextVert = GetComponent<ConfigurableJoint>().connectedBody.GetComponent<Vertebra>();
            nextVert.SetPrev(this);
        }

        initialRotation = transform.rotation;
        initialPosition = transform.position;
        rc = GameObject.Find("RotationController").GetComponent<RotationController>();
        rotation = transform.rotation;
        position = transform.position;
    }

    public void SetPrev(Vertebra prevVer)
    {
        prevVert = prevVer;
    }

    public bool HasNext()
    {
        return nextVert != null;
    }

    public bool HasPrev()
    {
        return prevVert != null;
    }

    public void Drop()
    {
        //GetComponent<XROffsetGrabInteractable>().canBeGrabbed = false;
    }

    private void Start()
    {
        if (HasPrev()) offsetPrev = transform.position - prevVert.transform.position;
        if (HasNext()) offsetNext = transform.position - nextVert.transform.position;

        initialRotation = transform.rotation;
        initialPosition = transform.position;

        if (!gameObject.name.Equals("T1")) return;
        cubeOffset = Quaternion.Inverse(cube.rotation) * transform.rotation;
        initialCubeUp = cube.up;
        initialCubeRight = cube.right;
        initialCubeForward = cube.forward;
    }

    private void Update()
    {
        if (GetComponent<XROffsetGrabInteractable>().isGrabbed)
        {
            var rot = GetComponent<XROffsetGrabInteractable>().interactor.transform.rotation * GetComponent<XROffsetGrabInteractable>().offset;
            rotation = rot;
        }

        //if (gameObject.name.Equals("T1")) // Pruebas con cubo
        //{
        //    rotation = CheckHardLimits(cube.rotation * cubeOffset);
        //    initialCubeUp = cube.up;
        //    initialCubeRight = cube.right;
        //    initialCubeForward = cube.forward;
        //    //position = cube.position + cubePosOffset;
        //}
        else
        {
            #region Rotation
            Quaternion prevRot = transform.rotation, nextRot = transform.rotation;
            if (HasPrev())
            {
                prevRot = FollowVertebraRotation(prevVert);
            }

            if (HasNext())
            {
                nextRot = FollowVertebraRotation(nextVert);
            }

            var diffPrev = Quaternion.Angle(prevRot, transform.rotation);
            var diffNext = Quaternion.Angle(nextRot, transform.rotation);

            var t = Mathf.Clamp(diffPrev - diffNext, 0, 1);

            rotation = Quaternion.Slerp(prevRot, nextRot, 0.5f);
            #endregion
        }

        transform.rotation = rotation;
        
        #region Colores
        float angle = 0;
        Color colorA = Color.white, colorB = Color.red;
        if (HasNext())
        {
            Transform next = nextVert.transform;

            switch (rc.vector)
            {
                case RotationController.Vectors.right:
                    angle = Vector3.SignedAngle(transform.right, next.right, Vector3.right); // x
                    break;
                case RotationController.Vectors.up:
                    angle = Vector3.SignedAngle(transform.up, next.up, Vector3.up); // y
                    break;
                case RotationController.Vectors.forward:
                    angle = Vector3.SignedAngle(transform.forward, next.forward, Vector3.forward); // y
                    break;
                case RotationController.Vectors.real:
                    colorB = realMaterial.color;
                    angle = 360;
                    break;
            }

            var material = transform.Find("Ver").GetComponent<MeshRenderer>().material;
            material.color = Color.Lerp(colorA, colorB, Mathf.InverseLerp(0, 10, Mathf.Abs(angle)));
        }

        if (HasPrev())
        {
            Transform prev = prevVert.transform;

            switch (rc.vector)
            {
                case RotationController.Vectors.right:
                    angle = Vector3.SignedAngle(transform.right, prev.right, Vector3.right); // x
                    break;
                case RotationController.Vectors.up:
                    angle = Vector3.SignedAngle(transform.up, prev.up, Vector3.up); // y
                    break;
                case RotationController.Vectors.forward:
                    angle = Vector3.SignedAngle(transform.forward, prev.forward, Vector3.forward); // y
                    break;
                case RotationController.Vectors.real:
                    colorB = realMaterial.color;
                    angle = 360;
                    break;
            }

            var material = transform.Find("Ver").GetComponent<MeshRenderer>().material;
            material.color = Color.Lerp(colorA, colorB, Mathf.InverseLerp(0, 10, Mathf.Abs(angle)));
        }
        #endregion
    }

    private Quaternion FollowVertebraRotation(Vertebra vertebra)
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        var angleX = Vector3.SignedAngle(transform.right, vertebra.transform.right, Vector3.right);
        var angleY = Vector3.SignedAngle(transform.up, vertebra.transform.up, Vector3.up);
        var angleZ = Vector3.SignedAngle(transform.forward, vertebra.transform.forward, Vector3.forward);

        // Tolerance
        if(Mathf.Abs(angleX) > 2 || Mathf.Abs(angleY) > 2 || Mathf.Abs(angleZ) > 1)
        {
            rotation = Quaternion.Slerp(transform.rotation, vertebra.transform.rotation, 0.5f).eulerAngles;
        }

        return Quaternion.Euler(rotation);
    }

    private Quaternion CheckHardLimits2(Quaternion finalRot)
    {
        Quaternion rotation = finalRot;

        var angleXNext = Vector3.SignedAngle(transform.right, nextVert.transform.right, Vector3.forward);
        var angleYNext = Vector3.SignedAngle(transform.up, nextVert.transform.up, Vector3.right);
        var angleZNext = Vector3.SignedAngle(transform.forward, nextVert.transform.forward, Vector3.up);
       
        Vector3 crossProductX = Vector3.Cross(initialCubeForward, cube.forward);
        float dotProductX = Vector3.Dot(crossProductX, cube.right);

        if(Mathf.Abs(angleXNext) > 3f)
        {
            Debug.Log(dotProductX <= 0);
            if(angleXNext > 0 && dotProductX <= 0f)
            {
                Debug.Log("Está limitado");
                cubeOffset = Quaternion.Inverse(cube.rotation) * transform.rotation;
                rotation = transform.rotation;
            }
            else
            {
                Debug.Log("No limita");
            }
        }

        return rotation;
    }

    private Quaternion CheckHardLimits(Quaternion finalRot)
    {
        Vector3 rotation = finalRot.eulerAngles;

        var angleXNext = Vector3.SignedAngle(transform.right, nextVert.transform.right, Vector3.right);
        var angleYNext = Vector3.SignedAngle(transform.up, nextVert.transform.up, Vector3.up);
        var angleZNext = Vector3.SignedAngle(transform.forward, nextVert.transform.forward, Vector3.forward);

        Vector3 crossProductZ = Vector3.Cross(initialCubeUp, cube.up);
        float dotProductZ = Vector3.Dot(crossProductZ,cube.forward);

        Vector3 crossProductY = Vector3.Cross(initialCubeRight, cube.right);
        float dotProductY = Vector3.Dot(crossProductY, cube.up);

        Vector3 crossProductX = Vector3.Cross(initialCubeForward, cube.forward);
        float dotProductX = Vector3.Dot(crossProductX, cube.right);

        if (Mathf.Abs(angleXNext) > 3 || Mathf.Abs(angleYNext) > 2 || Mathf.Abs(angleZNext) > 2)
        {
            cubeOffset = Quaternion.Inverse(cube.rotation) * transform.rotation;

            if (angleXNext < 0 && dotProductX > 0 || angleXNext > 0 && dotProductX < 0 ||
                angleYNext < 0 && dotProductY > 0 || angleYNext > 0 && dotProductY < 0 ||
                angleZNext < 0 && dotProductZ > 0 || angleZNext > 0 && dotProductZ < 0)
            {
                rotation = transform.rotation.eulerAngles;
            }
        }

        return Quaternion.Euler(rotation);
    }
}