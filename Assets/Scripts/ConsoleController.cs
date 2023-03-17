using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] GameObject console;

    public InputActionReference toggleReference = null;

    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        //bool isActive = !console.activeSelf; 
        bool isActive = false;
        console.SetActive(isActive);
    }

    private void OnCollisionExit(Collision collision)
    {
        DebugDisplay.message = collision.gameObject.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DebugDisplay.message = collision.gameObject.ToString();
    }
}
