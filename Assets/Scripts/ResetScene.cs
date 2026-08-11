using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public static Action ResetWorld = delegate { };

    public bool bResetWorld;
    public bool unpause = false;

    [SerializeField]
    public AudioSource music;

    private void Start()
    {
        unpause = false;
    }

    private void Update()
    {
        if (bResetWorld)
        {
            bResetWorld = false;
            ResetWorld();
        }
    }

    public void ContinueButtonPressed()
    {
        music.Play();
        bResetWorld = true;
    }
    public void QuitButtonPressed()
    {
        unpause = true;
        bResetWorld = false;
        SceneManager.LoadScene("MainMenu");
    }
}
