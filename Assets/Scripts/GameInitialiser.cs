using UnityEngine;
using UnityEngine.UI;
using System;
using MurkaTestTask.Enums;
using MurkaTestTask.View;
using UnityEngine.SceneManagement;

namespace MurkaTestTask.Logic
{
    public class GameInitialiser : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image pressStartImage = null;
        [SerializeField] private Image taskImage = null;
        [SerializeField] private Button startButton = null;
        [SerializeField] private Button restartButton = null;
        [SerializeField] private GameObject trail = null;
        [SerializeField] private Sprite triangleSprite = null;
        [SerializeField] private Sprite squareSprite = null;
        [SerializeField] private Sprite lineSprite = null;
        [SerializeField] private Sprite zSprite = null;
        [SerializeField] private Sprite circleSprite = null;
        [SerializeField] private ScoreCounter scoreCounter = null;
        [SerializeField] private GameObject endGameWindow = null;
        [SerializeField] private Image gameOverImage = null;
        [SerializeField] private Image figureImage = null;
        [SerializeField] private Text timerText = null;
        [SerializeField] private Text finalScoreText = null;
        [SerializeField] private Timer timer = null;
        private TaskImagesVariants m_img;

        #endregion

        #region Events

        public event Action StartTimer = delegate { };
        public event Action<string> CurrentFigure = delegate { };

        #endregion

        #region Unity Events

        private void OnEnable()
        {
            if (scoreCounter != null)
                scoreCounter.Scored += ChangeTaskImage;
            if (gameOverImage != null)
                gameOverImage.gameObject.SetActive(false);
            if (timer != null)
                timer.TimeOver += EndGame;
        }

        private void OnDisable()
        {
            if (scoreCounter != null)
                scoreCounter.Scored -= ChangeTaskImage;
            if (timer != null)
                timer.TimeOver -= EndGame;
        }

        private void Start()
        {
            if (pressStartImage != null)
            {
                pressStartImage.gameObject.SetActive(true);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called From GUI
        /// </summary>
        public void StartNewGame()
        {
            if (startButton != null)
            {
                pressStartImage.gameObject.SetActive(false);
                if (trail != null)
                    trail.SetActive(true);
                if (taskImage != null)
                    taskImage.gameObject.SetActive(true);
                ChangeTaskImage();
                startButton.gameObject.SetActive(false);
                StartTimer();
            }
        }

        /// <summary>
        /// Called From GUI
        /// </summary>
        public void RestartGame()
        {
            if (restartButton != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void EndGame()
        {
            if (endGameWindow != null)
            {
                if (gameOverImage != null)
                {
                    gameOverImage.gameObject.SetActive(true);
                }
                endGameWindow.SetActive(true);
                figureImage.gameObject.SetActive(false);
                timerText.text = "Time Left: 0";
                finalScoreText.text = string.Format("Your Score: {0}", ScoreCounter.score);
            }
        }

        private void ChangeTaskImage()
        {
            m_img = GetRandom();
            switch (m_img)
            {
                case (TaskImagesVariants.TRIANGLE):
                    SetImage(triangleSprite);
                    CurrentFigure("triangle");
                    break;
                case (TaskImagesVariants.LINE):
                    SetImage(lineSprite);
                    CurrentFigure("line");
                    break;
                case (TaskImagesVariants.Z):
                    SetImage(zSprite);
                    CurrentFigure("z");
                    break;
                case (TaskImagesVariants.SQUARE):
                    SetImage(squareSprite);
                    CurrentFigure("square2");
                    break;
                case (TaskImagesVariants.CIRCLE):
                    SetImage(circleSprite);
                    CurrentFigure("circle");
                    break;
                default:
                    Debug.LogWarning("No image specified!");
                    break;
            }
        }

        private void SetImage(Sprite img)
        {
            taskImage.sprite = img;
        }

        private static TaskImagesVariants GetRandom()
        {
            return (TaskImagesVariants) UnityEngine.Random.Range(0, (int) TaskImagesVariants.Z + 1);
        }

        #endregion
    }
}
