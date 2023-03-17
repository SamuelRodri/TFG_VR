using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase que representa una union de dos vertebras
// Se asocia a la segunda de dos vertebras
public class VertebralJoint : MonoBehaviour
{
    // Limites
    [SerializeField] float minLim;
    [SerializeField] float maxLim;

    private HingeJoint joint;

    private void Awake()
    {
        // Configuramos el HingeJoint
        joint = GetComponent<HingeJoint>();

        joint.axis = Vector3.forward; // Rotará sobre el eje Z

        //joint.connectedBody = GetComponent<Vertebra>().NextVertebra.GetComponent<Rigidbody>();

        // Configuramos los limites
    
    }
}