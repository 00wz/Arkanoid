using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button ContinueButton;
    [SerializeField]
    private Button ExitButton;

    private void Awake()
    {
        ContinueButton.onClick.AddListener(Unpause);
        ExitButton.onClick.AddListener(ExitGame);
    }

    public void TogglePause()
    {
        if(gameObject.activeSelf)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
