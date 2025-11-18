using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int capacity = 12;

    [Tooltip("Slots will be auto-sized to Capacity in Awake.")]
    [SerializeField] private ItemData[] slots;

    public int Capacity => capacity;
    public IReadOnlyList<ItemData> Slots => slots;

    private void Awake()
    {
        if (slots == null || slots.Length != capacity)
        {
            slots = new ItemData[capacity];
        }
    }

    /// <summary>
    /// Adds the item in the first free slot. Returns true if successful.
    /// </summary>
    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("[Inventory] Tried to add null ItemData.");
            return false;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                Debug.Log($"[Inventory] Added '{item.id}' at slot {i}.");
                return true;
            }
        }

        Debug.Log("[Inventory] Inventory full, cannot add item.");
        return false;
    }

    /// <summary>
    /// Removes the item at the given index (if any).
    /// </summary>
    public void RemoveItem(int index)
    {
        if (!IsValidIndex(index))
        {
            Debug.LogWarning($"[Inventory] RemoveItem: invalid index {index}.");
            return;
        }

        if (slots[index] != null)
        {
            Debug.Log($"[Inventory] Removed '{slots[index].id}' from slot {index}.");
        }

        slots[index] = null;
    }

    /// <summary>
    /// Returns the item at the given index, or null if empty/invalid.
    /// </summary>
    public ItemData GetItem(int index)
    {
        if (!IsValidIndex(index)) return null;
        return slots[index];
    }

    /// <summary>
    /// Clears all slots.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
        }

        Debug.Log("[Inventory] Cleared all slots.");
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
