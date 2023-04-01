using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    public enum Vectors
    {
        right, up, forward, real
    };

    GameObject ver1;
    GameObject ver2;

    [SerializeField] GameObject console;
    [SerializeField] Text text;

    [Range(0, 2)]
    [SerializeField] static int i = 0;
    [SerializeField] public Vectors vector;

    public InputActionReference toggleReference = null;

    // Start is called before the first frame update
    void Start()
    {
        vector = (Vectors)3;
    }
    private void Awake()
    {
        toggleReference.action.started += ChangeVector;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= ChangeVector;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Controlando el vector {vector}");
    }

    private void ChangeVector(InputAction.CallbackContext context)
    {
        Debug.Log("Cambia");
        i = (i + 1) % 4;
        vector = (Vectors)i;
    }

    public void AddVertebra(GameObject ver)
    {
        if (ver1)
        {
            ver2 = ver;
            return;
        }

        ver1 = ver;
    }
}
