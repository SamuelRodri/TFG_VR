using System;
using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
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

    public float firstPrevDistance;
    public float firstNextDistance;

    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private bool beenCalled;
    private bool canMove;

    private float softLimitPosition;
    private float hardLimitPosition = 0.006f;

    private Vector3 targetPosition;

    private bool a;

    private void Awake()
    {
        softLimitPosition = 0.001f;
    }

    private void Start()
    {
        firstNextDistance = 0;
        firstPrevDistance = 0;

        targetPosition = transform.position;

        if (prevObject)
        {
            relativePositionPrev = prevObject.transform.InverseTransformPoint(transform.position);
            relativeRotationPrev = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
            firstPrevDistance = (prevObject.transform.position - transform.position).magnitude;
        }

        if (nextObject)
        {
            relativePositionNext = nextObject.transform.InverseTransformPoint(transform.position);
            relativeRotationNext = Quaternion.Inverse(nextObject.transform.rotation) * transform.rotation;
            firstNextDistance = (nextObject.transform.position - transform.position).magnitude;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (SimulationController.areJointsActivated) return;

        if (lastPosition != transform.position || lastRotation != transform.rotation) // Se ha movido
        {
            CallNeighbours(); // Avisamos a mis vecinos para que se muevan

            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }
    }

    private void LateUpdate()
    {
        beenCalled = false;

    }

    private void CallNeighbours()
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

    // Function to follow the position and rotation of an object
    public void UpdateTransformToFollowObject(JointComponentGraph objectToFollow)
    {
        if (beenCalled || isGrabbed) // Si ya se ha movido en este frame o está agarrado no se mueve
        {
            return;
        }
        
        if (CheckSoftLimitPosition(objectToFollow))
        {
            (Vector3 relativePos, Quaternion relativeRot) = GetRelativeTransform(objectToFollow);

            targetPosition = objectToFollow.transform.TransformPoint(relativePos);
            
            Quaternion targetRotation = objectToFollow.transform.rotation * relativeRot;

            Vector3 dir = objectToFollow.transform.position - transform.position;
            float distance = dir.magnitude;
            dir.Normalize();

            if(distance > softLimitPosition)
            {
                transform.position += dir * (distance - softLimitPosition);
            }
            //else if (distance < 0.001)
            //{
            //    transform.position += dir * (distance - 0.001f);
            //}

            transform.rotation = targetRotation;
            canMove = true;
            beenCalled = true;
            CallNeighbours();
        }
        else
        {
            canMove = false;
        }
    }

    // Funcion que devuelve la posicion y rotacion relativa en funcion del vecino que llama
    private (Vector3, Quaternion) GetRelativeTransform(JointComponentGraph objectToFollow) 
    {
        Vector3 relativePos = (objectToFollow == prevObject) ? relativePositionPrev : relativePositionNext;
        Quaternion relativeRot = (objectToFollow == prevObject) ? relativeRotationPrev : relativeRotationNext;

        return (relativePos, relativeRot);
    }

    // Funcion que comprueba el softlimit para moverse
    private bool CheckSoftLimitPosition(JointComponentGraph objectToFollow)
    {
        float distance = Vector3.Distance(transform.position, objectToFollow.transform.position);
        float firstDistance = (objectToFollow == prevObject) ? firstPrevDistance : firstNextDistance;

        float distanceDiff = Math.Abs(distance - firstDistance);
        
        var move = (distanceDiff > softLimitPosition);
        
        return move;
    }
}