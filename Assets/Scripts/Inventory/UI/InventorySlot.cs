using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image iconImage;

    private InventoryScreenController screen;

    private ItemData currentItem;
    private int slotIndex;
    
    public void Initialize(int index, InventoryScreenController screenController)
    {
        slotIndex = index;
        screen = screenController;
        Clear();
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (item == null)
        {
            Clear();
            return;
        }

        iconImage.sprite = item.inventoryIcon;
        iconImage.enabled = true;
    }

    public void Clear()
    {
        currentItem = null;
        iconImage.sprite = null;
        iconImage.enabled = false;
    }

    public void OnClick()
    {
        if (currentItem != null)
        {
            Debug.Log($"[InventorySlot] Clicked slot {slotIndex}, item: {currentItem.id}");

            if (screen != null)
            {
                screen.ShowDetails(currentItem);
            }
        }
        else
        {
            Debug.Log($"[InventorySlot] Clicked empty slot {slotIndex}");
        }
    }
}
