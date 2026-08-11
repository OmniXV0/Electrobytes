using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput pInput;

    [SerializeField]
    private GameObject pauseScreen = null;
    [SerializeField]
    private GameObject exitScreen = null;
    [SerializeField]
    private ResetScene resetScene;
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private AudioSource select;
    [SerializeField]
    private AudioSource click;

    [SerializeField]
    private HealthSystem healthSystem;

    public bool isPaused;

    public bool GetIsPaused() 
    {  
        return isPaused; 
    }

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
    }

    public void Resume()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseButton.enabled = !isPaused;
        pauseScreen.SetActive(isPaused);
    }

    private void OnDisable()
    {
        pInput.Player.Pause.performed -= EnableClick;
        pInput.Menu.Select.performed -= EnableSelect;
        pInput.Menu.Click.performed -= EnableClick;
    }

    private void Update()
    {
        if (isPaused || healthSystem.gameOver)
        {
            pInput.Player.Pause.performed -= EnableClick;
            pInput.Menu.Select.performed += EnableSelect;
            pInput.Menu.Click.performed += EnableClick;
        }
        else
        {
            pInput.Player.Pause.performed += EnableClick;
            pInput.Menu.Select.performed -= EnableSelect;
            pInput.Menu.Click.performed -= EnableClick;
        }
        if (resetScene.unpause)
        {
            Debug.Log("Unpause");
            if (!exitScreen.activeSelf)
                pauseScreen.SetActive(false);
            else
                Resume();
        }
    }
    private void EnableSelect(InputAction.CallbackContext c)
    {
        select.PlayOneShot(select.clip);
    }
    private void EnableClick(InputAction.CallbackContext c)
    {
        click.PlayOneShot(click.clip);
    }
}
