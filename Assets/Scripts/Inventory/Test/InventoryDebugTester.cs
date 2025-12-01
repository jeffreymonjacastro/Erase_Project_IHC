using UnityEngine;

public class InventoryDebugTester : MonoBehaviour
{
    [Header("References")]
    public InventoryController inventory;

    [Header("Test Items")]
    public ItemData maskItem;
    public ItemData noteItem;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryController>();
        }

        LogInventory("Initial state");
    }

    private void Update()
    {
        // 1 = add mask
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (maskItem != null)
            {
                inventory.AddItem(maskItem);
                LogInventory("After AddItem(mask) [1]");
            }
            else
            {
                Debug.LogWarning("[InventoryTester] maskItem is not assigned.");
            }
        }

        // 2 = add note
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (noteItem != null)
            {
                inventory.AddItem(noteItem);
                LogInventory("After AddItem(note) [2]");
            }
            else
            {
                Debug.LogWarning("[InventoryTester] noteItem is not assigned.");
            }
        }

        // 3 = remove slot 0
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.RemoveItem(0);
            LogInventory("After RemoveItem(0) [3]");
        }

        // 4 = clear all
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.Clear();
            LogInventory("After Clear() [4]");
        }
    }

    private void LogInventory(string label)
    {
        if (inventory == null) return;

        string contents = inventory.GetDebugContents();
        Debug.Log($"[InventoryTester] {label}\n{contents}");
    }
}
