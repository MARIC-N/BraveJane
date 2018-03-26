using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int NumberOfLifes = 3;
    public Text ScoreText;
    public Text LifeText;

    private int _score;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int amount)
    {
        _score += amount;

        ScoreText.text = _score.ToString();
    }

    public void UpdateLIfe()
    {

    }
}
