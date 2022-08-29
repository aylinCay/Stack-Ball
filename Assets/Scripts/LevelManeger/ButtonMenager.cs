using System;
using UnityEngine.UI;

namespace StackBall
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class ButtonMenager : MonoBehaviour
    {
        public GameObject player;

        public Button play;
        public bool isplay;
        public float time;
        // Start is called before the first frame update
        void Start()
        {
            time = 0;
            isplay = false;

        }

        // Update is called once per frame
        public void PlayButton()
        {
            isplay = true;
            Instantiate(player);
           play.gameObject.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Update()
        {
            if (isplay == true)
            {
                time++;
            }
        }
    }

}