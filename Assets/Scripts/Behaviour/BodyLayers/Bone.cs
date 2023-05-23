using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.BodyLayers
{
    public class Bone : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            BodyVisibilityController.ToggleSkeleton += Toggle;
        }

        private void Toggle()
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
        }
    }
}