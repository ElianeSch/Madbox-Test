using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    [SerializeField] private Loadout loadout;
    [SerializeField] private int activeSlotIndex = 0;
    private GameObject currentWeaponObject;
    private WeaponData currentWeapon;
    private bool isAttacking;
    public event Action<WeaponData> OnWeaponEquipped;
    public WeaponData CurrentWeapon => currentWeapon;

    public void SwitchWeapons()
    {
        activeSlotIndex = (activeSlotIndex + 1) % loadout.slots.Length;
        while (loadout.GetWeapon(activeSlotIndex) == null)
            activeSlotIndex = (activeSlotIndex + 1) % loadout.slots.Length;
        UpdateWeapon();
    }

    public void EquipFirstWeapon()
    {
        while (loadout.GetWeapon(activeSlotIndex) == null)
            activeSlotIndex = (activeSlotIndex + 1) % loadout.slots.Length;
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        WeaponData weapon = loadout.GetWeapon(activeSlotIndex);

        if (weapon == currentWeapon)
            return;

        UnequipCurrentWeapon();

        currentWeapon = weapon;

        if (currentWeapon != null && currentWeapon.weaponPrefab != null)
        {
            currentWeaponObject = Instantiate(currentWeapon.weaponPrefab, weaponParent);
            currentWeaponObject.transform.localPosition = currentWeapon.localPosition;
            currentWeaponObject.transform.localRotation = currentWeapon.localRotation;
            //currentWeaponObject.transform.localScale = Vector3.one;
        }
        isAttacking = false;
        OnWeaponEquipped?.Invoke(currentWeapon);
    }

    public void UnequipCurrentWeapon()
    {
        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
            currentWeaponObject = null;
            currentWeapon = null;
        }
    }

    public bool CanAttack()
    {
        return currentWeapon != null && !isAttacking;
    }

    public void Attack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        StartCoroutine(StopAttackAfterFrame());
    }

    private IEnumerator StopAttackAfterFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    public void HitTarget(Transform target)
    {
        if (target == null) return;
        if (target.GetComponent<Health>() == null) return;
        target.GetComponent<Health>().TakeDamage(currentWeapon.baseDamage);
    }

    public void SetLoadout(Loadout newLoadout)
    {
        loadout = newLoadout;
        UpdateWeapon();
    }

    public void Reset()
    {
        UnequipCurrentWeapon();
        activeSlotIndex = 0;
    }
}
