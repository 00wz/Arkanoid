using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageController : MonoBehaviour
{
    [SerializeField]
    private SplineContainer spline;
    [SerializeField]
    private Rigidbody carriage;
    [SerializeField]
    private float InputSensitivity = 0.1f;
    [SerializeField]
    [Range(0f, 1f)]
    private float slinePositionDuration = 0f;
    //[SerializeField]
    //private Transform ballSpawnPosition;

    private void Update()
    {
        slinePositionDuration = Mathf.Repeat( slinePositionDuration - Input.GetAxis("Mouse X") * InputSensitivity, 1f);
    }

    private void FixedUpdate()
    {
        carriage.MovePosition(spline.EvaluatePosition(slinePositionDuration).ToVector3());

        carriage.MoveRotation(
            Quaternion.LookRotation(spline.EvaluateTangent(slinePositionDuration).ToVector3(), Vector3.up));
    }

    private void OnValidate()
    {
        carriage.transform.position = spline.EvaluatePosition(slinePositionDuration).ToVector3(); 
        carriage.transform.rotation =
             Quaternion.LookRotation(spline.EvaluateTangent(slinePositionDuration).ToVector3(), Vector3.up);
    }
}

public static class Float3Extension
{
    public static Vector3 ToVector3(this Unity.Mathematics.float3 self)
    {
        return new Vector3(self.x, self.y, self.z);
    }
}
