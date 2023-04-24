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
    private Vector3 relativeScale;

    private Vector3 finalPosNext;
    private Vector3 finalPosPrev;

    private Quaternion finalRotNext;
    private Quaternion finalRotPrev;

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
        if (nextObject) // Tiene "hijo"
        {
            Vector3 target = nextObject.transform.TransformPoint(relativePositionNext);
            Vector3 newPosition = Vector3.Lerp(transform.position, target, 0.7f);

            finalPosNext = Vector3.Distance(newPosition, transform.position) > 0.005 ? newPosition : transform.position;

            Quaternion targetRot = nextObject.transform.rotation * relativeRotationNext;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, 0.6f);

            finalRotNext = newRotation;
        }

        if (prevObject) // Tiene "padre"
        {
            Vector3 target = prevObject.transform.TransformPoint(relativePositionPrev);
            Vector3 newPosition = Vector3.Lerp(transform.position, target, 0.7f);

            finalPosPrev = Vector3.Distance(newPosition, transform.position) > 0.005 ? newPosition : transform.position;

            Quaternion targetRot = prevObject.transform.rotation * relativeRotationPrev;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

            finalRotPrev = newRotation;
        }

        if (!isGrabbed)
        {
            if (!nextObject) // No tiene hijo
            {
                transform.position = finalPosPrev;
                transform.rotation = finalRotPrev;
            }
            else if (!prevObject)
            {
                transform.position = finalPosNext;
                transform.rotation = finalRotNext;
            }
            else
            {
                // Aquí va raro, si lo pones a 1 va bien pero solo coge el next
                transform.position = Vector3.Slerp(finalPosPrev, finalPosNext, 1f); 
                transform.rotation = Quaternion.Lerp(finalRotPrev, finalRotNext, 1f);
            }
        }
    }
}