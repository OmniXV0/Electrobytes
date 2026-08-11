using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private PlayerInput pInput;

    [SerializeField]
    public int hp;
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private HealthUI hUI;
    [SerializeField]
    private PointsMenu pointsMenu;
    [SerializeField]
    private ResetScene resetScene;
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject restartScreen;
    [SerializeField]
    private GameObject exitScreen;
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private string actionTrigger;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private string playerTrigger;
    [SerializeField]
    private GameObject playerExplosion;

    public bool gameOver = true;
    public static bool noEnemies = false;

    private void Start()
    {
        Debug.Log("Start");
        pInput = new PlayerInput();
        pInput.Enable();
        ResetHealth();
        playerExplosion.SetActive(false);
    }
    public void UpdateHealth(int value)
    {
        hp -= value;
        hp = Mathf.Clamp(hp, 0, maxHealth);

        Debug.Log("Damage");
        playerAnim.enabled = true;
        playerAnim.SetTrigger(playerTrigger);

        hUI.UpdateUI(hp / (float)maxHealth);

        if (hp == 0)
        {
            noEnemies = true;
            pauseButton.enabled = false;
            resetScene.music.Stop();
            playerExplosion.SetActive(true);
            anim.enabled = true;
            anim.SetTrigger(actionTrigger);
            Debug.Log("Player Death");

            StartCoroutine(nameof(GameOver));
        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        gameOver = true;
        gameOverScreen.gameObject.SetActive(true);
        playerPrefab.SetActive(false);
    }
    public void ResetHealth()
    {
        Debug.Log("Reset");
        BulletObject.fasterSpeed = 1;
        noEnemies = false;
        gameOver = false;
        pauseButton.enabled = true;
        playerExplosion.SetActive(false);
        playerAnim.enabled = false;
        playerPrefab.GetComponent<SpriteRenderer>().color = Color.white;
        restartScreen.gameObject.SetActive(false);
        playerPrefab.SetActive(true);
        hp = maxHealth;
        hUI.UpdateUI(hp);
    }
}
