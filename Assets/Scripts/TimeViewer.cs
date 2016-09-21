using UnityEngine;
using System.Collections;
using MurkaTestTask.Logic;
using UnityEngine.UI;

namespace MurkaTestTask.View
{
    public class TimeViewer : MonoBehaviour
    {
        [SerializeField] private Timer timer = null;
        [SerializeField] private Text timerText = null;

        #region UnityEvents

        private void OnEnable()
        {
            if (timer != null)
            {
                timer.TimeChanged += ShowTime;
            }
        }

        private void OnDisable()
        {
            if (timer != null)
            {
                timer.TimeChanged -= ShowTime;
            }
        }

        #endregion

        private void ShowTime(float time)
        {
            if (timer != null)
            {
                timerText.text = string.Format("Time Left: {0:0.#}", time);
            }
        }
    }
}
