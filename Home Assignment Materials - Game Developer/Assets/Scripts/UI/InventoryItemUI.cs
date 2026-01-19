using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform dragRoot;
    [SerializeField] private Transform gridTransform;
    public WeaponData WeaponData { get; private set; }

    public void Initialize(WeaponData weapon)
    {
        WeaponData = weapon;
        icon.sprite = weapon.weaponIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(dragRoot, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LoadoutSlotUI slot =
            eventData.pointerCurrentRaycast.gameObject?
            .GetComponentInParent<LoadoutSlotUI>();

        if (slot != null)
        {
            WeaponManager.Instance.loadout.Equip(slot.slotIndex, WeaponData);
        }
        else
        {
            int slotIndex = WeaponManager.Instance.loadout.FindWeaponSlot(WeaponData);
            if (slotIndex != -1)
                WeaponManager.Instance.loadout.Unequip(slotIndex);
            transform.SetParent(gridTransform);
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void ResetRect()
    {
        RectTransform rt = (RectTransform)transform;
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
    }

}
