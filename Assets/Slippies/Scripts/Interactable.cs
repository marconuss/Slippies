using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public bool minigameRunning = false;

    public InputAction actionKey;
    public InputAction escapeAction;

    public UnityEvent interactAction;


    private void Start()
    {
        actionKey.Enable();
        actionKey.performed += _ => KeyPress();
        escapeAction.Enable();
        escapeAction.performed += _ => EscapeAction();

        SceneManager.sceneUnloaded += OnMinigameUnloaded;
    }
    private void OnDisable()
    {
        actionKey.performed -= _ => KeyPress();
        actionKey.Disable();
        escapeAction.performed -= _ => EscapeAction();
        escapeAction.Disable();

        SceneManager.sceneUnloaded -= OnMinigameUnloaded;
    }


    private void OnMinigameUnloaded(Scene scene)
    {
        if(scene.name == "Slippie's")
        {
            minigameRunning = false;
             interactAction.Invoke();
        }
    }

    private void KeyPress()
    {
        if (isInRange && !minigameRunning)
        {
            interactAction.Invoke();
            minigameRunning = true;
            SceneManager.LoadSceneAsync("Slippie's", LoadSceneMode.Additive);
        }
    }

    private void EscapeAction()
    {
        if (isInRange && !minigameRunning)
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "FakeHarold")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "FakeHarold")
        {
            isInRange = false;
        }
    }

    //private void Update()
    //{
    //    if(isInRange && SLGameManager.instance)
    //    {
    //        minigameRunning = SLGameManager.instance.slippiesIsRunning;
    //    }
    //}
}
