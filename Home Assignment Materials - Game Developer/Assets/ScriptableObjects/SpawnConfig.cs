using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawning/Spawn Config")]
public class SpawnConfig : ScriptableObject
{
    public List<EnemySpawnEntry> singles = new();
    public List<SquadSpawnEntry> squads = new();

    [Serializable]
    public class EnemySpawnEntry
    {
        public GameObject prefab;
        public int count = 10;
    }

    [Serializable]
    public class SquadDefinition
    {
        public List<SquadMember> members = new();
        [Serializable]
        public class SquadMember
        {
            public GameObject prefab;
            public int count = 3;
        }
    }

    [Serializable]
    public class SquadSpawnEntry
    {
        public SquadDefinition squad;
        public int squadCount = 3;
        public float radius = 2.0f;
    }
}