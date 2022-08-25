using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StackBall.Controllers
{
    public class LineController : MonoBehaviour
    {
        public float force;
        public UnityEvent explosionEvent = new UnityEvent();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                explosionEvent.Invoke();
        }
    }
}