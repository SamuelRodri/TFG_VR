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

        public delegate void MountMode();
        public static event MountMode OnMountMode;

        public delegate void NormalMode();
        public static event NormalMode OnNormalMode;

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
            JointComponent[] jointComponents = FindObjectsOfType<JointComponent>();

            foreach(JointComponent component in jointComponents)
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

        public void MountColumnMode()
        {
            BreakJointsPress();
            OnMountMode();
        }

        public void BackToNormalMode()
        {
            RestoreColumn();
            OnNormalMode();
        }
    }
}