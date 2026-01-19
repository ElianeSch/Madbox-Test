using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private bool dropOnDeath = true;

    private bool dropped;

    private void OnEnable()
    {
        if (health != null)
            health.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int _, int __)
    {
        if (!dropOnDeath) return;
        if (dropped) return;

        if (currentHealth <= 0)
        {
            GiveLoot();
        }
    }

    public void GiveLoot()
    {
        if (dropped) return;
        dropped = true;

        if (lootTable == null) return;
        lootTable.SpawnLoot(transform.position + Vector3.up, Quaternion.identity);
    }
}
