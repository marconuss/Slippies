using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SLGameManager : MonoBehaviour
{
    public static SLGameManager instance;

    public float gameSpeed;

    public bool gameOver;

    public int scoreMultiplier = 1;
    private int score;
    private int highScore;
    private string scorePlayerPrefs = "SLHighScore";

    [SerializeField]
    private SLScoreBoard scoreDisplay;
    [SerializeField]
    private SLScoreBoard highScoreDisplay;


    [HideInInspector]
    public int flagFilp = 1;

    [SerializeField]
    private LineRenderer leftLimitLine;
    [SerializeField]
    private LineRenderer rightLimitLine;

    public SlippiesInput inputActions;

    private InputAction resetHighScore;
    private InputAction multiplyScore;

    private void Awake()
    {
        inputActions = new SlippiesInput();
        inputActions.Enable();

        resetHighScore = inputActions.UI.Reset;
        multiplyScore = inputActions.UI.AddBonus;
        resetHighScore.Enable();
        resetHighScore.performed += _ => ResetHighScore();
        multiplyScore.Enable();
        multiplyScore.performed += _ => ScoreMultiplier();

        instance = this;
    }
    private void OnEnable()
    {
        highScore = PlayerPrefs.GetInt(scorePlayerPrefs, 0);
        highScoreDisplay.UpdateNumberUI(highScore);

        if (SLPauseManager.instance)
        {
            SLPauseManager.instance.OnPause += OnPause;
            SLPauseManager.instance.OnReset += OnReset;
        }

    }

    private void OnDisable()
    {
        resetHighScore.Disable();
        multiplyScore.Disable();
        resetHighScore.performed -= _ => ResetHighScore();
        multiplyScore.performed -= _ => ScoreMultiplier();
        PlayerPrefs.SetInt(scorePlayerPrefs, highScore);
        if (SLPauseManager.instance)
        {
            SLPauseManager.instance.OnPause -= OnPause;
            SLPauseManager.instance.OnReset -= OnReset;
        }
    }

    private void OnPause(bool isPaused)
    {
        gameSpeed = 0;
    }
    private void OnReset()
    {
        gameOver = false;
        ResetScoreMultiplier(); 
        scoreDisplay.ResetNumberUI();
        if (score > highScore)
        {
            highScore = score;
            highScoreDisplay.UpdateNumberUI(score);
            PlayerPrefs.SetInt(scorePlayerPrefs, highScore);
        }
    }
    public void UpdateScore()
    {
        score += (1 * scoreMultiplier);
        scoreDisplay.UpdateNumberUI(score);

    }
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt(scorePlayerPrefs, 0);
        highScoreDisplay.ResetNumberUI();
    }

    public void ScoreMultiplier()
    {
        scoreMultiplier += 1;
    }

    public void ResetScoreMultiplier()
    {
        scoreMultiplier = 1;
    }


    public LineRenderer GetLeftLimit()
    {
        return leftLimitLine;
    }
    public LineRenderer GetRightLimit()
    {
        return rightLimitLine;
    }
}
