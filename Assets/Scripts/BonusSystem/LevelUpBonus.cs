using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBonus : Bonus
{
    public override void ActivateBonus()
    {
        _main.carriageController.LevelUp();
    }
}
