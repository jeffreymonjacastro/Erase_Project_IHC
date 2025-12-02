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
    private GameObject _currentHeadItemInstance;

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
            gasFeedback.SetMaskEquipped(false);
        }
    }

    private void EquipHeadItem(ItemData item)
    {
        UnequipHeadItem();

        if (item.prefab == null)
        {
            Debug.LogWarning($"[EquipmentController] EquipHeadItem: Item '{item.name}' has no prefab to equip.");
            return;
        }

        _currentHeadItem = item;
        _currentHeadItemInstance = Instantiate(item.prefab, headAnchor);
        _currentHeadItemInstance.transform.localPosition = Vector3.zero;
        _currentHeadItemInstance.transform.localRotation = Quaternion.identity;
        _currentHeadItemInstance.transform.localScale = Vector3.one;
    }

    private void UnequipHeadItem()
    {
        if (_currentHeadItemInstance != null)
        {
            Destroy(_currentHeadItemInstance);
            _currentHeadItemInstance = null;
        }

        _currentHeadItem = null;
    }
}
