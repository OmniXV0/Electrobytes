using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectGA : GameAction
{
    [SerializeField]
    private SpriteRenderer sRenderer;
    [SerializeField]
    private CircleCollider2D cCollider;

    public override void Action()
    {
        Debug.Log("Disable");
        sRenderer.enabled = false;
        cCollider.enabled = false;
    }
}
