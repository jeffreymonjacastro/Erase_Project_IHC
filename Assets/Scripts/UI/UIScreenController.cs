using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{

    [Header("Inventory Screens")]
    [SerializeField] private InventoryScreenController objectInventoryScreen;
    [SerializeField] private InventoryScreenController clueInventoryScreen;


    // Start is called before the first frame update
    void Start()
    {
        if (objectInventoryScreen == null)
        {
            Debug.LogError("[UIScreenController] Missing reference: object inventory screen");
        }

        if (clueInventoryScreen == null)
        {
            Debug.LogError("[UIScreenController] Missing reference: clue inventory screen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            if (clueInventoryScreen.IsOpen)
            {
                clueInventoryScreen.HideAll();
                clueInventoryScreen.SetIsOpen(false);
            }

            UpdateInventoryScreen(objectInventoryScreen);
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (objectInventoryScreen.IsOpen)
            {
                objectInventoryScreen.HideAll();
                objectInventoryScreen.SetIsOpen(false);
            }
            UpdateInventoryScreen(clueInventoryScreen);
        }
    }

    private void UpdateInventoryScreen(InventoryScreenController inventoryScreen)
    {
        if (inventoryScreen.IsOpen)
        {
            inventoryScreen.HideAll();
        }
        else
        {
            inventoryScreen.Show();
        }
        inventoryScreen.SetIsOpen(!inventoryScreen.IsOpen);
    }
}
