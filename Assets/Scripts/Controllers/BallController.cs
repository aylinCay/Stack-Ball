using System;

namespace StackBall.Controllers
{
    using UnityEngine;

    public class BallController : MonoBehaviour
    {
        public float force;

        public Rigidbody rigidBody;


        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
        }


        private void OnCollisionEnter(Collision collision)
        {
            rigidBody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}