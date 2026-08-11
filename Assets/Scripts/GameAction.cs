using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction : MonoBehaviour
{
    public float delay;

    public virtual void Action()    //virtual means it can be overriden by a child class
    { } //base.Action will just call whatever is put in the braces, otherwise nothing is needed inside
    public virtual void DeAction()
    { }
    public virtual void ResetAction()
    { }
}
