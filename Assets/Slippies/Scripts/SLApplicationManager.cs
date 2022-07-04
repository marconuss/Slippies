using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SLApplicationManager : MonoBehaviour
{

    public static SLApplicationManager instance;

    public bool minigameRunning = false;

    public InputAction escapeAction;

    [SerializeField]
    private GameObject player;

	private StarterAssets.ThirdPersonController thirdPersonController;
	private PlayerInput playerInput;

    [SerializeField]
    private PanelManager panelManager;

    private void OnEnable()
    {
        escapeAction.Enable();
        escapeAction.performed += _ => EscapeAction();
    }

    private void Start()
    {
        thirdPersonController = player.GetComponent<StarterAssets.ThirdPersonController>();
        playerInput = player.GetComponent<PlayerInput>();
        instance = this;
        playerInput.DeactivateInput();
    }

    void EscapeAction()
    {
        if(!minigameRunning)
        {
            panelManager.OpenPanel(panelManager.initiallyOpen);
            playerInput.DeactivateInput();
            thirdPersonController.SetCursorLocked(false);
        }
    }

    public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}


}
