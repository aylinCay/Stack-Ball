using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
namespace StackBall
{
    public class UIManager : MonoBehaviour
    {
        
        public static UIManager GlobalAccess { get; set; }

        [field: SerializeField] private GameObject gamePanel;
        [field: SerializeField] private GameObject resultPanel;

        [SerializeField] private TextMeshProUGUI _scoreText;

       

        public void Start()
        {
            
        }

        public void OnGamePanel(bool isOn)
        {
            gamePanel.SetActive(isOn);
        }

        public string scoreText
        {
            get => _scoreText.text;
            set => _scoreText.text = "Score" + value;
        }

      

        private void Awake()
        {
            GlobalAccess = this;

        }

        public void OnResultPanel(bool isOn)
        {
            
            resultPanel.SetActive(isOn);

        }

       

        public void OnReplay()
        {
            SceneManager.LoadScene(0);
            
        }

      
    }
}