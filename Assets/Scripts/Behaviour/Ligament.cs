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
            if (GetComponent<MeshRenderer>())
            {
                GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
            }else if (GetComponent<SkinnedMeshRenderer>())
            {
                GetComponent<SkinnedMeshRenderer>().enabled = !GetComponent<SkinnedMeshRenderer>().enabled;
            }
            GetComponent<MeshCollider>().enabled = !GetComponent<MeshCollider>().enabled;
        }
    }
}