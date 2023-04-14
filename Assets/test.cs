using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        var vertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
