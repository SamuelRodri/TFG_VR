using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var deformingMesh = GetComponent<MeshFilter>().mesh.GetSubMesh(0);
        for(var i = 0; i < deformingMesh.indexCount; i++)
        {
            
        }
    }
}
