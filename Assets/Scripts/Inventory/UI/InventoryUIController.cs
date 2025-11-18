using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the InventoryPanel.")]
    public GameObject root;

    public InventoryController inventory;
    public Transform slotContainer;     // the Grid transform
    public GameObject slotPrefab;       // the Slot prefab
    
    public InventoryScreenController screenController;

    private InventorySlot[] slotsUI;

    private void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }
    }

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
            slot.Initialize(i, screenController);

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

    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
