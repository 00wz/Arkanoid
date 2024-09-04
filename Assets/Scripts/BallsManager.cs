using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private Vector3 centerOfGameZone;
    [SerializeField]
    private float radiusOfGameZone;
    [SerializeField]
    private bool drowGameZone;
    [SerializeField]
    private GameObject gameOverWindow;
    [SerializeField]
    private Transform ballSpawnPosition;

    private List<Ball> _balls = new();
    private const float DESTROY_DELAY = 5f;

    void Start()
    {
        gameOverWindow.SetActive(false);
        SpawnBall();
    }

    private void SpawnBall()
    {
        Quaternion direction = 
            Quaternion.LookRotation(Utils.GetRandomInsideUnitCircle(), Vector3.up);
        Ball ball = Instantiate(ballPrefab, ballSpawnPosition.position, direction);
        _balls.Add(ball);
    }

    void Update()
    {
        float sqrRadiusOfGameZone = radiusOfGameZone * radiusOfGameZone;
        for(int i = 0; i < _balls.Count; )
        {
            if((_balls[i].transform.position - centerOfGameZone).sqrMagnitude > sqrRadiusOfGameZone)
            {
                Destroy(_balls[i].gameObject, DESTROY_DELAY);
                _balls.RemoveAt(i);
                continue;
            }
            i++;
        }
        if(_balls.Count == 0)
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        gameOverWindow.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (drowGameZone)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireSphere(centerOfGameZone, radiusOfGameZone);
        }
    }
}
