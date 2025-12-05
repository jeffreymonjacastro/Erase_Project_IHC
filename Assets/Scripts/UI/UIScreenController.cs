using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{

    [Header("Inventory Screens")]
    [SerializeField] private InventoryScreenController objectInventoryScreen;
    [SerializeField] private InventoryScreenController clueInventoryScreen;

    [Header("VR")]
    [Tooltip("Optional: assign to enable a laser pointer when UI is open.")]
    [SerializeField] private SimpleLaserPointer laserPointer;

    [Header("Feedback")]
    [SerializeField] private InventoryFeedbackController feedback;


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

        if (laserPointer == null)
        {
            Debug.LogWarning("[UIScreenController] Missing reference: laser pointer");
        }

        if (feedback == null)
        {
            Debug.LogWarning("[UIScreenController] Missing reference: feedback controller");
        }
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (clueInventoryScreen.IsOpen)
            {
                clueInventoryScreen.HideAll();
                clueInventoryScreen.SetIsOpen(false);
            }

            UpdateInventoryScreen(objectInventoryScreen);
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.B))
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
            SetLaserActive(false);
        }
        else
        {
            inventoryScreen.Show();
            SetLaserActive(true);
        }
        inventoryScreen.SetIsOpen(!inventoryScreen.IsOpen);

        feedback?.PlayInventoryToggleFeedback();
    }

    private void SetLaserActive(bool active)
    {
        if (laserPointer != null)
        {
            laserPointer.SetActive(active);
        }
    }
}
