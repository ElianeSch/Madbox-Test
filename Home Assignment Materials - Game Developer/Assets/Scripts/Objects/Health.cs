using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public event Action<int, int, int> OnHealthChanged;
    private bool isDead;
    public bool IsDead => isDead;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        if (CurrentHealth <= 0) isDead = true;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth,amount);
    }

    public bool IsFullHealth()
    {
        return CurrentHealth >= maxHealth;
    }
}
