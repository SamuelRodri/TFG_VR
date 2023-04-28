using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponentGraph : MonoBehaviour
{
    public bool isGrabbed;

    public JointComponentGraph prevObject;
    public JointComponentGraph nextObject;

    private Vector3 relativePositionNext;
    private Vector3 relativePositionPrev;
    private Quaternion relativeRotationPrev;
    private Quaternion relativeRotationNext;

    private void Start()
    {
        if (prevObject)
        {
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
            relativeRotationPrev = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
        }

        if (nextObject)
        {
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
            relativeRotationNext = Quaternion.Inverse(nextObject.transform.rotation) * transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed) // Está siendo agarrado
        {
            if (prevObject) // Si tiene previo hacemos que nos siga
            {
                prevObject.UpdateTransformToFollowObject(this);
            }

            if (nextObject) // Si tiene siguiente hacemos que nos siga
            {
                nextObject.UpdateTransformToFollowObject(this);
            }
        }
    }

    // Function to follow the position and rotation of an object
    public void UpdateTransformToFollowObject(JointComponentGraph objectToFollow)
    {
        (Vector3 relativePos, Quaternion relativeRot) target = GetRelativeTransform(objectToFollow);

        Vector3 targetPosition = objectToFollow.transform.TransformPoint(target.relativePos);
        Quaternion targetRotation = objectToFollow.transform.rotation * target.relativeRot;

        transform.SetPositionAndRotation(targetPosition, targetRotation);

        JointComponentGraph neighbor = (nextObject && objectToFollow != nextObject) ? nextObject :
                                       (prevObject && objectToFollow != prevObject) ? prevObject : null;

        if (neighbor) neighbor.UpdateTransformToFollowObject(this);
    }

    // Funcion que devuelve la posicion y rotacion relativa en funcion del vecino que llama
    private (Vector3, Quaternion) GetRelativeTransform(JointComponentGraph objectToFollow) 
    {
        Vector3 relativePos = (objectToFollow == prevObject) ? relativePositionPrev : relativePositionNext;
        Quaternion relativeRot = (objectToFollow == prevObject) ? relativeRotationPrev : relativeRotationNext;

        return (relativePos, relativeRot);
    }
}