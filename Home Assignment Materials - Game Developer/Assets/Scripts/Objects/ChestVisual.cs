using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Health health;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeStrength = 0.15f;
    [SerializeField] private int shakeVibrato = 10;
    private const string OpenTrigger = "Open";
    private bool isOpen = false;
    private void Start()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth, int damage)
    {
        if (currentHealth <= 0 && !isOpen)
        {
            Open();
        }
        else
        {
            Shake();
            SpawnDamageNumber(damage, transform.position);
        }
    }

    private Tween shakeTween;

    private void Shake()
    {
        shakeTween?.Kill();
        shakeTween = transform.DOShakePosition(
            duration: shakeDuration,
            strength: new Vector3(shakeStrength, 0f, shakeStrength),
            vibrato: shakeVibrato,
            randomness: 90,
            snapping: false,
            fadeOut: true
        );
    }

    [SerializeField] private DamageNumber damageNumberPrefab;
    public void SpawnDamageNumber(int damage, Vector3 hitPosition)
    {
        DamageNumber dn = Instantiate(
            damageNumberPrefab,
            hitPosition + Vector3.up,
            Quaternion.identity
        );
        dn.Initialize(damage);
    }

    public void Open()
    {
        isOpen = true;
        animator.SetTrigger(OpenTrigger);
    }
}
