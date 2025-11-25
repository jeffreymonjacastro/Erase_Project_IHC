using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsController : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the ItemDetailsPanel.")]
    [SerializeField] private GameObject root;

    [Header("UI References")]
    public TMP_Text titleText;
    public Image descriptionImage;
    public TMP_Text descriptionText;

    [Header("Controllers")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private InventoryScreenController screenController;

    private ItemData currentItem;

    private void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }

        if (inventoryController == null)
        {
            Debug.LogError("[ItemDetailsController] Missing reference: inventory controller");
        }

        if (equipmentController == null)
        {
            Debug.LogError("[ItemDetailsController] Missing reference: equipment controller");
        }

        if (screenController == null)
        {
            Debug.LogError("[ItemDetailsController] Missing reference: screen controller");
        }

        Hide();
    }

    public void OnEquipPressed()
    {
        if (currentItem == null) return;

        equipmentController.Equip(currentItem);

        inventoryController.RemoveItem(currentItem);

        currentItem = null;
        ShowItem();

        screenController.HideAll();
    }
    
    public bool ShowItem() 
    {
        if (currentItem == null)
        {
            Hide();
            return false;
        }

        ShowData();

        return true;
    }

    public void ShowItem(ItemData item)
    {
        currentItem = item;

        if (item == null)
        {
            Hide();
            return;
        }

        ShowData();
    }

    public void ShowData()
    {
        if (titleText != null)
        {
            titleText.text = string.IsNullOrEmpty(currentItem.id) ? "Item" : currentItem.id;
        }

        if (descriptionImage != null)
        {
            descriptionImage.sprite = currentItem.descriptionImage != null
                ? currentItem.descriptionImage
                : currentItem.inventoryIcon;

            descriptionImage.enabled = descriptionImage.sprite != null;
        }

        if (descriptionText != null)
        {
            descriptionText.text = string.IsNullOrEmpty(currentItem.descriptionText)
                ? "No description available."
                : currentItem.descriptionText;
        }

        root.SetActive(true);
    }

    public void Close()
    {
        Hide();
    }

    public void Hide()
    {
        currentItem = null;
        if (root != null)
        {
            root.SetActive(false);
        }
    }
}
