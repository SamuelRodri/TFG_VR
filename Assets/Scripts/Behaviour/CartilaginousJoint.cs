using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    // Class representing a cartilaginous ligament of the spine
    public class CartilaginousJoint : MonoBehaviour
    {
        private ConfigurableJoint[] joints; // Previous and Next joints
        private JointComponent jc;
        private JointComponent prev;
        private JointComponent next;

        [SerializeField] float linearLimit;

        private void Awake()
        {
            joints = GetComponents<ConfigurableJoint>();
            jc = GetComponent<JointComponent>();
            prev = joints[0].connectedBody.GetComponent<JointComponent>();
            next = joints[1].connectedBody.GetComponent<JointComponent>();

            // Configurable joints values
            SoftJointLimit limit = new();
            limit.limit = linearLimit;
            joints[0].linearLimit = limit;
        }

        private void Start()
        {
            // Initialize joints connections
            prev.SetNext(GetComponent<JointComponent>());
            jc.SetPrev(prev);
            next.SetPrev(GetComponent<JointComponent>());
            jc.SetNext(next);
        }
    }
}