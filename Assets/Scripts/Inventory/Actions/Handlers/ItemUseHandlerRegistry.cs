using UnityEngine;

public class ItemUseHandlerRegistry : MonoBehaviour
{
    [SerializeField] private MaskUseHandler maskHandler;
    [SerializeField] private KeyUseHandler keyHandler;

    public ItemUseHandlerBase GetHandlerFor(ItemData itemData)
    {
        if (itemData == null) return null;

        switch (itemData.type)
        {
            case ItemType.Mask:
                return maskHandler;
            case ItemType.Key:
                return keyHandler;
            default:
                return null; // Generic items have no use handler
        }
    }
}