using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG.UI
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

        public void ToggleSkeletonVisibility()
        {
            Debug.Log("ESQUELETO");
            skeleton.SetActive(!skeleton.activeInHierarchy);
        }

        public void ToggleLigamentsVisibility()
        {
            ligaments.SetActive(!ligaments.activeInHierarchy);
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