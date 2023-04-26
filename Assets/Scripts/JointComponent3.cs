using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponent3 : MonoBehaviour
{
    public GameObject prevObject;
    public GameObject nextObject;

    private Rigidbody rb;
    private Vector3 linearOffsetPrev;
    private Vector3 linearOffsetNext;

    private Vector3 relativePositionNext;
    private Vector3 relativePositionPrev;
    private Quaternion relativeRotationPrev;
    private Quaternion relativeRotationNext;

    public bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (prevObject)
        {
            linearOffsetPrev = transform.position - prevObject.transform.position;
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
            relativeRotationPrev = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
        }

        if (nextObject)
        {
            linearOffsetNext = transform.position - nextObject.transform.position;
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
            relativeRotationNext = Quaternion.Inverse(nextObject.transform.rotation) * transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (isGrabbed) return;

        Vector3 directionPrev = transform.position, directionNext = transform.position;
        Quaternion rotationPrev = transform.rotation, rotationNext = transform.rotation;

        if (prevObject)
        {
            directionPrev = prevObject.transform.TransformPoint(relativePositionPrev);
            Debug.Log(Vector3.Distance(transform.position, directionPrev));
            rotationPrev = prevObject.transform.rotation * relativeRotationPrev;
        }

        if (nextObject)
        {
            directionNext = nextObject.transform.TransformPoint(relativePositionNext);
            rotationNext = nextObject.transform.rotation * relativeRotationNext;
        }

        Vector3 finalPos;
        Quaternion finalRot;

        if (!prevObject)
        {
            finalPos = directionNext;
            finalRot = rotationNext;

        }else if (!nextObject)
        {
            finalPos = directionPrev;
            finalRot = rotationPrev;
        }
        else
        {
            finalPos = Vector3.Lerp(directionPrev, directionNext, 0.5f);
            finalPos = Vector3.MoveTowards(transform.position, finalPos, 20 * Time.deltaTime);
            finalRot = Quaternion.Slerp(rotationPrev, rotationNext, 0.5f);
        }

        transform.rotation = finalRot;
        rb.MovePosition(finalPos);
    }
}
