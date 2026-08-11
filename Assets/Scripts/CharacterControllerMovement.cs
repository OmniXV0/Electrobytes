using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField]
    private float fireInterval = 0.1f;
    private Vector3 direction;
    private Quaternion desiredRotation;
    private CharacterController cController;
    private bool bCanFire = false;
    private bool bFiring;

    public bool bCanPause = false;
    [SerializeField]
    private PauseMenu pauseMenu;

    public bool isInMenu = false;

    [SerializeField]
    private AudioSource aSource;

    public static Action Fire = delegate { };
    public static Action StopFire = delegate { };

    [SerializeField]
    private HealthSystem healthSystem;

    void Start()
    {
        PlayerReset();
    }
    private void OnDisable()
    {
        pInput.Player.Shoot.performed -= EnableFire;
        pInput.Player.Shoot.canceled -= DisableFire;

        pInput.Player.Pause.performed -= EnablePause;
        pInput.Player.Pause.canceled -= DisablePause;
    }

    void Update()
    {
        direction = pInput.Player.Aim.ReadValue<Vector2>();

        if (bCanPause && !isInMenu)
        {
            Debug.Log("Pause Menu");
            pauseMenu.Resume();
            bCanPause = false;
        }

        if (direction.magnitude > 0.5f)
        {
            desiredRotation = Quaternion.LookRotation(transform.forward,direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * 10);
        }
        if (healthSystem.hp == 0)
        {
            pInput.Player.Shoot.performed -= EnableFire;
            pInput.Player.Shoot.canceled -= DisableFire;

            pInput.Player.Pause.performed -= EnablePause;
            pInput.Player.Pause.canceled -= DisablePause;
        }
    }
    public void Menu()
    {
        isInMenu = true;
        Cursor.visible = true;
        Debug.Log("In Menu");

        pInput.Player.Pause.performed += EnablePause;
        pInput.Player.Pause.canceled += DisablePause;

        if (bCanPause && isInMenu)
        {
            Debug.Log("Not in Menu");
            isInMenu = false;
            bCanPause = false;
        }
    }
    public void ExitMenu()
    {
        Debug.Log("Exit Menu");
        isInMenu = false;
    }
    IEnumerator Firing()
    {
        while(bFiring) 
        {
            if(bCanFire)
            {
                if (aSource)
                    aSource.Play();
                Fire();
            }
            yield return new WaitForSeconds(fireInterval);            
        }
    }

    private void EnableFire(InputAction.CallbackContext c)
    {
        bCanFire = true;
    }
    private void DisableFire(InputAction.CallbackContext c)
    {
        bCanFire = false;
    }
    private void EnablePause(InputAction.CallbackContext c)
    {
        bCanPause = true;
    }
    private void DisablePause(InputAction.CallbackContext c)
    {
        bCanPause = false;
    }

    public void PlayerReset()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        cController = GetComponent<CharacterController>();

        pInput.Player.Shoot.performed += EnableFire;
        pInput.Player.Shoot.canceled += DisableFire;

        pInput.Player.Pause.performed += EnablePause;
        pInput.Player.Pause.canceled += DisablePause;

        isInMenu = false;

        bFiring = true;
        bCanFire = false;
        StartCoroutine(nameof(Firing));
    }
}