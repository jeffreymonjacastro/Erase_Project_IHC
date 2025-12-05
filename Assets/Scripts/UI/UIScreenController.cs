using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UIScreenController : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private PauseMenuController pauseMenu;

    [Header("Inventory Screens")]
    [SerializeField] private InventoryScreenController objectInventoryScreen;
    [SerializeField] private InventoryScreenController clueInventoryScreen;

    [Header("VR")]
    [Tooltip("Optional: assign to enable a laser pointer when UI is open.")]
    [SerializeField] private SimpleLaserPointer laserPointer;

    [Header("Feedback")]
    [SerializeField] private InventoryFeedbackController feedback;

    private int laserToggleCount = 0;


    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("[UIScreenController] Missing reference: pause menu");
        }

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
        // Left controller "tool menu" / Start button
        if (OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch))
        {
            if (pauseMenu.IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        else if (pauseMenu.IsPaused)
        {
            return;
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (clueInventoryScreen.IsOpen)
            {
                clueInventoryScreen.HideAll();
                clueInventoryScreen.SetIsOpen(false);
                SetLaserActive(false);
            }

            UpdateInventoryScreen(objectInventoryScreen);
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            if (objectInventoryScreen.IsOpen)
            {
                objectInventoryScreen.HideAll();
                objectInventoryScreen.SetIsOpen(false);
                SetLaserActive(false);
            }
            UpdateInventoryScreen(clueInventoryScreen);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.ResumeGame();
        SetLaserActive(false);
    }

    public void PauseGame()
    {
        pauseMenu.PauseGame();
        SetLaserActive(true);
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
            if (active)
            {
                laserToggleCount++;
            }
            else
            {
                laserToggleCount--;
            }
            
            if (laserToggleCount == 1)
            {
                laserPointer.SetActive(true);
            }
            else if (laserToggleCount == 0)
            {
                laserPointer.SetActive(false);
            }
        }
    }
}
