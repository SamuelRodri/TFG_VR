using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TFG.Utils
{
    public class AddJointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AddJoint myScript = (AddJoint)target;

            if (GUILayout.Button("Execute"))
            {
                myScript.AddJointComponent();
            }
        }
    }
}