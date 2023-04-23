using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointComponent2 : MonoBehaviour
{
    public GameObject prevObject;
    private Rigidbody rb;
    private Vector3 linearoffset;
    private Quaternion rotationalOffset;
    private Quaternion rotationInicial;

    public bool isGrabbed = false;
    public float umbral = 0.006f;

    private Vector3 antPrev;
    Vector3 posicionRelativa;
    Vector3 rotacionRelativa;

    Matrix4x4 translationMatrix;
    Matrix4x4 rotationMatrix;

    bool isDone = false;

    private Vector3 relativePosition;
    private Quaternion relativeRotation;
    private Vector3 relativeScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        linearoffset = transform.position - prevObject.transform.position;
        rotationalOffset = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
        Debug.Log(rotationalOffset.eulerAngles);
        translationMatrix = Matrix4x4.Translate(linearoffset);
        rotationMatrix = Matrix4x4.Rotate(rotationalOffset);

        relativePosition = prevObject.transform.InverseTransformPoint(transform.position);
        relativeRotation = Quaternion.Inverse(prevObject.transform.rotation) * transform.rotation;
        //relativeScale = Vector3.Scale(transform.localScale, Vector3.one / prevObject.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDone) return;
        //Matrix4x4 a1 = prevObject.transform.localToWorldMatrix;
        //a1 = Matrix4x4.TRS(a1.GetColumn(3), Quaternion.LookRotation(a1.GetColumn(2), a1.GetColumn(1)), Vector3.one);
        //Debug.Log($"a1: {a1.ToString("f2")}");

        //Matrix4x4 m0 = transform.localToWorldMatrix;
        //m0 = Matrix4x4.TRS(m0.GetColumn(3), Quaternion.LookRotation(m0.GetColumn(2), m0.GetColumn(1)), Vector3.one);
        //Debug.Log($"m0: {m0.ToString("f2")}");

        //Matrix4x4 t = a1 * Matrix4x4.Inverse(m0);
        ////Debug.Log($"t: {t.ToString("f1")}");
        //Matrix4x4 b = t * m0;

        //Matrix4x4 final = b * translationMatrix;
        //transform.position = final.GetColumn(3);
        //final *= rotationMatrix;
        ////transform.rotation = Quaternion.LookRotation(final.GetColumn(2), final.GetColumn(1));
        //transform.position = prevObject.transform.position + linearoffset;
        //var rotacionPadreGlobal = prevObject.transform.rotation;
        //var rotacionLocal = Quaternion.Inverse(prevObject.transform.rotation) * transform.localRotation;
        //transform.rotation = rotacionPadreGlobal * rotacionLocal;
        isDone = true;

        //Matrix4x4 a1 = prevObject.transform.localToWorldMatrix;
        //a1 = Matrix4x4.TRS(a1.GetColumn(3), Quaternion.LookRotation(a1.GetColumn(2), a1.GetColumn(1)), Vector3.one);

        //Matrix4x4 m0 = transform.localToWorldMatrix;
        //m0 = Matrix4x4.TRS(m0.GetColumn(3), Quaternion.LookRotation(m0.GetColumn(2), m0.GetColumn(1)), Vector3.one);

        //Matrix4x4 transformMatrix = translationMatrix * Matrix4x4.Inverse(rotationMatrix);

        //Matrix4x4 final = a1 * translationMatrix * rotationMatrix;
        //transform.position = final.GetColumn(3);
        //transform.rotation = Quaternion.LookRotation(final.GetColumn(2), final.GetColumn(1));

        if(Vector3.Distance(prevObject.transform.TransformPoint(relativePosition), transform.position) > 0.002f)
        {
            Vector3 target = prevObject.transform.TransformPoint(relativePosition);
            Vector3 newPosition = Vector3.Lerp(transform.position, target, 0.57f);

            transform.position = newPosition;
        }

        if(Quaternion.Angle(prevObject.transform.rotation * relativeRotation, transform.rotation) > 2.5f)
        {
            Quaternion target = prevObject.transform.rotation * relativeRotation;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, target, 0.45f);

            transform.rotation = newRotation;
        }
    }
}