using TFG.Behaviour;
using UnityEngine;

namespace TFG.Behaviour
{
    public class SimulationController : MonoBehaviour
    {
        public delegate void BreakJoints();
        public static event BreakJoints OnBreak;

        public delegate void RestoreJoints();
        public static event RestoreJoints OnRestore;

        public bool areJointsActivated = true;

        public void BreakJointsPress()
        {
            if (areJointsActivated)
            {
                areJointsActivated = false;
                OnBreak();
            }
        }

        public void RestoreJointsPress()
        {
            areJointsActivated = true;
            OnRestore();
        }

        public void RestoreColumn()
        {
            var jointComponents = Object.FindObjectsOfType<JointComponent>();

            foreach(var component in jointComponents)
            {
                component.ResetTransform();

                if (component.GetComponent<CartilaginousJoint>())
                {
                    component.GetComponent<CartilaginousJoint>().RestoreLinks();
                }

                RestoreJointsPress();
            }
        }
    }
}