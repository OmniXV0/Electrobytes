using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsMenu : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public static int counter = 0;

    [SerializeField]
    private Animator pointsAnim;
    [SerializeField]
    private string pointsTrigger;
    [SerializeField]
    private AudioSource pointsAudioSource;
    [SerializeField]
    private TextMeshProUGUI exclaimation;
    [SerializeField]
    private ScreenShake screenShake;

    private void OnEnable()
    {
        PointsGA.UpdatePoints += UpdateUI;
    }
    private void OnDisable()
    {
        PointsGA.UpdatePoints -= UpdateUI;
    }

    private void Start()
    {
        counter = 0;

        Debug.Log("Begin");
        exclaimation.text = "Begin";
        Exclaimation();
    }
    private void Exclaimation()
    {
        pointsAudioSource.Play();
        pointsAnim.SetTrigger(pointsTrigger);
        screenShake.start = true;
    }

    public void UpdateUI(int pointsValue)
    {
        counter += pointsValue;       //go to project settings->editor->check off enter play mode options to stop recompiling the scripts and loading the scenes
        counterText.text = counter.ToString();
        if (counter < 64)
            BulletObject.fasterSpeed = 1.0f;
        else if (counter == 64)
        {
            Debug.Log("Cool");
            exclaimation.text = "Cool";
            Exclaimation();
        }
        else if (counter >= 64 && counter < 128)
            BulletObject.fasterSpeed = 1.5f;
        else if (counter == 128)
        {
            Debug.Log("Good");
            exclaimation.text = "Good";
            Exclaimation();
        }
        else if (counter >= 128 && counter < 192)
            BulletObject.fasterSpeed = 2.0f;
        else if (counter == 192)
        {
            Debug.Log("Great");
            exclaimation.text = "Great";
            Exclaimation();
        }
        else if (counter >= 192 && counter < 256)
            BulletObject.fasterSpeed = 2.5f;
        else if (counter == 256)
        {
            Debug.Log("Excellent");
            exclaimation.text = "Excellent";
            Exclaimation();
        }
        else if (counter >= 256)
            BulletObject.fasterSpeed = 3.0f;
    }

    public void ResetPoints()
    {
        counter = 0;
        counterText.text = counter.ToString();

        Debug.Log("Begin");
        exclaimation.text = "Begin";
        Exclaimation();
    }
}
