using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBonus : Bonus
{
    public override void ActivateBonus()
    {
        _main.ballsManager.SpawnBall();
    }
}
