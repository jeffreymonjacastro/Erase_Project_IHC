using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsController : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the ItemDetailsPanel.")]
    public GameObject root;

    [Header("UI References")]
    public TMP_Text titleText;
    public Image descriptionImage;
    public TMP_Text descriptionText;

    private ItemData currentItem;

    private void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }

        Hide();
    }

    public void ShowItem(ItemData item)
    {
        currentItem = item;

        if (item == null)
        {
            Hide();
            return;
        }

        if (titleText != null)
        {
            titleText.text = string.IsNullOrEmpty(item.id) ? "Item" : item.id;
        }

        if (descriptionImage != null)
        {
            descriptionImage.sprite = item.descriptionImage != null
                ? item.descriptionImage
                : item.inventoryIcon;

            descriptionImage.enabled = descriptionImage.sprite != null;
        }

        if (descriptionText != null)
        {
            descriptionText.text = string.IsNullOrEmpty(item.descriptionText)
                ? "No description available."
                : item.descriptionText;
        }

        root.SetActive(true);
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
