using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private Text Health;
    [SerializeField]
    private Text Score;

    public void SetHealth(int value)
    {
        Health.text = value.ToString();
    }

    public void SetScore(int value)
    {
        Score.text = value.ToString();
    }

}
