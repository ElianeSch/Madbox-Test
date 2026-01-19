using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesCountText;
    [SerializeField] private float punchScale = 0.2f;
    [SerializeField] private float punchDuration = 0.15f;
    private int totalEnemiesCount;
    private int enemiesKilled;

    private void Awake()
    {
       EnemySpawnerManager.OnAllEnemiesSpawned += OnAllEnemiesSpawned;
       Enemy.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnAllEnemiesSpawned()
    {
        Debug.Log("OnAllEnemiesSpawned");
        totalEnemiesCount = EnemySpawnerManager.Instance.enemiesCount;
        enemiesKilled = 0;
        enemiesCountText.text = enemiesKilled + " / " + totalEnemiesCount;
    }
    private Tween punchTween;
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        enemiesCountText.text = enemiesKilled + " / " + totalEnemiesCount;
        punchTween?.Kill();
        punchTween = enemiesCountText.transform
            .DOPunchScale(Vector3.one * punchScale, punchDuration, vibrato: 6, elasticity: 0.5f).OnComplete(()=>enemiesCountText.transform.localScale = Vector3.one);
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
        EnemySpawnerManager.OnAllEnemiesSpawned -= OnAllEnemiesSpawned;
    }
}
