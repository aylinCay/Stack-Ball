using StackBall.Controllers;

namespace StackBall
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Mathematics;
    using UnityEngine;
    
    public class PlayerController : MonoBehaviour
    {
        
        private Rigidbody rigidBody;
        private float speed = 7f;
        private bool impact;
       
        private GridController gridCont;
        
        void Start()
        {
            gridCont = GetComponent<GridController>();
            rigidBody = GetComponent<Rigidbody>();
           
        }
    
        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                impact =true;
            }
    
            if(Input.GetMouseButtonUp(0))
            {
                impact = false;

            }
           
        }
    
        public void FixedUpdate()
        {
            if(impact)
            {
                rigidBody.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * speed, 0);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (!impact)
            {
                rigidBody.velocity = new Vector3(0, 50 * Time.deltaTime * speed, 0);
            }
           
           
        }

        public void OnCollisionStay(Collision collisionInfo)
        { 
            if(impact)
            {
                if (collisionInfo.gameObject.tag == "Broken")
                {
                    collisionInfo.gameObject.GetComponent<GridController>().Breaking();
                } 
                Debug.Log("Ateşlendi");
                    
            } 
            
        }
    }
}
