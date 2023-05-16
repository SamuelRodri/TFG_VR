using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponent2 : MonoBehaviour
{
    public GameObject prevObject;
    public GameObject nextObject;

    private Rigidbody rb;
    private Vector3 linearoffset;
    private Quaternion rotationalOffset;
    private Quaternion rotationInicial;

    public static float umbral = 0f;

    private Vector3 antPrev;
    Vector3 posicionRelativa;
    Vector3 rotacionRelativa;

    Matrix4x4 translationMatrix;
    Matrix4x4 rotationMatrix;

    public bool isGrabbed = false;

    private Vector3 relativePositionNext;
    private Quaternion relativeRotationNext;
    private Vector3 relativePositionPrev;
    private Quaternion relativeRotationPrev;
    private Vector3 lastPosition;

    private Vector3 finalPosNext;
    private Vector3 finalPosPrev;

    private Quaternion finalRotNext;
    private Quaternion finalRotPrev;

    public bool canMove = true;

    float step = 0f;
    float speed = 20;
    bool a = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        finalPosPrev = transform.position;
        finalPosNext = transform.position;

        if (nextObject)
        {
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
            relativeRotationNext = Quaternion.Inverse(nextObject.transform.rotation) * transform.rotation;
        }

        if (prevObject)
        {
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
            relativeRotationPrev = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float prevDistance = 0, nextDistance = 0;
        float prevRot = 0, nextRot = 0;

        finalPosPrev = transform.position;
        finalPosNext = transform.position;

        if (nextObject) // Tiene "hijo"
        {
            Vector3 target = nextObject.transform.TransformPoint(relativePositionNext);

            //Vector3 newPosition = Vector3.Slerp(transform.position, target, 1);
            //finalPosNext = Vector3.Distance(nextObject.transform.position, target) > 0.005 ? newPosition : transform.position;
            finalPosNext = target;

            Quaternion targetRot = nextObject.transform.rotation * relativeRotationNext;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, 1f);

            finalRotNext = Quaternion.Angle(transform.rotation, newRotation) > 3 ? newRotation : transform.rotation;

            //nextDistance = Vector3.Distance(transform.position, newPosition);

            nextRot = Quaternion.Angle(transform.rotation, newRotation);

        }

        if (prevObject) // Tiene "padre"
        {
            Vector3 target = prevObject.transform.TransformPoint(relativePositionPrev);

            //Vector3 newPosition = Vector3.Lerp(transform.position, target, 1);
            //finalPosPrev = Vector3.Distance(transform.position, target) > 0.005 ? newPosition : transform.position;
            finalPosPrev = target;

            Quaternion targetRot = prevObject.transform.rotation * relativeRotationPrev;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, 1f);

            finalRotPrev = Quaternion.Angle(transform.rotation, newRotation) > 3 ? newRotation : transform.rotation;

            //prevDistance = Vector3.Distance(transform.position, newPosition);

            prevRot = Quaternion.Angle(transform.rotation, newRotation);
        }

        if (!isGrabbed)
        {
            if (prevDistance > 0.03 || nextDistance > 0.03 || nextRot > 5 || prevRot > 5)
            {
                //return;
            }

            if (!nextObject) // No tiene hijo
            {
                transform.position = finalPosPrev;
                transform.rotation = finalRotPrev;
            }
            else if (!prevObject)
            {
                Debug.Log($"Soy {name} y me voy a mover a la posicion de mi next {finalPosNext}");
                transform.position = finalPosNext;
                transform.rotation = finalRotNext;
            }
            else
            {
                transform.position = Vector3.Slerp(finalPosPrev, finalPosNext, 1f);
                transform.rotation = Quaternion.Lerp(finalRotPrev, finalRotNext, 0.5f);
            }
        }
    }
}