using System;

namespace StackBall.Controllers
{
    using UnityEngine;

    public class GridController : MonoBehaviour
    {
        public LineController line;

        Rigidbody rigidBody;
        private Collider coll;


        private void Start()
        {
            line = GetComponentInParent<LineController>();
            line.explosionEvent.AddListener(Explosion);
            rigidBody = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();
        }

        public void Breaking()
        {
            Debug.Log("explosion");
            CameraController.Instance.CameraShake(5f,0.1f);
            Handheld.Vibrate();
            line.explosionEvent.Invoke(); 
        }

        public void Explosion()
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.useGravity = true;
            coll.isTrigger = true;

            var forceRand = Random.Range(0, line.force);

            var explosionDirection = Vector3.right;

         
                var randX = Random.Range(-6, 6);
                explosionDirection = Vector3.right * randX; 
           

            var forceDir = Vector3.zero;
            forceDir.y = (line.force + forceRand) * 2f;
            forceDir.x = explosionDirection.x * ((line.force + forceRand) * .5f);
            forceDir.z = (line.force + forceRand) * -1f;
            transform.SetParent(null);
            rigidBody.AddForce(forceDir, ForceMode.Impulse);
        }
    }
}