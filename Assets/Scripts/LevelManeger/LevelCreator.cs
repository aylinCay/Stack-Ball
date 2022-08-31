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
        public float lineOfsetY = .8f; //space between objects
        public bool isInital; //check start
        public int lineCount = 50; //how many will we create
        public Vector3 lastpos = Vector3.zero;
        public Material brokenMetarial;
        public Material unBrokenMetarial;
        public void CreatorLevel()
        {
            var rot = 0f;
            if (isInital) 
                return;
            if(linePrefab == null) // obje is not null
                return;
            for (int i = 0; i <= lineCount; i++)
            {

                rot += 3; //we set increasing
                var pos = lastpos - (Vector3.up * lineOfsetY); //top-down position adjustment
                var lineRot =linePrefab.transform.rotation= quaternion.Euler(Vector3.up * rot);//we set the rotation
                var l= Instantiate(linePrefab, pos,lineRot); //we created the objects
                var gridCount = l.transform.childCount;
                for (int j = gridCount - 1; j >= 0; j--)
                {
                   var grid= l.transform.GetChild(j).gameObject;
                   grid.tag = "Broken";
                   grid.GetComponent<Renderer>().sharedMaterial = brokenMetarial;
                }

                if (Random.Range(0, 100) < 15)
                {
                    int randChid = Random.Range(0, gridCount);
                    for (int j = gridCount - 1; j >= 0; j--)
                    {
                        if (j == randChid)
                        {
                            continue;
                        }
                        var grid= l.transform.GetChild(j).gameObject;
                        grid.tag = "UnBreakable";
                        grid.GetComponent<Renderer>().sharedMaterial = unBrokenMetarial;
                    }
                }
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