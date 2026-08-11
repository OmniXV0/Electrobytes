using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player health");
        if (other.TryGetComponent(out HealthSystem hSystem))
        {
            hSystem.UpdateHealth(damage);
        }
    }
}
