using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Utils
{
    // Set a parent object breking the hierarchy
    public class BreakHierarchy : MonoBehaviour
    {
        public GameObject parent;

        public void SetParent()
        {
            if (transform.childCount > 0)
            {
                try
                {
                    transform.parent = parent.transform;
                    transform.GetChild(0).GetComponent<BreakHierarchy>().SetParent();
                }
                catch
                {
                    Debug.Log("No lo tiene");
                }
            }
        }
    }
}