using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.DebugManagement {

    public class ErrorDisplay : MonoBehaviour
    {
        public Text errorText;

        void Start()
        {
            Application.logMessageReceived += HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                errorText.text += logString + "\n";
            }
        }
    }
}
