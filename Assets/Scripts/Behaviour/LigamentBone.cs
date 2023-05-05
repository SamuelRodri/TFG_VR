using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    public class LigamentBone : MonoBehaviour
    {
        [SerializeField] GameObject parentVertebra;
        [SerializeField] GameObject parentLigament;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Start()
        {
            parentVertebra = transform.parent.gameObject;
            SimulationController.OnBreak += ChangeParent;
            SimulationController.OnRestore += ChangeParent;
        }

        private void ChangeParent()
        {
            if (transform.parent.name == parentVertebra.name)
            {
                transform.parent = parentLigament.transform;
            }
            else
            {
                transform.SetPositionAndRotation(initialPosition, initialRotation);
                transform.parent = parentVertebra.transform;
            }
        }
    }
}