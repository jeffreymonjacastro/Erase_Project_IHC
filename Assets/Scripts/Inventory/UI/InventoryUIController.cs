using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the InventoryPanel.")]
    [SerializeField] private GameObject root;

    [Header("UI References")]
    [SerializeField] private Transform slotContainer;     // the Grid transform
    [SerializeField] private GameObject slotPrefab;       // the Slot prefab

    [Header("Controllers")]
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InventoryScreenController screenController;

    private InventorySlot[] slotsUI;

    private void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }

        if (inventory == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: inventory");
        }

        if (slotContainer == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: slot container");
        }

        if (slotPrefab == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: slot prefab");
        }

        if (screenController == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: screen controller");
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
