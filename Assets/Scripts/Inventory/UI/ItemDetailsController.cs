using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsController : MonoBehaviour
{
    [Header("Root")]
    [Tooltip("Root object to enable/disable. Usually the ItemDetailsPanel.")]
    [SerializeField] private GameObject root;

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
