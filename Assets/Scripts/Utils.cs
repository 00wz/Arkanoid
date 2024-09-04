using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomInsideUnitCircle()
    {
        Vector3 randomCorclePoint = Random.insideUnitCircle;
        return new Vector3(randomCorclePoint.x, 0f, randomCorclePoint.y);
    }
}
