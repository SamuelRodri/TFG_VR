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
            if (GetComponentsInChildren<ConfigurableJoint>().Length > 0)
            {
                prevJoint = GetComponentsInChildren<ConfigurableJoint>()[0];
            }

            if (GetComponentsInChildren<ConfigurableJoint>().Length > 1)
            {
                nextJoint = GetComponentsInChildren<ConfigurableJoint>()[1];
            }

            // Initialization of JointComponent
            jointComponent = GetComponent<JointComponent>();

            controller = GameObject.Find("SimulationController").GetComponent<SimulationController>();
        }

        private void Start()
        {
            InitializeJoints();

            SimulationController.OnBreak += BreakLinks;
            SimulationController.OnRestore += RestoreLinks;
        }

        private void InitializeJoints()
        {
            // Configurable joints linear limits
            SoftJointLimit auxiliarLimit = new();

            // Configurable joints connectedBodies
            if (prevJoint)
            {
                prevJoint.connectedBody = prevVert.GetComponent<Rigidbody>();
                auxiliarLimit.limit = linearLimitPrev;
                prevJoint.linearLimit = auxiliarLimit;
                
                // Initialize joints component links
                prevVert.GetComponent<JointComponent>().SetNext(jointComponent);
                jointComponent.SetPrev(prevVert.GetComponent<JointComponent>());
            }

            if (nextJoint)
            {
                nextJoint.connectedBody = nextVert.GetComponent<Rigidbody>();
                auxiliarLimit.limit = linearLimitNext;
                nextJoint.linearLimit = auxiliarLimit;

                nextVert.GetComponent<JointComponent>().SetPrev(jointComponent);
                jointComponent.SetNext(nextVert.GetComponent<JointComponent>());
            }
        }

        public void RestoreLinks()
        {
            if (prevJoint)
            {
                prevJoint.xMotion = ConfigurableJointMotion.Locked;
                prevJoint.yMotion = ConfigurableJointMotion.Limited;
                prevJoint.zMotion = ConfigurableJointMotion.Locked;
            }

            if (nextJoint)
            {
                nextJoint.xMotion = ConfigurableJointMotion.Locked;
                nextJoint.yMotion = ConfigurableJointMotion.Limited;
                nextJoint.zMotion = ConfigurableJointMotion.Locked;
            }
        }

        public void BreakLinks()
        {
            if (prevJoint)
            {
                prevJoint.xMotion = ConfigurableJointMotion.Free;
                prevJoint.yMotion = ConfigurableJointMotion.Free;
                prevJoint.zMotion = ConfigurableJointMotion.Free;
            }

            if (nextJoint)
            {
                nextJoint.xMotion = ConfigurableJointMotion.Free;
                nextJoint.yMotion = ConfigurableJointMotion.Free;
                nextJoint.zMotion = ConfigurableJointMotion.Free;
            }
        }
    }
}