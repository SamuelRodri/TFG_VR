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

    private Vector3 lastPosition;

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

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed) // Está siendo agarrado
        {
                if (prevObject) // Si tiene previo hacemos que nos siga
                {
                    Debug.Log("Esto no debería verse");
                    prevObject.FollowObject(this);
                }

                if (nextObject) // Si tiene siguiente hacemos que nos siga
                {
                    Debug.Log("Esto no debería verse");
                    nextObject.FollowObject(this);
                }
        }
    }

    public void FollowObject(JointComponentGraph obj)
    {
        if (obj.Equals(prevObject))
        {
            transform.position = obj.transform.TransformPoint(relativePositionPrev);

            if (nextObject)
            {
                nextObject.FollowObject(this);
            }
        }

        if (obj.Equals(nextObject))
        {
            transform.position = obj.transform.TransformPoint(relativePositionNext);

            if (prevObject)
            {
                prevObject.FollowObject(this);
            }
        }
    }
}