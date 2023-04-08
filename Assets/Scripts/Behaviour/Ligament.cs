using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    public class Ligament : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            BodyVisibilityController.ToggleLigaments += Toggle;
        }

        private void Toggle()
        {
            GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        }
    }
}