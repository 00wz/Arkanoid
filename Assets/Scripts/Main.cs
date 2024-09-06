using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameOverWindow gameOverWindow;
    [SerializeField]
    public BallsManager ballsManager;
    [SerializeField]
    private AnimalManager animalManager;
    [SerializeField]
    private BonusManager bonusManager;
    [SerializeField]
    public CarriageController carriageController;
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
    private const string BEST_SCORE_SAVE_KEY = "best_score";

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            carriageController.LevelUp();
        }
    }

    private async UniTask Start()
    {
        gameOverWindow.gameObject.SetActive(false);
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
        ReduceHealthAsync();
    }

    private async UniTask ReduceHealthAsync()
    {
        health--;
        hud.SetHealth(health);

        await UniTask.Delay(TimeSpan.FromSeconds(healthDownDelaySeconds));

        if (health <= 0)
        {
            GameOver();
        }
        else
        {
            ballsManager.SpawnBall();
        }
    }

    private void GameOver()
    {
        var bestScore = PlayerPrefs.GetInt(BEST_SCORE_SAVE_KEY, 0);
        if(_score > bestScore)
        {
            PlayerPrefs.SetInt(BEST_SCORE_SAVE_KEY, _score);
            bestScore = _score;
        }

        hud.gameObject.SetActive(false);
        gameOverWindow.SetScores(bestScore, _score);
        gameOverWindow.gameObject.SetActive(true);
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
