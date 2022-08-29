using StackBall.Controllers;
using Unity.VisualScripting;
using UnityEngine;

namespace StackBall
{
    public class PlayerController : MonoBehaviour
    {
        private float _speed = 7f;

        [SerializeField] private Vector3 _force = new Vector3(0f, -100f, 0f);

        private Rigidbody _rigidBody;
        private bool _isOnInput;

        public ParticleSystem bomb;
        public GameObject panel;

        private GridController gridCont;

        public bool isBoost;
        public Collider coll;

        void Start()
        {
            gridCont = GetComponent<GridController>();
            _rigidBody = GetComponent<Rigidbody>();
            GameManager.instance.virtualCamera.Follow = transform;
            GameManager.instance.virtualCamera.LookAt = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isOnInput = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isOnInput = false;
            }
        }

        public void FixedUpdate()
        {
            if (_isOnInput)
            {
                if (!isBoost)
                    _rigidBody.velocity = CalcForce();
                else
                {
                    transform.position += CalcForce() * .1f;
                }
            }
        }

        public void PhysicOn(bool isOn)
        {
            if (isOn)
            {
                _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                _rigidBody.constraints = (RigidbodyConstraints)122;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_isOnInput)
            {
                if (collision.gameObject.TryGetComponent<GridController>(out var grid))
                {
                    if (grid.Breaking())
                    {
                        isBoost = true;

                        GameManager.instance.AddScore(1);
                    }
                    else
                    {
                        isBoost = false;
                        coll.isTrigger = false;
                    }

                    PhysicOn(isBoost);
                }

                return;
            }


            _rigidBody.velocity = CalcForce() * -1f;
        }

        public Vector3 CalcForce() => _force * _speed * Time.deltaTime;


        public void OnCollisionExit(Collision collision)
        {
            if (_isOnInput)
            {
                if (collision.gameObject.TryGetComponent<GridController>(out var grid))
                {
                }
            }
        }
    }
}