using System;
using Unity.Mathematics;

namespace StackBall
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelCreator : MonoBehaviour
    {
        public float rotateSpeed = 1f;
        public GameObject linePrefab; //we defined the object
        public float lineOfsetY = .6f; //space between objects
        public bool isInital; //check start
        public int lineCount = 50; //how many will we create
        public Vector3 lastpos = Vector3.zero;
        
        public void CreatorLevel()
        {
            var rot = 0f;
            if (isInital) 
                return;
            if(linePrefab == null) // obje is not null
                return;
            for (int i = 0; i <= lineCount; i++)
            {

                rot += 72; //we set increasing
                var pos = lastpos - (Vector3.up * lineOfsetY); //top-down position adjustment
                var lineRot =linePrefab.transform.rotation= quaternion.Euler(Vector3.up * rot);//we set the rotation
                var l= Instantiate(linePrefab, pos,lineRot); //we created the objects

                
               l.transform.SetParent(transform);
                lastpos = pos;
            }

            isInital = true;

        }

        public void OnEnable()
        {
            CreatorLevel();
        }

        public void Update()
        {
          transform.Rotate(Vector3.up* rotateSpeed*Time.deltaTime);
        }

        
    }

    
}