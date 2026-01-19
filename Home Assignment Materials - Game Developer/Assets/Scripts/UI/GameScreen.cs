using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI levelTitle;
    private void Start()
    {
        GameManager.Instance.OnLevelCleared += OnLevelCleared;
        LevelManager.Instance.OnLevelChanged += OnLevelChanged;
        Hide();
    }

    private void OnLevelChanged(int levelIndex)
    {
        Show();
        levelTitle.text = "Level "+ (levelIndex + 1);
    }

    private void OnLevelCleared()
    {
        Hide();
    }
    public override void OnHide()
    {

    }

    public override void OnShow()
    {

    }
}
