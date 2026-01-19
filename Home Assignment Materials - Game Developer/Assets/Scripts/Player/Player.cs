using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private TargetProvider targetProvider;
    private Loadout loadout;
    [HideInInspector]
    public Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    public Action OnStartMoving;
    public Action OnStopMoving;
    public WeaponHandler WeaponHandler => weaponHandler;
    private Vector3 initialPlayerPosition;
    private Quaternion initialPlayerRotation;
    private float speedModifier = 1f;
    public event Action OnAttackStarted;
    public event Action OnAttackCancelled;
    private bool canMove;

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        LevelManager.Instance.OnLevelChanged += OnLevelChanged;
        MagnetLootBehaviour.OnLootPickedUp += OnLootPickedUp;
        GameManager.Instance.OnLevelCleared += OnLevelCleared;
        loadout = WeaponManager.Instance.loadout;
        weaponHandler.SetLoadout(loadout);
        weaponHandler.OnWeaponEquipped += OnWeaponEquipped;
        initialPlayerPosition = transform.position;
        initialPlayerRotation = transform.rotation;
    }

    private void OnLevelCleared()
    {
        canMove = false;
    }

    private void OnLevelChanged(int currentLevelIndex)
    {
        transform.position = LevelManager.Instance.CurrentLevel.playerSpawnPoint.position;
        canMove = true;
    }

    private void OnWeaponEquipped(WeaponData data)
    {
        speedModifier = data.movementSpeedModifier;
        targetProvider.SetRange(data.attackRange);
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Game)
        {
            weaponHandler.EquipFirstWeapon();
        }
        else
        {
            Reset();
        }
    }

    private void Update()
    {
        if (canMove == false) return;
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y);
        transform.position += move * baseMoveSpeed * speedModifier * Time.deltaTime;
        if (move.sqrMagnitude > 0.000001f)
        {
            transform.forward = move;
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up) * Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = targetRotation;
            if (lastMoveDirection.sqrMagnitude <= 0.000001f)
            {
                weaponHandler.StopAttack();
                targetProvider.SetCurrentTarget(null);
                OnAttackCancelled?.Invoke();
                OnStartMoving?.Invoke();
            }
        }
        else
        {
            if (lastMoveDirection.sqrMagnitude > 0.000001f)
            {
                OnStopMoving?.Invoke();
            }
            if (weaponHandler.CanAttack())
            {
                if (targetProvider.CurrentTarget == null)
                    targetProvider.GetClosestTarget();
                if (targetProvider.IsTargetAlive(targetProvider.CurrentTarget) == false)
                    targetProvider.GetClosestTarget();
                if (targetProvider.CurrentTarget != null)
                {
                    weaponHandler.Attack();
                    Vector3 attackDirection = targetProvider.CurrentTarget.position - transform.position;
                    attackDirection.y = 0f;
                    Quaternion targetRotation = Quaternion.LookRotation(attackDirection, Vector3.up) * Quaternion.Euler(0f, 180f, 0f);
                    transform.rotation = targetRotation;
                    OnAttackStarted?.Invoke();
                }
            }
        }
        lastMoveDirection = move;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void SetMoveSpeedModifier(float moveSpeedModifier)
    {
        speedModifier = moveSpeedModifier;
    }

    public void HitTarget()
    {
        weaponHandler.HitTarget(targetProvider.CurrentTarget);
    }

    public void OnAttackFinished()
    {
        weaponHandler.StopAttack();
    }

    private void OnLootPickedUp(MagnetLootBehaviour lootpickedUp)
    {
        if (lootpickedUp.GetComponent<Coin>() != null)
        {
            GameManager.Instance.AddGold(1);
        }
    }

    private void Reset()
    {
        transform.position = initialPlayerPosition;
        transform.rotation = initialPlayerRotation;
        weaponHandler.Reset();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        MagnetLootBehaviour.OnLootPickedUp -= OnLootPickedUp;
        if (weaponHandler != null)
            weaponHandler.OnWeaponEquipped -= OnWeaponEquipped;
    }
}