using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TFG.Utils
{
    [CustomEditor(typeof(RenameBone))]
    [CanEditMultipleObjects]
    public class RenameBoneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RenameBone myScript = (RenameBone)target;

            if (GUILayout.Button("Execute"))
            {
                myScript.Rename();
            }
        }
    }
}
