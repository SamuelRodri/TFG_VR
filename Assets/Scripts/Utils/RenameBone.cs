using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Utils
{
    public class RenameBone : MonoBehaviour
    {
        public GameObject parent;

        public void Rename()
        {
            name = parent.name + " - " + name;
        }
    }
}