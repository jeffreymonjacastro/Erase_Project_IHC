using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data", fileName = "NewItemData")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    public string id; // e.g. "mask", "newspaper_clip_1"

    [Header("Inventory UI")]
    public Sprite inventoryIcon;
    public Sprite descriptionImage;
    [TextArea]
    public string titleText;
    [TextArea]
    public string descriptionText;

    [Header("World Representation")]
    public GameObject prefab;

    [Header("Type & Behavior")]
    public ItemType type = ItemType.Generic;

    [Tooltip("Only used when ItemType == Key")]
    public string keyId;

    [Tooltip("If true, equiping item grants protection from gas")]
    public bool grantsGasProtection = false;

    [Header("Usage")]
    [SerializeField] private bool inWorld = true;
    [SerializeField] private bool isUsable = false;
    [SerializeField] private bool removeFromInventoryOnUse = true;
    public bool IsUsable => isUsable;
    public bool RemoveFromInventoryOnUse => removeFromInventoryOnUse;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
            Debug.LogError($"[ItemData '{name}']: Missing id!");

        if (type == ItemType.Key && string.IsNullOrEmpty(keyId))
            Debug.LogError($"[ItemData '{name}']: Missing key id!");

        if (inWorld && prefab == null)
            Debug.LogError($"[ItemData '{name}']: Missing prefab!");
    }
#endif 
}