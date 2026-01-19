using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : Manager<PlayerInputManager>
{
    [SerializeField] private Player player;
    [SerializeField] private Joystick joystick;
    private void OnEnable()
    {
        joystick.OnDirectionChanged += HandleDirection;
    }

    private void OnDisable()
    {
        joystick.OnDirectionChanged -= HandleDirection;
    }

    private void HandleDirection(Vector2 direction)
    {
        player.SetDirection(direction);
    }

    public void SwitchPlayerWeapon()
    {
        player.WeaponHandler.SwitchWeapons();
    }
}
