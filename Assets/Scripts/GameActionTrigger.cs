using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameActionTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("if enabled, it triggers DeAction on OnTriggerExit")]
    private bool bToggle;
    [SerializeField]
    private List<GameAction> actions;
    [SerializeField]
    private List<GameAction> otherActions;
    private bool bAction, bDeAction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bAction) return;
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Projectile Collide");
            bAction = true;
            StartCoroutine(nameof(ExecuteActions));
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player Collide");
            bAction = true;
            StartCoroutine(nameof(ExecuteOtherActions));
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (bAction) return;
        if (bToggle)
        {
            if (other.CompareTag("Projectile"))
            {
                bAction = true;
                bDeAction = true;
                StartCoroutine(nameof(ExecuteActions));
            }
            else if (other.CompareTag("Player"))
            {
                bAction = true;
                bDeAction = true;
                StartCoroutine(nameof(ExecuteOtherActions));
            }
        }
    }
    IEnumerator ExecuteActions()
    {
        foreach (GameAction item in actions)
        {
            yield return new WaitForSeconds(item.delay);

            if (bDeAction)
                item.DeAction();
            else
                item.Action();
        }
        bAction = false;
        bDeAction = false;
    }
    IEnumerator ExecuteOtherActions()
    {
        foreach (GameAction item in otherActions)
        {
            yield return new WaitForSeconds(item.delay);

            if (bDeAction)
                item.DeAction();
            else
                item.Action();
        }
        bAction = false;
        bDeAction = false;
    }
}
