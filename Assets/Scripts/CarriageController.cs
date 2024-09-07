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
    public float InputSensitivity = 0.1f;
    [SerializeField]
    [Range(0f, 1f)]
    private float slinePositionDuration = 0f;
    [SerializeField]
    private AudioClip lvlUpAudioClip;

    private readonly Rigidbody[] carriages = new Rigidbody[8];
    private readonly float[] deltaDurations = 
        { 0f, 0.5f, 0.25f, 0.75f, 0.125f, 0.375f, 0.625f, 0.875f };
    private readonly int[] numberOfCarriagesPerLevel = { 1, 2, 4, 8 };
    private int _currentLevel;

    private void Start()
    {
        carriages[0] = carriage;
        for(int i = 1; i < carriages.Length; i++)
        {
            carriages[i] = Instantiate(carriage, transform);
        }

        SetLevel(0);
    }

    public void LevelUp()
    {
        int nextLevel = Mathf.Min(numberOfCarriagesPerLevel.Length - 1, _currentLevel + 1);
        SetLevel(nextLevel);
        Utils.PlayClip2D(lvlUpAudioClip);
    }

    private void SetLevel(int num)
    {
        int i = 0;
        for(; i < numberOfCarriagesPerLevel[num]; i++)
        {
            carriages[i].gameObject.SetActive(true);
        }
        for(; i < carriages.Length; i++)
        {
            carriages[i].gameObject.SetActive(false);
        }
        _currentLevel = num;
    }

    private void Update()
    {
        if(Time.timeScale != 0f)//pause check
        {
            slinePositionDuration -= Input.GetAxis("Mouse X") * InputSensitivity;
        }
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < numberOfCarriagesPerLevel[_currentLevel]; i++)
        {
            float duration = Mathf.Repeat(slinePositionDuration + deltaDurations[i], 1f);
            carriages[i].MovePosition(spline.EvaluatePosition(duration).ToVector3());
            carriages[i].MoveRotation(
                Quaternion.LookRotation(spline.EvaluateTangent(duration).ToVector3(), Vector3.up));
        }
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
