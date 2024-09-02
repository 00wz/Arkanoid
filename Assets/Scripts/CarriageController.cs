using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageController : MonoBehaviour
{
    [SerializeField]
    private SplineContainer spline;

    [SerializeField]
    private GameObject carriage;

    private float t = 0f;

    private void Update()
    {
        //Debug.Log(t);
        t = Mathf.Repeat( t + Input.GetAxis("Mouse X"), 1);
        carriage.transform.position = 
        //spline.EvaluateTangent(t).ToVector3();
        spline.EvaluatePosition(t).ToVector3();
    }
}

public static class Float3Extension
{
    public static Vector3 ToVector3(this Unity.Mathematics.float3 self)
    {
        return new Vector3(self.x, self.y, self.z);
    }
}
