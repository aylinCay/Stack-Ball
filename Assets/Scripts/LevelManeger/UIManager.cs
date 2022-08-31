using System;
using TMPro;
using UnityEngine;

namespace StackBall
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager GlobalAccess { get; set; }

        [field: SerializeField] private GameObject gamePanel;

        [SerializeField] private TextMeshProUGUI _scoreText;

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
    }
}