using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SLMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject resumeButton;

    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private GameObject gameoverButton;

    [SerializeField]
    private GameObject player;

    private bool gameStarted;

    private bool playerInPosition;
    private Vector3 playerStartPosition;

    [SerializeField]
    private float speed = 2;

    private InputAction submit;
    private InputAction escape;

    private void OnEnable()
    {
        submit = SLGameManager.instance.inputActions.UI.Submit;
        escape = SLGameManager.instance.inputActions.UI.Cancel;

        submit.Enable();
        submit.performed += _ => OnSubmit();
        escape.Enable();
        escape.performed += _ => OnCancel();
    }

    private void Start()
    {
        SLPauseManager.instance.OnPause += OnPause;
        gameStarted = false;
        playerInPosition = true;
        startButton.SetActive(true);
        resumeButton.SetActive(false);
        restartButton.SetActive(false);
        gameoverButton.SetActive(false);

        playerStartPosition = player.transform.position;
    }
    private void OnDisable()
    {
        if (SLPauseManager.instance)
        {
            SLPauseManager.instance.OnPause -= OnPause;
        }
        submit.performed -= _ => OnSubmit();
        escape.performed -= _ => OnCancel();
        submit.Disable();
        escape.Disable();
    }

    private void OnPause(bool isPaused)
    {
        if (gameStarted)
        {
            if (SLGameManager.instance.gameOver)
            {
                restartButton.SetActive(isPaused);
                gameoverButton.SetActive(isPaused);
            }
            else
            {
                resumeButton.SetActive(isPaused);
            }
        }
    }

    public void PlayPauseGame()
    {
        SLPauseManager.instance.IsPaused = !SLPauseManager.instance.IsPaused;
    }

    private void Update()
    {
        if (!playerInPosition)
        {
            float step = speed * Time.unscaledDeltaTime;
            player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, 
                                                                 new Vector3(playerStartPosition.x, playerStartPosition.y -1, 0),
                                                                 step);
            if ((player.transform.localPosition.y - Vector3.down.y) < 0.01f)
            {
                playerInPosition = true;
                if (!gameStarted)
                {
                    PlayPauseGame();
                    gameStarted = true;
                }
            }
        }
    }

    public void OnSubmit()
    {
        if (playerInPosition && !gameStarted)
        {
            playerInPosition = false;
            startButton.SetActive(false);
        }
        else if (SLPauseManager.instance.IsPaused)
        {
            if (SLGameManager.instance.gameOver)
            {
                ResetGame();
            }
            else
            {
                PlayPauseGame();
            }
        }
    }
    public void OnCancel()
    {
        if (gameStarted)
        {
            if (SLGameManager.instance.gameOver || SLPauseManager.instance.IsPaused)
            {
                ResetGame();
            }
            else
            {
                PlayPauseGame();
            }
        }
        else if (!gameStarted && playerInPosition)
        {
            SLGameManager.instance.inputActions.Disable();
            SceneManager.UnloadSceneAsync("Slippie's");
        }
    }


    private void ResetGame()
    {
        gameStarted = false;
        playerInPosition = true;
        startButton.SetActive(true);
        resumeButton.SetActive(false);
        restartButton.SetActive(false);
        gameoverButton.SetActive(false);
        SLPauseManager.instance.OnReset();
    }
}
