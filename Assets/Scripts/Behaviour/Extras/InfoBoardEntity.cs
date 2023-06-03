using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TFG.Behaviour.Extras
{
    public class InfoBoardEntity : MonoBehaviour
    {
        [SerializeField] GameObject board;
        [SerializeField] TMP_Text information;

        // Start is called before the first frame update
        void Start()
        {
            //board = transform.GetChild(0).gameObject;
            //information = board.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            //information.text = name;
        }

        public void SetBoardActive()
        {
            board.SetActive(true);
        }

        public void SetBoardInactive()
        {
            board.SetActive(false);
        }
    }
}