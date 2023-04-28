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
                //transform.GetChild(i).gameObject.AddComponent<JointComponentGraph>();
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<JointComponentGraph>().prevObject =
                    transform.GetChild(i).gameObject.GetComponent<JointComponent3>().prevObject.GetComponent<JointComponentGraph>();
                transform.GetChild(i).gameObject.GetComponent<JointComponentGraph>().nextObject =
                    transform.GetChild(i).gameObject.GetComponent<JointComponent3>().nextObject.GetComponent<JointComponentGraph>();
            }


        }
    }
}