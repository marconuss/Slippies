using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLPauseManager : MonoBehaviour
{
    private bool isPaused = true;

    public static SLPauseManager instance;

    public bool IsPaused
    {
        get => isPaused;
        set
        {
            if ((value != isPaused) && (OnPause != null))
            {
                isPaused = value;
                OnPause(value);
            }
        }
    }


    public delegate void PauseAction(bool isPaused);
    public delegate void ResetAction();
    public delegate void ExitAction();

    public PauseAction OnPause;
    public ResetAction OnReset;
    public ExitAction OnExit;


    private void Awake()
    {
        instance = this;
        IsPaused = true;
    }

}
