using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointsGA : GameAction
{
    [SerializeField]
    private int pointsValue = 1;

    public static Action<int> UpdatePoints = delegate { }; //"static" means you can only have one instance of the function/delegate

    public override void Action()
    {
        UpdatePoints(pointsValue);
    }
}
