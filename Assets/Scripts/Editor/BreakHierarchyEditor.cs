using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TFG.Utils
{
    [CustomEditor(typeof(BreakHierarchy))]
    [CanEditMultipleObjects]
    public class BreakHierarchyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BreakHierarchy myScript = (BreakHierarchy)target;

            if(GUILayout.Button("Execute"))
            {
                myScript.SetParent();
            }
        }
    }
}