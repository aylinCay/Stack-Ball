using System;
using UnityEngine;
using UnityEngine.Events;

namespace StackBall
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [field: SerializeField] public float gameScore { get; private set; }

        [field: SerializeField] public UnityEvent onGameStartEvent { get; private set; }

        [field: SerializeField] public UnityEvent onGameExitEvent { get; private set; }

        [field: SerializeField] public UnityEvent onGameLoseEvent { get; private set; }

        [field: SerializeField] public UnityEvent onGameWinEvent { get; private set; }

        public PlayerController player { get; private set; }

        [field: SerializeField] private GameObject playerPrefab { get; set; }
        [field: SerializeField] private GameObject playerPivot { get; set; }

        private void Awake()
        {
            CreateInstance();
        }


        public void AddScore(float Value)
        {
            gameScore += Value;
            UIManager.GlobalAccess.scoreText = gameScore.ToString();
        }

        public void SetScore(float Value)
        {
            gameScore = Value;
            UIManager.GlobalAccess.scoreText = gameScore.ToString();
        }

        public void OnGameStart()
        {
            onGameStartEvent.Invoke();
        }

        public void OnGameExit()
        {
            onGameExitEvent.Invoke();
        }

        public void OnGameLose()
        {
            onGameLoseEvent.Invoke();
        }

        public void OnGameWin()
        {
            onGameWinEvent.Invoke();
        }

        public void CreatePlayer()
        {
            var pivot = playerPivot ?? gameObject;

            var playerInstance = Instantiate(playerPrefab, pivot.transform.position, playerPivot.transform.rotation);
            player = playerInstance.GetComponent<PlayerController>();
        }

        private void CreateInstance()
        {
            if (GameManager.instance != null)
            {
                if (GameManager.instance.gameObject != null)
                {
                    if (GameManager.instance.gameObject != this.gameObject)
                    {
                        Destroy(this.gameObject);
                        return;
                    }
                }
            }

            instance = this;
        }
    }
}