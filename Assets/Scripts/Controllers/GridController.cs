using System;

namespace StackBall.Controllers
{
    using UnityEngine;

    public class GridController : MonoBehaviour
    {
        public LineController line;
        public Rigidbody rigidBody;

        private void Start()
        {
            line = GetComponentInParent<LineController>();
            line.explosionEvent.AddListener(this.Explosion);
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Breaking()
        {
            line.explosionEvent.Invoke();
        }

        public void Explosion()
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.useGravity = true;

            var forceRand = Random.Range(0, line.force);

            var explosionDirection = Vector3.right;

            if (transform.position.x == 0f)
            {
                var randX = Random.Range(0, 10);
                explosionDirection = randX < 5 ? Vector3.right : Vector3.left;
            }
            else
            {
                explosionDirection = transform.position.x < 0f ? Vector3.left : Vector3.right;
            }

            var forceDir = Vector3.one;
            forceDir.y = (line.force + forceRand) * 2f;
            forceDir.x = explosionDirection.x * ((line.force + forceRand) * .5f);
            forceDir.z = (line.force + forceRand) * -1f;

            rigidBody.AddForce(forceDir, ForceMode.Impulse);
        }
    }
}