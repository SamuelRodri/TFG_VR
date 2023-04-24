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

    public float umbral = 0.006f;

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
                Vector3 newPosition = Vector3.Lerp(transform.position, target, 50 * Time.deltaTime);

                finalPosNext = newPosition;

                Quaternion targetRot = nextObject.transform.rotation * relativeRotationNext;
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, 50 * Time.deltaTime);

                transform.rotation = newRotation;
        }

        if (prevObject) // Tiene "padre"
        {
                Vector3 target = prevObject.transform.TransformPoint(relativePositionPrev);
                Vector3 newPosition = Vector3.Lerp(transform.position, target, 50 * Time.deltaTime);

                finalPosPrev = newPosition;

                Quaternion targetRot = prevObject.transform.rotation * relativeRotationPrev;
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRot, 50 * Time.deltaTime);

                transform.rotation = newRotation;
        }

        if (!isGrabbed)
        {
            if (prevObject) // Si tiene padre
            {
                if (nextObject) // Si tiene hijo
                {
                    if (!prevObject.GetComponent<JointComponent2>().isGrabbed && nextObject.GetComponent<JointComponent2>().isGrabbed)
                    {
                        transform.position = Vector3.Slerp(finalPosPrev, finalPosNext, 0.8f);
                    }else if (prevObject.GetComponent<JointComponent2>().isGrabbed && !nextObject.GetComponent<JointComponent2>().isGrabbed)
                    {
                        transform.position = Vector3.Slerp(finalPosPrev, finalPosNext, 0.2f);
                    }
                    else
                    {
                        transform.position = Vector3.Slerp(finalPosPrev, finalPosNext, 0.5f);
                    }
                }
                else
                {
                    transform.position = finalPosPrev;
                }
            }
            else // Si no tiene padre
            {
                if (nextObject) // Si tiene hijo
                {
                    transform.position = finalPosNext;
                }
            }
            
        }
    }
}