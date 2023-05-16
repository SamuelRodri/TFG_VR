using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Hand;
using UnityEngine;

public class test : MonoBehaviour
{
    public Animator animator;
    public bool grabbed;
    private int value;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener referencia al componente Animator del padre
        Animator parentAnimator = GetComponentInParent<Animator>();

        // Establecer el peso de la capa a cero para detener la animación
        
        if (Input.GetKeyDown(KeyCode.F) && !grabbed)
        {
            grabbed = true;
            //GetComponent<HandAnimations>().SetAnimFloats(1f, 1f);

        }

        if (grabbed)
        {

            if (animator.GetFloat("Grip") >= 0.99)
            {
                grabbed = false;
            }
        }
    }
}