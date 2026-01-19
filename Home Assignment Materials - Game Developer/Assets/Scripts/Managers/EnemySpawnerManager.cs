using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerManager : Manager<EnemySpawnerManager>
{
    [SerializeField] private Transform enemiesParent;
    private Dictionary<SpawnPoint,bool> spawnPointsDict = new Dictionary<SpawnPoint,bool>();
    private List<GameObject> enemies = new List<GameObject>(); 
    public int enemiesCount;
    public static Action OnAllEnemiesSpawned;

    protected override void Awake()
    {
        base.Awake();
        if (enemiesParent == null)
            enemiesParent = transform;
    }

    private void Start()
    {
        LevelManager.Instance.OnLevelChanged += OnLevelChanged;
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Loadout)
        {
            RemoveEnemies();
        }
    }

    private void OnLevelChanged(int currentLevelIndex)
    {
        enemiesCount = 0;
        spawnPointsDict.Clear();
        List<SpawnPoint> spawnPoints = LevelManager.Instance.CurrentLevel.enemiesSpawnPointParent.GetComponentsInChildren<SpawnPoint>().ToList();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPointsDict.Add(spawnPoints[i], false);
        }
        SpawnConfig(LevelManager.Instance.CurrentLevel.config);
        OnAllEnemiesSpawned?.Invoke();
    }

    public void SpawnConfig(SpawnConfig config)
    {
        if (config == null)
        {
            return;
        }

        foreach (var entry in config.singles)
        {
            if (entry.prefab == null || entry.count <= 0) continue;

            for (int i = 0; i < entry.count; i++)
            {
                Spawn(entry.prefab, GetRandomPoint());
            }
        }

        foreach (var squadEntry in config.squads)
        {
            if (squadEntry.squad == null || squadEntry.squad.members == null) continue;
            if (squadEntry.squadCount <= 0) continue;

            for (int s = 0; s < squadEntry.squadCount; s++)
            {
                SpawnSquad(squadEntry, GetRandomPoint());
            }
        }
    }

    private void SpawnSquad(SpawnConfig.SquadSpawnEntry squadEntry, Vector3 center)
    {
        float radius = Mathf.Max(0f, squadEntry.radius);

        foreach (var member in squadEntry.squad.members)
        {
            if (member.prefab == null || member.count <= 0) continue;

            for (int i = 0; i < member.count; i++)
            {
                Vector3 offset = UnityEngine.Random.insideUnitSphere * radius;
                offset.y = 0f;

                Vector3 pos = center + offset;
                Spawn(member.prefab, pos);
            }
        }
    }

    private GameObject Spawn(GameObject prefab, Vector3 position)
    {
        var go = Instantiate(prefab, position, Quaternion.identity, enemiesParent);
        enemies.Add(go);
        enemiesCount++;
        return go;
    }

    private Vector3 GetRandomPoint()
    {
        List<SpawnPoint> freePoints = new List<SpawnPoint>();
        foreach (var kvp in spawnPointsDict)
        {
            if (!kvp.Value)
                freePoints.Add(kvp.Key);
        }
        if (freePoints.Count == 0)
        {
            return Vector3.zero;
        }
        int index = UnityEngine.Random.Range(0, freePoints.Count);
        SpawnPoint chosen = freePoints[index];
        spawnPointsDict[chosen] = true;
        return chosen.transform.position;
    }

    private void RemoveEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            Destroy(enemy);
        }
        enemies.Clear();
    }
}