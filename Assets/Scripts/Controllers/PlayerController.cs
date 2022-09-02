using System;
using StackBall.Controllers;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

namespace StackBall
{
    public class PlayerController : MonoBehaviour
    {
        private float _speed = 7f;
        [SerializeField] private Vector3 _force = new Vector3(0f, -100f, 0f);
       
        private Rigidbody _rigidBody;
        public bool _isOnInput;
        [FormerlySerializedAs("slashEffect")] public GameObject splashEffect;

        public ParticleSystem bomb;
       
        
        
        public bool isBoost;
       public bool isUnbreakable;
       public int failCrash;

        void Start()
        {
            
           
            _rigidBody = GetComponent<Rigidbody>();
            GameManager.instance.virtualCamera.Follow = transform;
            GameManager.instance.virtualCamera.LookAt = transform;
           

        }

        // Update is called once per frame
        void Update()
        {
            _isOnInput = Input.GetButton("Fire1");

            if (failCrash >= 3)
            {
               bomb.Play();
               Destroy(gameObject, 1f);
              GameManager.instance.OnGameLose();
              GameManager.instance.AddConclusion("GAME OVER");
            }
        }

        public void FixedUpdate()
        {
            if (_isOnInput && !isUnbreakable)
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

        public void OnCollisionEnter(Collision other)
        {
            
            GameObject splash=  Instantiate(splashEffect, transform.position,Quaternion.Euler(new Vector3(90f,0f,0f)));
            splash.transform.position = other.contacts[0].point + Vector3.up * .1f;
            splash.transform.SetParent(other.gameObject.transform);
            if (_isOnInput)
            {
                
                if (other.collider.gameObject.CompareTag("Broken"))
                {
                    Debug.Log("burada");
                    failCrash = 0;
                    other.transform.GetComponent<GridController>().Breaking();
                    
                 GameManager.instance.AddScore(10);
                 return;
                 
                }
                
            }
            if (other.collider.gameObject.CompareTag("UnBreakable"))
            {
                if(_isOnInput) failCrash++;
                _isOnInput = false;
                isUnbreakable = true;
              
            }

            if (other.collider.gameObject.CompareTag("Finish"))
            {
                 GameManager.instance.OnGameWin();
                GameManager.instance.AddConclusion("WİN");
            }
            
            
                    _rigidBody.velocity = CalcForce() * -1f;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.gameObject.CompareTag("UnBreakable"))
            {
                isUnbreakable = false;
               
            }
        }

        public Vector3 CalcForce() => _force * _speed * Time.deltaTime;
    }
}