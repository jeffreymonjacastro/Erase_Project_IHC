using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public InventoryController inventory;
    public Transform slotContainer;     // the Grid transform
    public GameObject slotPrefab;       // the Slot prefab

    [Header("Details")]
    public ItemDetailsController detailsController;

    private InventorySlot[] slotsUI;

    private void Start()
    {
        BuildSlotsUI();
        RefreshAll();
    }

    private void Update()
    {
        RefreshAll();
    }

    private void BuildSlotsUI()
    {
        int capacity = inventory.Capacity;
        slotsUI = new InventorySlot[capacity];

        for (int i = 0; i < capacity; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Initialize(i, detailsController);

            slotsUI[i] = slot;
        }
    }

    public void RefreshAll()
    {
        var items = inventory.Slots;

        for (int i = 0; i < items.Count; i++)
        {
            slotsUI[i].SetItem(items[i]);
        }
    }
}
