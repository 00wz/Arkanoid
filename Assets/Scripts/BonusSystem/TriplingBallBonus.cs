using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplingBallBonus : Bonus
{
    public override void ActivateBonus()
    {
        _main.ballsManager.MultiplyBalls(3);
    }
}
