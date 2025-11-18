using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image iconImage;

    private ItemDetailsController detailsController;

    private ItemData currentItem;
    private int slotIndex;
    
    public void Initialize(int index, ItemDetailsController details)
    {
        slotIndex = index;
        detailsController = details;
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

            if (detailsController != null)
            {
                detailsController.ShowItem(currentItem);
            }
        }
        else
        {
            Debug.Log($"[InventorySlot] Clicked empty slot {slotIndex}");

            if (detailsController != null)
            {
                detailsController.Hide();
            }
        }
    }
}
