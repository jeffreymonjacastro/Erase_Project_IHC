using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int capacity = 15;

    [Header("Feedback")]
    [SerializeField] private InventoryFeedbackController feedback;

    [Tooltip("Slots will be auto-sized to Capacity in Awake.")]
    private ItemData[] slots;

    public int Capacity => capacity;
    public IReadOnlyList<ItemData> Slots => slots;

    private void Awake()
    {
        if (slots == null || slots.Length != capacity)
        {
            slots = new ItemData[capacity];
        }


        if (feedback == null)
        {
            Debug.LogWarning("[EquipmentController] Missing reference: feedback controller");
        }
    }

    /// <summary>
    /// Adds the item in the first free slot. Returns true if successful.
    /// </summary>
    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("[Inventory] Tried to add null ItemData.");
            return false;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                Debug.Log($"[Inventory] Added '{item.id}' at slot {i}.");

                feedback?.PlayItemStoredFeedback();

                return true;
            }
        }

        Debug.LogWarning("[Inventory] Inventory full, cannot add item.");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (!IsValidIndex(index))
        {
            Debug.LogError($"[Inventory] RemoveItem: invalid index {index}.");
            return;
        }

        if (slots[index] != null)
        {
            Debug.Log($"[Inventory] Removed '{slots[index].id}' from slot {index}.");
        }

        slots[index] = null;
    }

    public ItemData GetItem(int index)
    {
        if (!IsValidIndex(index)) return null;
        return slots[index];
    }

    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
        }
    }

    public bool HasFreeSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) return true;
        }
        return false;
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < slots.Length;
    }

    /// <summary>
    /// Returns a human-readable representation of the inventory contents.
    /// Helpful for debugging/logging.
    /// </summary>
    public string GetDebugContents()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Inventory contents:");

        for (int i = 0; i < slots.Length; i++)
        {
            string slotText = slots[i] != null ? slots[i].id : "(empty)";
            sb.AppendLine($"  [{i}] {slotText}");
        }

        return sb.ToString();
    }
}
