using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private Button RestartButton;
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private Text CurrentScore;
    [SerializeField]
    private Text BestScore;

    private const string CURRENT_SCORE_LABEL = "You score: ";
    private const string BEST_SCORE_LABEL = "Best score: ";

    private void Awake()
    {
        RestartButton.onClick.AddListener(RestartGame);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetScores(int best, int current)
    {
        CurrentScore.text = CURRENT_SCORE_LABEL + current.ToString();
        BestScore.text = BEST_SCORE_LABEL + best.ToString();
    }
}
