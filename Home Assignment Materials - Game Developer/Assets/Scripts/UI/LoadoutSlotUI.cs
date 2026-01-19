using TMPro;
using UnityEngine;

public class LoadoutSlotUI : MonoBehaviour
{
    public int slotIndex;
    [SerializeField] private GameObject statsLayout;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI baseDamage;
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private TextMeshProUGUI speedModifier;

    private void OnEnable()
    {
        WeaponManager.Instance.loadout.OnSlotChanged += OnSlotChanged;
        UpdateStats(WeaponManager.Instance.loadout.slots[slotIndex]);

    }

    private void OnDisable()
    {
        if (WeaponManager.Instance != null)
            WeaponManager.Instance.loadout.OnSlotChanged -= OnSlotChanged;
    }

    private void OnSlotChanged(int changedSlot, WeaponData newWeapon, WeaponData oldWeapon)
    {
        if (changedSlot != slotIndex) return;
        UpdateStats(newWeapon);
        if (newWeapon == null)
        {
            return;
        }
        statsLayout.SetActive(true);
        
        InventoryItemUI item = ((LoadoutScreen)UIManager.Instance.GetScreen<LoadoutScreen>()).itemsUI[newWeapon];
        item.transform.SetParent(transform);
        item.ResetRect();
    }

    private void UpdateStats(WeaponData newWeapon)
    {
        if (newWeapon == null)
        {
            weaponName.text = "Equip a weapon!";
            statsLayout.SetActive(false);
            return;
        } 
        weaponName.text = newWeapon.weaponDisplayName;
        baseDamage.text = newWeapon.baseDamage.ToString();
        range.text = newWeapon.attackRange.ToString() + " m";
        speedModifier.text = newWeapon.movementSpeedModifier.ToString() + " m/s";
    }
}
