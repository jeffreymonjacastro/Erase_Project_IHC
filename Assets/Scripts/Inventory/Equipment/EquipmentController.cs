using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraAnchor;      // CenterEyeAnchor

    [Header("Controllers")]
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InventoryScreenController screen;

    private void Awake()
    {
        if (cameraAnchor == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: camera anchor");
        }

        if (inventory == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: inventory controller");
        }

        if (screen == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: screen controller");
        }
    }

    public void Equip(ItemData item, int index)
    {
        if (item == null || item.prefab == null)
        {
            Debug.LogError("Equip called with null item or prefab");
            return;
        }

        inventory.RemoveItem(index);
        //Debug.LogError("[EquipmentController] Inventory item removed");

        screen.HideAll();
        //Debug.LogError("[EquipmentController] Screen hidden");

        Vector3 spawnPos = cameraAnchor.position + cameraAnchor.forward * 1.0f;
        Quaternion spawnRot = Quaternion.LookRotation(cameraAnchor.forward, Vector3.up);

        Instantiate(item.prefab, spawnPos, spawnRot);
    }
}
