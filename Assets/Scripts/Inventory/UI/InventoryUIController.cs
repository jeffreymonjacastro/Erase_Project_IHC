using UnityEngine;

public class InventoryUIController : InventoryUIBase
{
    [Header("Inventory")]
    [SerializeField] private InventoryController inventory;

    private InventorySlot[] slots;

    protected override void Awake()
    {
        base.Awake();

        if (inventory == null)
        {
            Debug.LogError("[InventoryUIController] Missing reference: inventory");
        }
    }

    protected override void Start()
    {
        base.Start();
        RefreshAll();
    }

    private void Update()
    {
        RefreshAll();
    }

    protected override void BuildSlots()
    {
        int capacity = inventory.Capacity;
        slots = new InventorySlot[capacity];

        for (int i = 0; i < capacity; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Initialize(i, screenController);

            slots[i] = slot;
        }
    }

    public void RefreshAll()
    {
        var items = inventory.Slots;

        for (int i = 0; i < items.Count; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }
}
