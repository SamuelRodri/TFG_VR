using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour
{
    public class BoneJoint : MonoBehaviour
    {
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}