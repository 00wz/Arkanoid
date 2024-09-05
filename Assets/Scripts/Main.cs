using Cysharp.Threading.Tasks;
using System;
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
    [SerializeField]
    private List<AnimalWave> animalWaves;
    [SerializeField]
    private Hud hud;
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private float healthDownDelaySeconds = 2f;

    private int _currentWave = -1;
    private int _score = 0;

    private async UniTask Start()
    {
        gameOverWindow.SetActive(false);
        animalManager.OnDeathAnimal += OnDeathAnimal;
        ballsManager.OnAllBallsOut += ReduceHealth;
        bonusManager.Init(this);
        hud.gameObject.SetActive(true);
        hud.SetHealth(health);
        hud.SetScore(_score);

        await StartNewxtWave();
        ballsManager.SpawnBall();
    }

    private async UniTask StartNewxtWave()
    {
        _currentWave = Mathf.Min(_currentWave + 1, animalWaves.Count - 1);
        await animalManager.SpawnAnimalWave(animalWaves[_currentWave]);
    }

    private void OnDeathAnimal(Animal dyingAnimal)
    {
        if(UnityEngine.Random.Range(0f, 1f) < dyingAnimal.bonusProbability)
        {
            bonusManager.SpawnRandomBonus(dyingAnimal.transform.position);
        }
        
        if(animalManager.AnimalsCount < animalWaves[_currentWave].MinCount)
        {
            StartNewxtWave();
        }

        _score++;
        hud.SetScore(_score);
    }

    private void ReduceHealth()
    {
        health--;
        if(health <= 0)
        {
            hud.gameObject.SetActive(false);
            gameOverWindow.SetActive(true);
        }
        else
        {
            hud.SetHealth(health);
            ballsManager.SpawnBall(healthDownDelaySeconds);
        }
    }
}

[Serializable]
public struct AnimalWave
{
    [Serializable]
    public struct AnimalCount
    { 
        public Animal AnimalPrefab;
        public int Count;
    }

    public List<AnimalCount> AnimalCounts;
    public int MinCount;
}
