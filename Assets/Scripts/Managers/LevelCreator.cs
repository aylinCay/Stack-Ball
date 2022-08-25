using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace StackBall.Mangers
{
    public class LevelCreator : MonoBehaviour
    {
        public float rotateSpeed;
        public GameObject linePrefab;


        public float lineOffsetY = .6f;
        public int lineCount = 50;
        public bool isInitial;
        public Vector3 lastPos = Vector3.zero;

        public void CreateLevel()
        {
            if (isInitial)
                return;

            if (linePrefab == null)
                return;

            for (int i = 0; i < lineCount; i++)
            {
                var pos = lastPos - (Vector3.up * .6f);
                var l = Instantiate(linePrefab, pos, quaternion.identity);

                var rot = Random.Range(0, 10);
                l.transform.rotation = quaternion.Euler(Vector3.up * (rot * 36f));
                l.transform.SetParent(transform);

                lastPos = pos;
            }

            isInitial = true;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }


        private void OnEnable()
        {
            CreateLevel();
        }
    }
}