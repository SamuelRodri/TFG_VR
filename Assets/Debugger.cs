using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] SimulationController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.BreakJointsPress();
        }
    }
}
