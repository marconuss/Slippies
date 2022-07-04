using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class SLInteractable : MonoBehaviour
{
    [SerializeField]
    private GameObject planeGameOn;
    [SerializeField]
    private GameObject planeGameOff;

    [SerializeField]
    private GameObject interactUI;

    public bool isInRange;

    public InputAction actionKey;

    public UnityEvent interactAction;


    private void Start()
    {
        actionKey.Enable();
        actionKey.performed += _ => KeyPress();

        SceneManager.sceneUnloaded += OnMinigameUnloaded;
    }
    private void OnDisable()
    {
        actionKey.performed -= _ => KeyPress();
        actionKey.Disable();

        SceneManager.sceneUnloaded -= OnMinigameUnloaded;
    }


    private void OnMinigameUnloaded(Scene scene)
    {
        if (scene.name == "Slippie's")
        {
            SLApplicationManager.instance.minigameRunning = false;
            planeGameOn.SetActive(SLApplicationManager.instance.minigameRunning);
            planeGameOff.SetActive(!SLApplicationManager.instance.minigameRunning);
            interactAction.Invoke();
        }
    }

    private void KeyPress()
    {
        if (isInRange && !SLApplicationManager.instance.minigameRunning)
        {
            interactAction.Invoke();
            interactUI.SetActive(false);
            SLApplicationManager.instance.minigameRunning = true;
            planeGameOn.SetActive(SLApplicationManager.instance.minigameRunning);
            planeGameOff.SetActive(!SLApplicationManager.instance.minigameRunning);
            SceneManager.LoadSceneAsync("Slippie's", LoadSceneMode.Additive);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "FakeHarold")
        {
            isInRange = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "FakeHarold")
        {
            isInRange = false;
            interactUI.SetActive(false);
        }
    }

}
