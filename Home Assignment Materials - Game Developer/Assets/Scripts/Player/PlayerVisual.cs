using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;
    private const string walkBool = "IsWalking";
    private const string attackBool = "IsAttacking";
    private const string attackSpeedFloat = "AttackSpeed";
    private const string attackTypeSwingBool = "AttackSwing";
    private const string attackTypeRotateBool = "AttackMelee";
    private string currentAttackType = "AttackSwing";

    private void Start()
    {
        player.OnStartMoving += OnStartMoving;
        player.OnStopMoving += OnStopMoving;
        player.OnAttackStarted += OnAttack;
        player.OnAttackCancelled += OnAttackCancelled;
    }

    private void OnStartMoving()
    {
        animator.SetBool(walkBool, true);
    }

    private void OnStopMoving()
    {
        animator.SetBool(walkBool, false);
    }

    private void OnAttack()
    {
        animator.SetFloat(attackSpeedFloat, player.WeaponHandler.CurrentWeapon.attackSpeed);
        currentAttackType = player.WeaponHandler.CurrentWeapon.attackType == AttackType.Swing ? attackTypeSwingBool : attackTypeRotateBool;
        animator.SetBool(currentAttackType, true);
        animator.SetBool(attackBool, true);
    }

    public void OnHit() // quand l'épée est au plus bas, avant l'anim où le joueur se remet, fonction appelée par event de l'anim
    {
        player.HitTarget();
    }

    private void OnAttackCancelled()
    {
        animator.SetBool(attackBool, false);
        animator.SetBool(currentAttackType, false);
    }

    public void OnAttackFinished() // quand l'anim s'est finie de jouer
    {
        animator.SetBool(attackBool, false);
        animator.SetBool(currentAttackType, false);
        player.OnAttackFinished();
    }

    private void OnDestroy()
    {
        player.OnStartMoving -= OnStartMoving;
        player.OnStopMoving -= OnStopMoving;
        player.OnAttackStarted -= OnAttack;
        player.OnAttackCancelled -= OnAttackCancelled;
    }
}
