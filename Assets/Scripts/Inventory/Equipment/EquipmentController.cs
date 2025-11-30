using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraAnchor;      // CenterEyeAnchor

    [Header("Where equipped items are attached")]
    [SerializeField] private Transform handAnchor;   // e.g. ControllerGrabInteractor transform or a child of it

    [Header("Controllers")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InventoryScreenController screenController;    

    private GameObject currentEquipped;

    private void Awake()
    {
        if (cameraAnchor == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: camera anchor");
        }

        if (inventoryController == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: inventory controller");
        }

        if (screenController == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: screen controller");
        }
    }

    public void Equip(ItemData item, int index)
    {
        Unequip();

        if (item == null || item.prefab == null)
        {
            Debug.LogError("Equip called with null item or prefab");
            return;
        }

        inventoryController.RemoveItem(index);
        //Debug.LogError("[EquipmentController] Inventory item removed");

        screenController.HideAll();
        //Debug.LogError("[EquipmentController] Screen hidden");

        Vector3 spawnPos = cameraAnchor.position + cameraAnchor.forward * 1.0f;
        Quaternion spawnRot = Quaternion.LookRotation(cameraAnchor.forward, Vector3.up);

        currentEquipped = Instantiate(item.prefab, spawnPos, spawnRot);
    }

    public void Unequip()
    {
        if (currentEquipped != null)
        {
            Destroy(currentEquipped);
            currentEquipped = null;
        }
    }
}
