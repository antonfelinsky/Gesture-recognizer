using System;
using UnityEngine;

namespace MurkaTestTask.Logic
{
    public class FigureController : MonoBehaviour
    {
        #region Fields

        private string taskFigureName = null;
        [SerializeField] GameInitialiser gameInitialiser = null;

        #endregion

        #region Events

        public event Action AddScore = delegate { };
        public event Action DontAddScore = delegate { };

        #endregion

        #region Unity Events

        private void OnEnable()
        {
            if (gameInitialiser != null)
            {
                gameInitialiser.CurrentFigure += SetFigureToCheck;
            }
            GestureRecognizer.FigureRecognized += CheckResult;
        }

        private void OnDisable()
        {
            if (gameInitialiser != null)
            {
                gameInitialiser.CurrentFigure -= SetFigureToCheck;
            }
            GestureRecognizer.FigureRecognized -= CheckResult;
        }

        #endregion

        #region Methods

        private void SetFigureToCheck(string sfig)
        {
            print(sfig);
            taskFigureName = sfig;
        }

        private void CheckResult(object pfig)
        {
            string paintedFigureName = pfig.ToString();
            print("Gesture detected as " + paintedFigureName);
            print("Task figure is " + taskFigureName);
            if (paintedFigureName == taskFigureName)
                AddScore();
            else
                DontAddScore();
        }

        #endregion

    }
}
