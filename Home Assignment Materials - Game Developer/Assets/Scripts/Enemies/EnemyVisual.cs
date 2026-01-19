using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material deadMaterial;
    [SerializeField] private SkinnedMeshRenderer renderer;
    [SerializeField] private ParticleSystem particlesOnHit;
    [SerializeField] private ParticleSystem particlesOnDestroy;
    private Material originalMaterial;
    private const string damagedTrigger = "Damaged";
    private const string dieTrigger = "Die";

    private void Awake()
    {
        enemy.OnDamaged += HandleDamaged;
        enemy.OnDie += HandleDie;
        originalMaterial = renderer.material;
    }

    private void HandleDie()
    {
        animator.SetTrigger(dieTrigger);
        renderer.material = deadMaterial;
    }

    private void HandleDamaged(int damage)
    {
        animator.SetTrigger(damagedTrigger);
        renderer.material = hitMaterial;
        particlesOnHit.Play();
        SpawnDamageNumber(damage, transform.position + Vector3.up);
        DOVirtual.DelayedCall(0.3f, () => renderer.material = originalMaterial);
    }

    public void OnEndDeathAnimation()
    {
        particlesOnDestroy.Play();
        enemy.Remove();
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

    private void OnDestroy()
    {
        enemy.OnDamaged -= HandleDamaged;
        enemy.OnDie -= HandleDie;
    }
}
