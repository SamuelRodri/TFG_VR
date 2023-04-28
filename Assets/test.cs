using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;

    private Vector3 relativePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        relativePosition = objectToFollow.transform.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(objectToFollow.transform.position, transform.position);

        if(distance > 2)
        {
            var a = objectToFollow.transform.TransformPoint(relativePosition);
            transform.position = a;
        }
        else if(distance < 1)
        {
            Vector3 dir = (objectToFollow.transform.position - transform.position).normalized;
            Vector3 mov = dir * (distance - 1);

            transform.position += mov;
        }
    }
}
