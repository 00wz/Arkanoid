using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverWindow;
    [SerializeField]
    public BallsManager ballsManager;
    [SerializeField]
    private AnimalManager animalManager;
    [SerializeField]
    private BonusManager bonusManager;

    private int _score = 0;

    private void Start()
    {
        gameOverWindow.SetActive(false);
        animalManager.OnDeathAnimal += OnDeathAnimal;
        for(int i = 0; i < 10; i++)
        animalManager.SpawnAnimal();
        ballsManager.OnAllBallsOut += GameOver;
        ballsManager.SpawnBall();
        bonusManager.Init(this);
    }

    private void OnDeathAnimal(Animal dyingAnimal)
    {
        bonusManager.SpawnRandomBonus(dyingAnimal.transform.position);
    }

    private void GameOver()
    {
        gameOverWindow.SetActive(true);
    }
}
