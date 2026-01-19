using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AttackRangeVisual : MonoBehaviour
{
    private int segments = 64;
    [SerializeField] private WeaponHandler weaponHandler;
    private float radius = 5f;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;
    }

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        weaponHandler.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnWeaponEquipped(WeaponData data)
    {
        radius = data.attackRange;
        DrawCircle(radius);
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Game)
        {
            DrawCircle(radius);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    private void DrawCircle(float r)
    {
        lineRenderer.positionCount = segments;

        float angleStep = 2f * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * r;
            float z = Mathf.Sin(angle) * r;

            lineRenderer.SetPosition(i, new Vector3(x, 0.1f, z));
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
        weaponHandler.OnWeaponEquipped -= OnWeaponEquipped;
    }
}
