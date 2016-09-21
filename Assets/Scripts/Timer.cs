using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MurkaTestTask.Logic
{
    public class Timer : MonoBehaviour
    {

        #region Fields

        public static float initialTime = 20;
        public static float timing = 20;
        private float levelTiming = 20;
        private bool timerCanStart = false;
        private string timeLeft;
        [SerializeField] private GameInitialiser gameInitialiser = null;
        private bool timerStarted = false;

        #endregion

        #region Events

        public event Action<float> TimeChanged = delegate { };
        public event Action TimeOver = delegate { };

        #endregion

        #region Unity Events

        private void Awake()
        {
            timing = levelTiming;
            initialTime = levelTiming;
        }

        private void OnEnable()
        {
            if (gameInitialiser != null)
                gameInitialiser.StartTimer += OnStartTimer;
        }

        private void OnDisable()
        {
            if (gameInitialiser != null)
                gameInitialiser.StartTimer -= OnStartTimer;
        }

        void Update()
        {
            if (!timerStarted)
                StartCoroutine(TurnTimerOn());
        }

        #endregion

        #region Methods

        private void OnStartTimer()
        {
            timerCanStart = true;
        }

        #endregion

        private IEnumerator TurnTimerOn()
        {

            timerStarted = true;
            if (timerCanStart)
            {
                if (timing > 0)
                {
                    timing -= 0.1f;
                    TimeChanged(timing);
                }
                if (timing < 0)
                {
                    TimeOver();
                }
            }
            yield return new WaitForSeconds(0.1f);
            timerStarted = false;
        }
    }
}
