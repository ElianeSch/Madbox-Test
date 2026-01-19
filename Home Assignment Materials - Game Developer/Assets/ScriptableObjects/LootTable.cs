using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Loot Table")]
public class LootTable : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public GameObject prefab;
        public float weight = 1f;
        public int minAmount = 1;
        public int maxAmount = 1;
    }
    public List<Entry> entries = new();

    public int minEntries = 1;
    public int maxEntries = 1;

    public void SpawnLoot(Vector3 position, Quaternion rotation)
    {
        if (entries == null || entries.Count == 0) return;

        int rolls = UnityEngine.Random.Range(minEntries, maxEntries + 1);

        for (int i = 0; i < rolls; i++)
        {
            Entry chosen = PickWeightedEntry();
            if (chosen == null || chosen.prefab == null) continue;

            int min = Mathf.Min(chosen.minAmount, chosen.maxAmount);
            int max = Mathf.Max(chosen.minAmount, chosen.maxAmount);
            int amount = UnityEngine.Random.Range(min, max + 1);

            float scatterRadius = 1.2f;

            for (int j = 0; j < amount; j++)
            {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * scatterRadius;

                Vector3 offset = new Vector3(circle.x, 0f, circle.y);

                Quaternion randomRotation = Quaternion.Euler(
                    0f,
                    UnityEngine.Random.Range(0f, 360f),
                    0f
                );

                Instantiate(chosen.prefab, position + offset, randomRotation);
            }
        }
    }

    private Entry PickWeightedEntry()
    {
        float total = 0f;

        for (int i = 0; i < entries.Count; i++)
        {
            float w = Mathf.Max(0f, entries[i].weight);
            total += w;
        }

        if (total <= 0f) return null;

        float r = UnityEngine.Random.Range(0f, total);

        for (int i = 0; i < entries.Count; i++)
        {
            float w = Mathf.Max(0f, entries[i].weight);
            if (w <= 0f) continue;

            if (r < w)
                return entries[i];

            r -= w;
        }

        return entries[entries.Count - 1];
    }
}
