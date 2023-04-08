using TFG.Behaviour;
using UnityEngine;

namespace TFG.Behaviour
{
    public class SimulationController : MonoBehaviour
    {
        public delegate void BreakJoints();
        public event BreakJoints OnBreak;

        public delegate void RestoreJoints();
        public event RestoreJoints OnRestore;

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
                component.Reset();

                if (component.GetComponent<CartilaginousJoint>())
                {
                    Debug.Log("Restaura");
                    component.GetComponent<CartilaginousJoint>().RestoreLinks();
                }

                areJointsActivated = true;
            }
        }
    }
}