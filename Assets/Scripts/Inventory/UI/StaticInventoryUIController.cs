using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;

public class StaticInventoryUIController : InventoryUIBase
{
    [Header("Inventory")]
    [SerializeField] private StaticInventoryController inventory;

    private List<InventorySlot> slots = new();

    protected override void Awake()
    {
        base.Awake();

        if (inventory == null)
        {
            Debug.LogError("[StaticInventoryUIController] Missing reference: inventory");
        }
    }

    protected override void BuildSlots()
    {
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        var items = inventory.Items;

        Debug.Log($"[StaticInventoryUIController] The number of items in inventory is {items.Count}");

        for (int i = 0; i < items.Count; i++)
        {
            var slotObj = Instantiate(slotPrefab, slotContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Initialize(i, screen, items[i]);
            slots.Add(slot);
        }

        Debug.Log($"[StaticInventoryUIController] The final size of the slots list is {slots.Count}");
    }
}
