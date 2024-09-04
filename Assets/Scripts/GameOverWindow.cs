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
}
