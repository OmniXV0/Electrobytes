using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGA : GameAction
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private string actionTrigger;

    public override void Action()
    {
        anim.enabled = true;
        Debug.Log("Play Animation");
        anim.SetTrigger(actionTrigger);
    }
}
