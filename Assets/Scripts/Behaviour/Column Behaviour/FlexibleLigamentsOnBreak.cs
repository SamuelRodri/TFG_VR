using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    public class FlexibleLigamentsOnBreak : MonoBehaviour
    {
        [SerializeField] GameObject objectToReplace;

        private void Start()
        {
            SimulationController.OnBreak += Show;
            SimulationController.OnRestore += Hide;

            Hide();
        }

        public void Show()
        {
            transform.position = objectToReplace.transform.position;
            transform.rotation = objectToReplace.transform.rotation;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}