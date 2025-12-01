using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data", fileName = "NewItemData")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    public string id; // e.g. "mask", "newspaper_clip_1"

    [Header("World Representation")]
    public GameObject prefab; // 3D object to equip

    [Header("Inventory UI")]
    public Sprite inventoryIcon;     // small icon for grid
    public Sprite descriptionImage;  // optional larger image
    [TextArea]
    public string titleText;   // optional text description
    [TextArea]
    public string descriptionText;   // optional text description

    [Header("Usage")]
    [SerializeField] private bool isConsumable = false;
    [SerializeField] private bool removeFromInventoryOnUse = true;
    [SerializeField] private ItemUseHandlerBase useHandler;
    public bool IsConsumable => isConsumable;
    public bool RemoveFromInventoryOnUse => removeFromInventoryOnUse;
    public ItemUseHandlerBase UseHandler => useHandler;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
            Debug.LogError($"[ItemData '{name}']: Missing id!");

        if (prefab == null)
            Debug.LogError($"[ItemData '{name}']: Missing prefab!");

        if (isConsumable == true && useHandler == null)
            Debug.LogError($"[ItemData '{name}']: Missing use handler!");
    }

}