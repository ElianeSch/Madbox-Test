using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action OnDie;
    public Action<int> OnDamaged;
    public static Action OnEnemyKilled;
    [SerializeField] private Health health;

    private void Start()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth, int damage)
    {
        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            OnDamaged?.Invoke(damage);
        }
    }

    public void Remove()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject,0.15f);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }
}
