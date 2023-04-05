using System.Collections;
using System.Collections.Generic;
using TFG.UI;
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

        public SimulationController controller;

        private void Awake()
        {
            // Initialization of configurable joints
            prevJoint = GetComponentsInChildren<ConfigurableJoint>()[0];
            nextJoint = GetComponentsInChildren<ConfigurableJoint>()[1];
            
            // Initialization of JointComponent
            jointComponent = GetComponent<JointComponent>();

            controller = GameObject.Find("SimulationController").GetComponent<SimulationController>();
        }

        private void Start()
        {
            InitializeJoints();

            controller.OnBreak += BreakLinks;
            controller.OnRestore += RestoreLinks;
        }

        private void InitializeJoints()
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

        public void RestoreLinks()
        {
            prevJoint.xMotion = ConfigurableJointMotion.Locked;
            prevJoint.yMotion = ConfigurableJointMotion.Limited;
            prevJoint.zMotion = ConfigurableJointMotion.Locked;

            nextJoint.xMotion = ConfigurableJointMotion.Locked;
            nextJoint.yMotion = ConfigurableJointMotion.Limited;
            nextJoint.zMotion = ConfigurableJointMotion.Locked;
        }

        public void BreakLinks()
        {
            prevJoint.xMotion = ConfigurableJointMotion.Free;
            prevJoint.yMotion = ConfigurableJointMotion.Free;
            prevJoint.zMotion = ConfigurableJointMotion.Free;

            nextJoint.xMotion = ConfigurableJointMotion.Free;
            nextJoint.yMotion = ConfigurableJointMotion.Free;
            nextJoint.zMotion = ConfigurableJointMotion.Free;
        }
    }
}