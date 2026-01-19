using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Swing,
    Rotate
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponID;
    public string weaponDisplayName;
    public Sprite weaponIcon;
    public GameObject weaponPrefab;
    public float attackRange = 1f;
    public float movementSpeedModifier = 1f;
    public int baseDamage;
    public Vector3 localPosition;
    public Quaternion localRotation;
    [Tooltip("Will multiply the animation duration")]
    public float attackSpeed = 1f;
    public AttackType attackType = AttackType.Swing;
}
