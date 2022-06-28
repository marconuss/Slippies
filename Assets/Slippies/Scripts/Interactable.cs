using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public bool isInRange;

    public InputAction actionKey;

    public UnityEvent interactAction;



    private void Start()
    {
        actionKey.Enable();
        actionKey.performed += KeyPress;
    }
    private void OnDisable()
    {
        actionKey.Disable();
    }

    private void KeyPress(InputAction.CallbackContext context)
    {
        if (isInRange)
        {
            Debug.Log("Action");
            interactAction.Invoke();
            SceneManager.LoadSceneAsync("Slippie's", LoadSceneMode.Additive);
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
}
