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

    private void Update()
    {
        slinePositionDuration = Mathf.Repeat( slinePositionDuration - Input.GetAxis("Mouse X") * InputSensitivity, 1f);
    }

    private void FixedUpdate()
    {/*
        carriage.transform.position = spline.EvaluatePosition(slinePositionDuration).ToVector3();
        carriage.transform.rotation =
             Quaternion.LookRotation(spline.EvaluateTangent(slinePositionDuration).ToVector3(), Vector3.up);
        */
        
        carriage.MovePosition(spline.EvaluatePosition(slinePositionDuration).ToVector3());

        carriage.MoveRotation(
            Quaternion.LookRotation(spline.EvaluateTangent(slinePositionDuration).ToVector3(), Vector3.up));
        
        /*
        var targetVelocity = 
            (spline.EvaluatePosition(t).ToVector3() - carriage.position) / Time.fixedDeltaTime;
        carriage.velocity = targetVelocity;*/

        /*var targetAngularSpeed = QuaternionDiff(
            Quaternion.LookRotation(spline.EvaluateTangent(t).ToVector3(), Vector3.up),
            carriage.rotation)/Time.fixedDeltaTime;*/
        /*
        var targetAngularSpeed = Quaternion.SlerpUnclamped(
            carriage.rotation,
            Quaternion.LookRotation(spline.EvaluateTangent(t).ToVector3(), Vector3.up),
            1f / Time.fixedDeltaTime);
        carriage.angularVelocity = targetAngularSpeed.eulerAngles;*/

        /*
        var targetAngularSpeed =
        (Quaternion.LookRotation(spline.EvaluateTangent(t).ToVector3(), Vector3.up).eulerAngles -
            carriage.rotation.eulerAngles) / Time.fixedDeltaTime;
        carriage.angularVelocity = targetAngularSpeed;*/
        /*
        var current = carriage.transform.forward;
        var target = spline.EvaluateTangent(t).ToVector3();
        var targetAngularSpeed =
            Vector3.Angle(current, target)
            / Time.fixedDeltaTime;
        carriage.angularVelocity = new Vector3(0f,
            Vector3.Cross(current, target).y < 0 ?
            -targetAngularSpeed :
            targetAngularSpeed,
            0f);*/
        /*
        var targetY =
            (Quaternion.LookRotation(spline.EvaluateTangent(t).ToVector3(), Vector3.up).eulerAngles.y
            - carriage.rotation.eulerAngles.y) / Time.fixedDeltaTime;
        carriage.angularVelocity = new Vector3(0f,targetY, 0f);*/
    }
    /*
    private Quaternion QuaternionDiff(Quaternion srs, Quaternion delta)
    {
        return Quaternion.Inverse(delta) * srs;
        //return srs * Quaternion.Inverse(delta);
    }*/

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
