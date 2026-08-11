using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private PlayerInput pInput;
    private bool quit;

    [SerializeField]
    private GameObject quitMenu = null;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private AudioSource select;
    [SerializeField]
    private AudioSource click;
    [SerializeField]
    private TextMeshProUGUI highScore;

    public int highScoreLoad = 0;
    public static MainMenu instance;

    private bool canQuit;
    private bool isQuit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            highScoreLoad = data.highScoreLoad;
            Debug.Log("High Score: " + highScoreLoad);
        }
    }

    public bool GetIsQuit()
    {
        return isQuit;
    }

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        isQuit = false;
        HealthSystem.noEnemies = true;

        pInput.Player.Pause.performed += EnableQuit;
        pInput.Player.Pause.canceled += DisableQuit;

        pInput.Menu.Select.performed += EnableSelect;
        pInput.Menu.Click.performed += EnableClick;
    }
    private void OnDisable()
    {
        pInput.Player.Pause.performed -= EnableQuit;
        pInput.Player.Pause.canceled -= DisableQuit;

        pInput.Menu.Select.performed -= EnableSelect;
        pInput.Menu.Click.performed -= EnableClick;
    }
    private void Update()
    {
        LoadPlayer();
   
        if (PointsMenu.counter > highScoreLoad)
        {
            Debug.Log("HighScore");
            highScoreLoad = PointsMenu.counter;
            SavePlayer();
        }
        highScore.text = highScoreLoad.ToString();
        highScoreLoad = int.Parse(highScore.text);

        if (canQuit)
        {
            Debug.Log("quit game");
            canQuit = false;
            QuitMenu();
        }
        if (quit)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
    private void EnableQuit(InputAction.CallbackContext c)
    {
        canQuit = true;
    }
    private void DisableQuit(InputAction.CallbackContext c)
    {
        canQuit = false;
    }
    private void EnableSelect(InputAction.CallbackContext c)
    {
        select.PlayOneShot(select.clip);
    }
    private void EnableClick(InputAction.CallbackContext c)
    {
        click.PlayOneShot(click.clip);
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitMenu()
    {
        Debug.Log("menu screen");
        playButton.enabled = false;
        quitButton.enabled = false;
        isQuit = !isQuit;
        Time.timeScale = isQuit ? 0 : 1;
        quitMenu.SetActive(isQuit);

        if(!quitMenu.activeSelf)
        {
            Debug.Log("title screen");
            playButton.enabled = true;
            quitButton.enabled = true;
            playButton.Select();
        }
    }
    public void Quit()
    {
        quit = true;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveSystem.SavePlayer(this);
    }
    private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(this);
    }
}
