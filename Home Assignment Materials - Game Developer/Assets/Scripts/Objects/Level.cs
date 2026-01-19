using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public SpawnConfig config;
    public Transform playerSpawnPoint;
    public Transform enemiesSpawnPointParent;
    [HideInInspector] public int enemiesCount;
}