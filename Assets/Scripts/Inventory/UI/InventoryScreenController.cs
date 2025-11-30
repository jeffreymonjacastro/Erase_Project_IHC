using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryScreenController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private InventoryUIBase inventory;
    [SerializeField] private ItemDetailsController details;

    [Header("VR")]
    [Tooltip("Optional: assign to enable a laser pointer when UI is open.")]
    [SerializeField] private SimpleLaserPointer laserPointer;

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
        SetLaserActive(true);
    }

    public void HideAll()
    {
        inventory.Hide();
        details.Hide();
        SetLaserActive(false);
    }

    private void SetLaserActive(bool active)
    {
        if (laserPointer != null)
        {
            laserPointer.SetActive(active);
        }
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
