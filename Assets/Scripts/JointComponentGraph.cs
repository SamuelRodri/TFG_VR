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

    private Vector3 lastPost;

    private bool isMoved;

    private float softLimitPosition = 0.001f;

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

        lastPost = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed) return;

        if(lastPost != transform.position) // Se ha movido
        {
            if (prevObject) // Si tiene previo hacemos que nos siga
            {
                prevObject.UpdateTransformToFollowObject(this);
            }

            if (nextObject) // Si tiene siguiente hacemos que nos siga
            {
                Debug.Log("Llama a next");
                nextObject.UpdateTransformToFollowObject(this);
            }

            lastPost = transform.position;
        }
    }

    private void LateUpdate()
    {
        isMoved = false;
    }

    // Function to follow the position and rotation of an object
    public void UpdateTransformToFollowObject(JointComponentGraph objectToFollow)
    {
        if (isMoved || isGrabbed) return;
        
        (Vector3 relativePos, Quaternion relativeRot) = GetRelativeTransform(objectToFollow);

        Vector3 targetPosition = objectToFollow.transform.TransformPoint(relativePos);
        Quaternion targetRotation = objectToFollow.transform.rotation * relativeRot;

        Debug.Log($"Mi prev está en la posicion: {prevObject.transform.position}");
        Debug.Log($"Y mi target position en: {targetPosition}");
        // Soft Limit
        float prevDistance = (prevObject) ? Vector3.Distance(transform.position, prevObject.transform.position) : 0;
        float nextDistance = (nextObject) ? Vector3.Distance(transform.position, nextObject.transform.position) : 0;

        float prevDistanceDiff = Math.Abs(prevDistance - firstPrevDistance);
        float nextDistanceDiff = Math.Abs(nextDistance - firstNextDistance);

        Debug.Log(prevDistance);

        if (prevDistanceDiff > softLimitPosition || nextDistanceDiff > softLimitPosition)
        {
            transform.position = targetPosition;

            isMoved = true;

        }

        transform.rotation = targetRotation;

        
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