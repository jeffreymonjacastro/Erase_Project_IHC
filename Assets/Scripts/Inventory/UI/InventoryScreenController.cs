using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryScreenController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private InventoryUIBase inventory;
    [SerializeField] private ItemDetailsController details;

    private bool showInventory;
    private bool isOpen;
    public bool IsOpen => isOpen;


    private void Awake()
    {
        if (inventory == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: inventory");
        }

        if (details == null)
        {
            Debug.LogError("[InventoryScreenController] Missing reference: details");
        }

        isOpen = false;
        showInventory = true;
        HideAll();
    }

    public void SetIsOpen(bool value) { isOpen = value; }

    public void Show()
    {
        if (showInventory || !ShowDetails())
        {
            ShowInventory();
        }
        else
        {
            ShowDetails();
        }
    }

    public void HideAll()
    {
        inventory.Hide();
        details.Hide();
    }

    public void ShowInventory()
    {
        showInventory = true;
        details.Hide();
        inventory.Show();
    }
    public bool ShowDetails()
    {
        inventory.Hide();
        return details.ShowItem();
    }

    public void ShowDetails(ItemData item, int index)
    {
        showInventory = false;
        inventory.Hide();
        details.ShowItem(item, index);
    }
}
