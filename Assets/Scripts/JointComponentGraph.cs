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

    private float firstPrevDistance;
    private float firstNextDistance;

    private void Start()
    {
        if (prevObject)
        {
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
            relativeRotationPrev = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
            firstPrevDistance = Vector3.Distance(transform.position, prevObject.transform.position);
        }

        if (nextObject)
        {
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
            relativeRotationNext = Quaternion.Inverse(nextObject.transform.rotation) * transform.rotation;
            firstNextDistance = Vector3.Distance(transform.position, nextObject.transform.position);
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
        float distance = Vector3.Distance(transform.position, objectToFollow.transform.position);
        float firstDistance = (objectToFollow == prevObject) ? firstPrevDistance : firstNextDistance;

        if(Mathf.Abs(distance - firstDistance) > 0.0005)
        {
            (Vector3 relativePos, Quaternion relativeRot) = GetRelativeTransform(objectToFollow);

            Vector3 targetPosition = objectToFollow.transform.TransformPoint(relativePos);
            Quaternion targetRotation = objectToFollow.transform.rotation * relativeRot;

            targetPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 8.5f);
            transform.SetPositionAndRotation(targetPosition, targetRotation);
        }

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

    private void UpdateRelativeTransform(JointComponentGraph objectToFollow)
    {
        if(objectToFollow == prevObject)
        {
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
        }
        else if(objectToFollow == nextObject)
        {
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
        }
    }
}