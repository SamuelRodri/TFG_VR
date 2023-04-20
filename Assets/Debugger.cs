using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] SimulationController controller;
    [SerializeField] BodyVisibilityController visibilityController;

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            visibilityController.ToggleSkeletonVisibility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            visibilityController.ToggleLigamentsVisibility();
        }
    }
}