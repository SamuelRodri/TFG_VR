using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
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
            GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        }
    }
}