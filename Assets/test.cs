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
        Vector3 targetPosition = transform.TransformPoint(relativePosition);
        float distance = Vector3.Distance(transform.position, objectToFollow.transform.position);

    }
}
