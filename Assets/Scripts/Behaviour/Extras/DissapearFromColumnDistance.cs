using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Extras
{
    // Class that controls the disappearance of a body layer when the column moves away
    public class DissapearFromColumnDistance : MonoBehaviour
    {
        [SerializeField] GameObject column;
        [SerializeField] float limitDistance;

        // Update is called once per frame
        void Update()
        {
            float actualDistance = Vector3.Distance(
                transform.position, 
                column.transform.position);

            if (actualDistance > limitDistance && SimulationController.areJointsActivated)
            {
                foreach (Transform child in transform) 
                {
                    // Hide all children belonging to the body layer
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    // We show all the children that belong to the body layer
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}