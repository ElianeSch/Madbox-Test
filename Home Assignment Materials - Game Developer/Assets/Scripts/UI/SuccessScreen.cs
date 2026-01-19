using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessScreen : UIScreen
{
    private void Start()
    {
        GameManager.Instance.OnLevelCleared += OnLevelCleared;
        LevelManager.Instance.OnLevelChanged += OnLevelChanged;
        Hide();
    }

    private void OnLevelChanged(int levelIndex)
    {
        Hide();
    }

    private void OnLevelCleared()
    {
        Show();
    }

    public override void OnHide()
    {

    }

    public override void OnShow()
    {

    }
}
