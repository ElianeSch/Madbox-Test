using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public enum GameState
    {
        Lobby,
        Loadout,
        Game
    }
    [SerializeField] private Player player;
    private GameState gameState = GameState.Lobby;
    public static event Action<GameState> OnGameStateChanged;
    private int enemyCounter;
    private int gold;
    public static Action<int> OnGoldChanged;
    public Action OnLevelCleared;

    private void Start()
    {
        Enemy.OnEnemyKilled += OnEnemyKilled;
        LevelManager.Instance.OnLevelChanged += OnLevelChanged;
        gold = PlayerPrefs.GetInt("Gold", 0);
        OnGoldChanged?.Invoke(gold);
    }

    private void OnLevelChanged(int obj)
    {
        enemyCounter = EnemySpawnerManager.Instance.enemiesCount;
        UIManager.Instance.Toast("Kill all enemies!");
    }

    public void SetGameState(GameState newState)
    {
        if (newState != gameState)
        {
            gameState = newState;
            OnGameStateChanged?.Invoke(gameState);
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void ClickOnLoadoutButton()
    {
        SetGameState(GameState.Loadout);
    }

    public void ClickOnPlayButton()
    {
        if (WeaponManager.Instance.loadout.HasAtLeastOneWeaponEquipped() == false)
        {
            UIManager.Instance.Toast("You must have at least one weapon equipped!");
            return;
        }
        SetGameState(GameState.Game);
        int lastLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex",0);
        LevelManager.Instance.LoadLevel(lastLevelIndex);
        enemyCounter = EnemySpawnerManager.Instance.enemiesCount;
    }

    public void ClickOnCloseButton()
    {
        SetGameState(GameState.Loadout);
    }

    public Player GetPlayer()
    {
        return player;
    }

    private void OnEnemyKilled()
    {
        enemyCounter--;
        if (enemyCounter == 0)
        {
            Debug.Log("OnLevelCleared");
            OnLevelCleared?.Invoke();
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("Gold", gold);
        OnGoldChanged?.Invoke(gold);
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
    }
}
