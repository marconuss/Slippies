using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLPlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SLPauseManager.instance.OnPause += IdleAnimationPlayPause;
    }
    private void OnDestroy()
    {
        SLPauseManager.instance.OnPause -= IdleAnimationPlayPause;   
    }

    private void IdleAnimationPlayPause(bool value)
    {
        _animator.SetBool("isPaused", SLPauseManager.instance.IsPaused);
    }
}
