using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    public class FibrousJoint : MonoBehaviour
    {
        // ConfigurableJoints
        private ConfigurableJoint prevJoint;
        private ConfigurableJoint nextJoint;

        public SimulationController controller;

        // Start is called before the first frame update
        void Start()
        {
            // Initialization of configurable joints
            prevJoint = GetComponentsInChildren<ConfigurableJoint>()[0];
            nextJoint = GetComponentsInChildren<ConfigurableJoint>()[1];

            SimulationController.OnBreak += BreakLinks;
            SimulationController.OnRestore += RestoreLinks;
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