using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverWindow;
    [SerializeField]
    private BallsManager ballsManager;
    [SerializeField]
    private AnimalManager animalManager;

    private void Start()
    {
        gameOverWindow.SetActive(false);
        for(int i = 0; i < 10; i++)
        animalManager.SpawnAnimal();
        ballsManager.OnAllBallsOut += GameOver;
        ballsManager.SpawnBall();
    }

    private void GameOver()
    {
        gameOverWindow.SetActive(true);
    }
}
