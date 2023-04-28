using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponentGraph : MonoBehaviour
{
    public bool isGrabbed;

    [SerializeField] JointComponentGraph prevObject;
    [SerializeField] JointComponentGraph nextObject;

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed) // Está siendo agarrado
        {
            if (prevObject)
            {
                prevObject.FollowObject(this);
            }

            if (nextObject)
            {
                nextObject.FollowObject(this);
            }
        }
    }

    public void FollowObject(JointComponentGraph obj)
    {
        transform.position = obj.transform.position;

        if(obj.Equals(prevObject) && nextObject)
        {
            nextObject.FollowObject(this);
        }

        if(obj.Equals(nextObject) && prevObject)
        {
            prevObject.FollowObject(this);
        }
    }
}
