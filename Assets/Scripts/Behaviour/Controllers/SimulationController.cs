using TFG.Behaviour;
using TFG.Behaviour.Column;
using UnityEngine;

namespace TFG.Behaviour.Controllers
{
    // General controller that executes the events
    public class SimulationController : MonoBehaviour
    {
        public delegate void BreakJoints();
        public static event BreakJoints OnBreak;

        public delegate void RestoreJoints();
        public static event RestoreJoints OnRestore;

        public static bool areJointsActivated = true;

        public void BreakJointsPress()
        {
            if (areJointsActivated)
            {
                areJointsActivated = false;
                OnBreak();
            }
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

                areJointsActivated = true;
                OnRestore();
            }
        }
    }
}