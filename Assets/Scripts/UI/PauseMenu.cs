using System;
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
    [SerializeField]
    private Slider MouseSencetivity;

    public event Action<float> OnMouseSencetivityChanged;

    private void Awake()
    {
        ContinueButton.onClick.AddListener(Unpause);
        ExitButton.onClick.AddListener(ExitGame);
    }

    public void Init(float mouseSencetivity)
    {
        MouseSencetivity.value = mouseSencetivity;
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
        OnMouseSencetivityChanged?.Invoke(MouseSencetivity.value);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
