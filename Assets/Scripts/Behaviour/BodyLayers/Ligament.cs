using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.BodyLayers
{
    public class Ligament : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            BodyVisibilityController.ToggleAllLigaments += Toggle;
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