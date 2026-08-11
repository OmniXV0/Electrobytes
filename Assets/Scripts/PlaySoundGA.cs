using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundGA : GameAction
{
    [SerializeField]
    [Tooltip("When true, it plays the same sound on DeAction")]
    private bool bDeAction;
    [SerializeField]
    private AudioSource aSource;

    public override void Action()
    {
        aSource.Play();
        Debug.Log("Play sound");
    }
    public override void DeAction()
    {
        if (bDeAction)
            aSource.Play();
    }
}
