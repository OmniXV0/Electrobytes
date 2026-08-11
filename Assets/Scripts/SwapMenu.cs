using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapMenu : MonoBehaviour
{
    public GameObject currentMenu, nextMenu;
    [SerializeField]
    private CharacterControllerMovement player;
    private void Update()
    {
        if (player.bCanPause && player.isInMenu)
        {
            player.bCanPause = false;
            player.isInMenu = false;
            NextMenu();
        }
    }
    public void NextMenu()
    {
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
    }
}
