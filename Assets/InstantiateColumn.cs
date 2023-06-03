using System;
using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Column;
using TFG.Behaviour.Controllers;
using UnityEngine;

public class InstantiateColumn : MonoBehaviour
{
    [SerializeField] GameObject originalLigament;
    [SerializeField] GameObject originalColumn;

    // Start is called before the first frame update
    void Start()
    {
        SimulationController.OnBreak += DuplicateColumn;
    }

    private void DuplicateColumn()
    {
        GameObject duplicatedColumn = Instantiate(originalColumn, originalColumn.transform);
        duplicatedColumn.transform.parent = transform;

        GameObject duplicatedSkeleton = duplicatedColumn.transform.GetChild(0).gameObject;
        GameObject duplicatedLigaments = duplicatedColumn.transform.GetChild(1).gameObject;
        
        var skeletonRenderers = duplicatedSkeleton.GetComponentsInChildren<Renderer>();

        foreach (var renderer in skeletonRenderers)
        {
            if (renderer.GetComponent<JointComponent>())
            {
                renderer.GetComponent<JointComponent>().areFlexible = true;
            }
            renderer.GetComponent<Renderer>().enabled = false;
        }

        var ligamentsRenderers = duplicatedLigaments.GetComponentsInChildren<Renderer>();

        foreach(var renderer in ligamentsRenderers)
        {
            if(renderer.gameObject.name != originalLigament.name)
            {
                renderer.GetComponent<JointComponent>().areFlexible = true;
                renderer.GetComponent<Renderer>().enabled = false;
            }

            if (renderer.gameObject.GetComponent<CartilaginousJoint>())
            {
                renderer.gameObject.GetComponent<CartilaginousJoint>().RestoreLinks();
            }
        }
    }
}