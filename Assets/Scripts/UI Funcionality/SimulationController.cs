using TFG.Behaviour;
using UnityEngine;

namespace TFG.UI
{
    public class SimulationController : MonoBehaviour
    {
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

                GetComponent<JointsController>().areJointsActivated = true;
            }
        }
    }
}