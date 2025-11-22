using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryScreenController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private InventoryUIController inventory;
    [SerializeField] private ItemDetailsController details;

    [Header("VR")]
    [Tooltip("Optional: assign to enable a laser pointer when UI is open.")]
    [SerializeField] private SimpleLaserPointer laserPointer;

    private bool isOpen;
    private bool showInventory;

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

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            if (isOpen)
            {
                HideAll();
                SetLaserActive(false);
            }
            else
            {
                if (showInventory || !ShowDetails())
                {
                    showInventory = true;
                    ShowInventory();
                }
                else
                {
                    ShowDetails();
                }
                SetLaserActive(true);
            }
            isOpen = !isOpen;
        }
    }

    public void HideAll()
    {
        inventory.Hide();
        details.Hide();
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
        details.Hide();
        inventory.Show();
    }
    public bool ShowDetails()
    {
        inventory.Hide();
        return details.ShowItem();
    }

    public void ShowDetails(ItemData item)
    {
        inventory.Hide();
        details.ShowItem(item);
    }
}
