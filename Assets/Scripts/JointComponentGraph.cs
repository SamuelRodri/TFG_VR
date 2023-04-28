using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponentGraph : MonoBehaviour
{
    public bool isGrabbed;

    [SerializeField] GameObject prevObject;
    [SerializeField] GameObject nextObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed) // Está siendo agarrado
        {
            if (prevObject)
            {
                prevObject.GetComponent<JointComponentGraph>().FollowObject(gameObject);
            }

            if (nextObject)
            {
                nextObject.GetComponent<JointComponentGraph>().FollowObject(gameObject);
            }
        }
    }

    public void FollowObject(GameObject obj)
    {
        transform.position = obj.transform.position;

        if(obj.Equals(prevObject) && nextObject)
        {
            nextObject.GetComponent<JointComponentGraph>().FollowObject(gameObject);
        }

        if(obj.Equals(nextObject) && prevObject)
        {
            prevObject.GetComponent<JointComponentGraph>().FollowObject(gameObject);
        }
    }
}
