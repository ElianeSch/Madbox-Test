using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutScreen : UIScreen
{
    [SerializeField] private InventoryItemUI itemPrefab;
    [SerializeField] private Transform grid;
    public Dictionary<WeaponData, InventoryItemUI> itemsUI = new Dictionary<WeaponData, InventoryItemUI>();

    private void Start()
    {
        CreateItems();
        GameManager.OnGameStateChanged += OnGameStateChanged;
        WeaponManager.Instance.loadout.OnSlotChanged += RefreshGrid;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Loadout)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void CreateItems()
    {
        foreach (var weapon in WeaponManager.Instance.allWeapons)
        {
            var item = Instantiate(itemPrefab, grid);
            item.Initialize(weapon);
            item.gameObject.SetActive(true);
            itemsUI.Add(weapon, item);
        }
    }

    private void RefreshGrid(int slotIndex, WeaponData newWeapon, WeaponData oldWeapon)
    {
        foreach (var pair in itemsUI)
        {
            if (WeaponManager.Instance.loadout.FindWeaponSlot(pair.Key) == -1)
            {
                pair.Value.transform.SetParent(grid);
                pair.Value.ResetRect();
            }
        }
    }

    public override void OnShow()
    {

    }

    public override void OnHide()
    {

    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
}
