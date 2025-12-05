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
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button actionButton;
    [SerializeField] private TMP_Text actionButtonText;

    [Header("Controllers")]
    [SerializeField] private InventoryActionsController actions;

    private ItemData currentItem;
    private int currentItemIndex;

    private bool active = false;

    private void Awake()
    {
        // If root is not assigned, assume this GameObject is the root.
        if (root == null)
        {
            root = gameObject;
        }

        if (actions == null)
        {
            Debug.LogError("[ItemDetailsController] Missing reference: inventory actions controller");
        }

        Hide();
    }

    private void Update()
    {
        if (!active) 
            return;

        if (currentItem == null)
        {
            SetActionButtonLabel(string.Empty);
            return;
        }

        string label = actions.GetActionLabelFor(currentItem);
        SetActionButtonLabel(label);
    }

    public void SetActionButtonLabel(string label)
    {
        if (actionButton == null || actionButtonText == null) return;
        
        bool active = !string.IsNullOrEmpty(label);

        actionButtonText.text = label ?? string.Empty;

        actionButton.interactable = active;
    }

    public void OnActionButtonPressed()
    {
        if (currentItem == null || currentItemIndex < 0) return;

        actions.HandleItemAction(currentItem, currentItemIndex);

        if (currentItem.RemoveFromInventoryOnUse)
        {
            UpdateCurrentItem(null);
            Hide();
        }
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

    public void ShowItem(ItemData item, int index)
    {
        UpdateCurrentItem(item);
        currentItemIndex = index;

        if (item == null)
        {
            Hide();
            return;
        }

        ShowData();
    }

    private void UpdateCurrentItem(ItemData item)
    {
        currentItem = item;

    }

    public void ShowData()
    {
        active = true;

        if (titleText != null)
        {
            titleText.text = string.IsNullOrEmpty(currentItem.titleText) ? currentItem.id : currentItem.titleText;
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
        if (root != null)
        {
            active = false;
            root.SetActive(false);
        }
    }
}
