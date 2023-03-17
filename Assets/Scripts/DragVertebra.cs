using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragVertebra : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.01f; 
    }
    private void OnMouseDown()
    {
        GetComponent<Rigidbody>().freezeRotation = true;

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody>().freezeRotation = false;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}