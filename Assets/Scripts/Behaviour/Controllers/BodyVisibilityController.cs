using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.Behaviour.Controllers
{
    public enum BodyLayer
    {
        skeleton, ligaments, nervs, cardio
    }

    // Class to control the visibility of the different layers of the body
    public class BodyVisibilityController : MonoBehaviour
    {
        [SerializeField] GameObject skeleton;
        [SerializeField] GameObject ligaments;
        [SerializeField] GameObject nervs;
        [SerializeField] GameObject cardio;

        public delegate void Toggle();

        public static event Toggle ToggleSkeleton;
        public static event Toggle ToggleLigaments;

        public void ToggleSkeletonVisibility()
        {
            ToggleSkeleton();
        }

        public void ToggleLigamentsVisibility()
        {
            ToggleLigaments();
        }

        public void ToggleNervsVisibility()
        {
            nervs.SetActive(!nervs.activeInHierarchy);
        }

        public void ToggleCardioVisibility()
        {
            cardio.SetActive(!cardio.activeInHierarchy);
        }
    }
}