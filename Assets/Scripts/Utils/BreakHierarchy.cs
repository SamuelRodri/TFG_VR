using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Utils
{
    // Set a parent object breking the hierarchy
    public class BreakHierarchy : MonoBehaviour
    {
        public void SetParent()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.AddComponent<JointComponent3>();
                transform.GetChild(i).gameObject.GetComponent<JointComponent3>().prevObject = 
                    transform.GetChild(i).gameObject.GetComponent<JointComponent2>().prevObject;
                transform.GetChild(i).gameObject.GetComponent<JointComponent3>().nextObject =
                    transform.GetChild(i).gameObject.GetComponent<JointComponent2>().nextObject;
            }

            
        }
    }
}