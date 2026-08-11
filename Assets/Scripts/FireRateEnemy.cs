using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateEnemy : MonoBehaviour
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
    [SerializeField]
    private Vector3 fireDirection;
    private List<BulletObject> bullets = new List<BulletObject>();

    private bool bActive;

    private void Start()
    {
        bulletPrefab.SetActive(false);
        for (int i = 0; i < bulletCacheCount; i++)
        {
            bullets.Add(Instantiate(bulletPrefab).GetComponent<BulletObject>());
            bullets[i].gameObject.SetActive(false);
        }

        bActive = true;
        StartCoroutine(nameof(Firing));
        StartCoroutine(nameof(Aiming));
    }
    IEnumerator Aiming()
    {
        Vector3 direction;
        while (bActive)
        {
            direction = target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Firing()
    {
        while (bActive)
        {
            fireRate = Random.Range(0.5f, 1f);
            yield return new WaitForSeconds(fireRate);
            foreach (BulletObject b in bullets)
            {
                if (!b.BActive)
                {
                    b.gameObject.SetActive(true);
                    b.transform.position = spawnPoint.position;
                    if (!HealthSystem.noEnemies)
                        b.Activate(speed, fireDirection);
                    break;
                }
            }
        }
    }
}
