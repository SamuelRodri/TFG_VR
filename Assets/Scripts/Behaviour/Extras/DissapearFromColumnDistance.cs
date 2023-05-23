using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Extras
{
    public class DissapearFromColumnDistance : MonoBehaviour
    {
        [SerializeField] GameObject column;
        [SerializeField] float distance;

        // Update is called once per frame
        void Update()
        {
            if(Vector3.Distance(transform.position, column.transform.position) > distance && SimulationController.areJointsActivated)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
