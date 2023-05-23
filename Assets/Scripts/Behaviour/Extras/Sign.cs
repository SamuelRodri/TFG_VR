using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace TFG.Behaviour.Extras
{
    public class Sign : MonoBehaviour
    {
        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Camera>().gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        }
    }
}