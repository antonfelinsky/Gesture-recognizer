using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using MurkaTestTask.Logic;

public class ScoreCounter : MonoBehaviour {

    #region Fields
    public static int score = 0;
    private string currentScore = null;
    private string taskFigureName = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text SuccessOrFail = null;
    [SerializeField] GameInitialiser gameInitialiser = null;
    #endregion

    #region Events
    public event Action Scored = delegate { };
    #endregion

    #region UnityEvents

    private void Awake()
    {
        score = 0;
    }
    private void OnEnable()
    {
        GestureRecognizer.FigureRecognized += CheckResult;
        if (gameInitialiser != null)
        {
            gameInitialiser.CurrentFigure += SetFigureToCheck;
        }       
        SuccessOrFail.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        GestureRecognizer.FigureRecognized -= CheckResult;
        if (gameInitialiser != null)
        {
            gameInitialiser.CurrentFigure -= SetFigureToCheck;
        }
    }
    #endregion

    #region Methods
    public void AddScore()
    {
        score += 1;
        currentScore = string.Format("Your Score: {0:0.##}", score);
        scoreText.text = currentScore;
        Timer.initialTime = Timer.initialTime - 1;
        Timer.timing = Timer.initialTime;
        Scored();
        if (SuccessOrFail != null)
        {
            SuccessOrFail.text = "Success";
            SuccessOrFail.color = Color.blue;
            StartCoroutine(Wait(true));
        }
    }
    public void DoNothing()
    {
        if (SuccessOrFail != null)
        {
            SuccessOrFail.text = "Failed";
            SuccessOrFail.color = Color.red;
            StartCoroutine(Wait(false));
        }
    }

    private void CheckResult(object pfig)
    {
        string paintedFigureName = pfig.ToString();
        print("Gesture detected as " + paintedFigureName);
        print("Task figure is " + taskFigureName);
        if (paintedFigureName == taskFigureName)
            AddScore();
        else
            DoNothing();
    }

    private void SetFigureToCheck(string sfig)
    {
        print(sfig);
        taskFigureName = sfig;
    } 
    #endregion

    private IEnumerator Wait(bool result)
    {     
            yield return new WaitForSeconds(1);
        if (result)
        {
            if (SuccessOrFail != null)
                SuccessOrFail.text = null;
        }
        else
        {
            if (SuccessOrFail != null)
        SuccessOrFail.text = null;
        }       
    }
}
