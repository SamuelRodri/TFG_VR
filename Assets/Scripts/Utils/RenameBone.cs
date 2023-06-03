using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Utils
{
    public class RenameBone : MonoBehaviour
    {
        public void Rename()
        {
            var child = transform.GetChild(0);

            Debug.Log(child.transform.childCount);
            for(int i = 0; i < child.childCount; i++)
            {
                var grandson = child.GetChild(i);
                grandson.name = name + " - " + " Bone." + string.Format("{0:000}", i);
            }
        }
    }
}