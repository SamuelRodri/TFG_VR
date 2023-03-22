using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    // Class representing a cartilaginous ligament of the spine
    public class CartilaginousJoint : MonoBehaviour
    {
        [Header("Previous Vertebra Settings")]
        [SerializeField] GameObject prevVert;
        [SerializeField] float linearLimitPrev;

        [Header("Next Vertebra Settings")]
        [SerializeField] GameObject nextVert;
        [SerializeField] float linearLimitNext;

        // ConfigurableJoints
        private ConfigurableJoint prevJoint;
        private ConfigurableJoint nextJoint;

        private JointComponent jointComponent;

        private void Awake()
        {
            // Initialization of configurable joints
            prevJoint = GetComponents<ConfigurableJoint>()[0];
            nextJoint = GetComponents<ConfigurableJoint>()[1];

            // Initialization of JointComponent
            jointComponent = GetComponent<JointComponent>();
        }

        private void Start()
        {
            // Configurable joints connectedBodies
            prevJoint.connectedBody = prevVert.GetComponent<Rigidbody>();
            nextJoint.connectedBody = nextVert.GetComponent<Rigidbody>();

            // Configurable joints linear limits
            SoftJointLimit auxiliarLimit = new();

            auxiliarLimit.limit = linearLimitPrev;
            prevJoint.linearLimit = auxiliarLimit;

            auxiliarLimit.limit = linearLimitNext;
            nextJoint.linearLimit = auxiliarLimit;

            // Initialize joints component links
            prevVert.GetComponent<JointComponent>().SetNext(jointComponent);
            nextVert.GetComponent<JointComponent>().SetPrev(jointComponent);

            jointComponent.SetPrev(prevVert.GetComponent<JointComponent>());
            jointComponent.SetNext(nextVert.GetComponent<JointComponent>());
        }
    }
}