using System;

public class Loadout
{
    public WeaponData[] slots = new WeaponData[3];
    // slotIndex, newWeapon, oldWeapon
    public event Action<int, WeaponData, WeaponData> OnSlotChanged;
    public WeaponData GetWeapon(int index) => slots[index];

    public int FindWeaponSlot(WeaponData weapon)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] == weapon)
                return i;
        return -1;
    }

    public void Equip(int slotIndex, WeaponData weapon)
    {
        WeaponData oldWeapon = slots[slotIndex];
        if (oldWeapon == weapon) return;

        int otherSlot = FindWeaponSlot(weapon);

        if (otherSlot != -1)
        {
            slots[otherSlot] = oldWeapon;
            slots[slotIndex] = weapon;

            OnSlotChanged?.Invoke(slotIndex, weapon, oldWeapon);
            OnSlotChanged?.Invoke(otherSlot, oldWeapon, weapon);
        }
        else
        {
            slots[slotIndex] = weapon;
            OnSlotChanged?.Invoke(slotIndex, weapon, oldWeapon);
        }
    }

    public void Unequip(int slotIndex)
    {
        WeaponData oldWeapon = slots[slotIndex];
        if (oldWeapon == null) return;

        slots[slotIndex] = null;
        OnSlotChanged?.Invoke(slotIndex, null, oldWeapon);
    }

    public bool HasAtLeastOneWeaponEquipped()
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null)
                return true;
        return false;
    }
}
