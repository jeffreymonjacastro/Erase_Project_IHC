using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Anchors")]
    [Tooltip("Where the mask (or head item) should be visually attached")]
    [SerializeField] private Transform headAnchor;

    [Header("Feedback")]
    public GasFeedbackController gasFeedback;

    public bool HasGasProtection { get; private set; }

    private ItemData _currentHeadItem;

    private void Awake()
    {
        if (headAnchor == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: head anchor");
        }

        if (gasFeedback == null)
        {
            Debug.LogError("[EquipmentController] Missing reference: gas feedback");
        }

        HasGasProtection = false;
    }

    public void DropItem(ItemData item)
    {
        if (item == null || item.prefab == null)
        {
            Debug.LogError("[EquipmentController] DropItem called with null item or prefab");
            return;
        }

        Vector3 spawnPos = headAnchor.position + headAnchor.forward * 1.0f;
        Quaternion spawnRot = Quaternion.LookRotation(headAnchor.forward, Vector3.up);

        Instantiate(item.prefab, spawnPos, spawnRot);
    }

    public void EquipItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("[EquipmentController] EquipItem called with null item");
            return;
        }

        if (item.type == ItemType.Mask)
        {
            EquipHeadItem(item);
        }

        if (item.grantsGasProtection)
        {
            HasGasProtection = true; 
            
            if (item.type == ItemType.Mask)
                gasFeedback.SetMaskEquipped(true);
        }
    }

    public void UnequipItem(ItemData item)
    {
        if (item == null)
            return;

        if (_currentHeadItem == item)
        {
            UnequipHeadItem();
        }

        if (item.grantsGasProtection)
        {
            HasGasProtection = false;

            if (item.type == ItemType.Mask)
                gasFeedback.SetMaskEquipped(false);
        }
    }

    private void EquipHeadItem(ItemData item)
    {
        UnequipHeadItem();

        _currentHeadItem = item;
    }

    private void UnequipHeadItem()
    {
        _currentHeadItem = null;
    }
}
