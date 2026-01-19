using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] private Level[] levels;
    [SerializeField] private Transform levelParent;
    private int currentLevelIndex = 0;
    public Level CurrentLevel => levels[currentLevelIndex];
    public Action<int> OnLevelChanged;

    private void Start()
    {
        GameManager.Instance.OnLevelCleared += OnLevelCleared;
    }

    private void OnLevelCleared()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length)
            currentLevelIndex = 0;
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        CleanLevel();
        currentLevelIndex = levelIndex;
        Level level = levels[levelIndex];
        Instantiate(level, levelParent);
        PlayerPrefs.SetInt("CurrentLevelIndex", levelIndex);
        OnLevelChanged?.Invoke(levelIndex);
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    private void CleanLevel()
    {
        for (int i = 0; i < levelParent.childCount; i++)
        {
            if (levelParent.GetChild(i).GetComponent<Level>() != null)
                Destroy(levelParent.GetChild(i).gameObject);
        }
    }
}