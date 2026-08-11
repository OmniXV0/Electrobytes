using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private CircleCollider2D cCollider;
    [SerializeField]
    private float lifetime;
    [SerializeField]
    private bool isEnemyBullet;

    [SerializeField]
    private AudioSource aSource;

    private float speed = 5;
    public static float fasterSpeed = 1.0f;
    private Vector3 direction = Vector3.zero;
    private bool bActive;
    private float timer;

    public bool BActive => bActive;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
        fasterSpeed = 1;
    }
    private void FixedUpdate()
    {
        if (bActive)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= lifetime)
                DisableSelf();
            transform.position += direction * Time.fixedDeltaTime * speed * fasterSpeed;
        }
    }
    public void Activate(float spd, Vector3 dir)
    {
        bActive = true;
        sRenderer.enabled = true;
        cCollider.enabled = true;
        direction = dir;
        speed = spd;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (aSource)
            aSource.Play();
        if (isEnemyBullet)
            DisableSelf();
    }
    private void DisableSelf()
    {
        bActive = false;
        timer = 0;
    }
}
