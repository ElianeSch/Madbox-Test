using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreen : UIScreen
{
    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        Show();
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Lobby)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public override void OnHide()
    {

    }

    public override void OnShow()
    {

    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
}
