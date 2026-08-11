using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePlayer : MonoBehaviour
{
    [SerializeField]
    private int bulletCacheCount = 1;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform target;
    private List<BulletObject> bullets = new List<BulletObject>();

    private bool bActive;

    private void OnEnable()
    {
        CharacterControllerMovement.Fire += Fire;
        CharacterControllerMovement.StopFire += DeAction;
    }
    private void OnDisable()
    {
        CharacterControllerMovement.Fire -= Fire;
        CharacterControllerMovement.StopFire -= DeAction;
    }
    private void Start()
    {
        bulletPrefab.SetActive(false);
        for (int i = 0; i < bulletCacheCount; i++)
        {
            bullets.Add(Instantiate(bulletPrefab).GetComponent<BulletObject>());
            bullets[i].gameObject.SetActive(false);
        }
    }
    private void Action()
    {
        bulletPrefab.SetActive(true);
        bActive = true;
        StartCoroutine(nameof(Firing));
        StartCoroutine(nameof(Aiming));
    }
    private void DeAction()
    {
        bulletPrefab.SetActive(false);
        bActive = false;
    }
    private void Fire()
    {
        foreach (BulletObject b in bullets)
        {
            if (!b.BActive)
            {
                b.gameObject.SetActive(true);
                b.transform.position = spawnPoint.position;
                b.Activate(speed, transform.up);
                break;
            }
        }
    }
    IEnumerator Aiming()
    {
        Vector3 direction;
        while (bActive)
        {
            direction = target.position - transform.position;
            direction.y = 0;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Firing()
    {
        while (bActive)
        {
            yield return new WaitForSeconds(fireRate);
            foreach (BulletObject b in bullets)
            {
                if (!b.BActive)
                {
                    b.gameObject.SetActive(true);
                    b.transform.position = spawnPoint.position;
                    b.Activate(speed, transform.up);
                    break;
                }
            }
        }
    }
}
