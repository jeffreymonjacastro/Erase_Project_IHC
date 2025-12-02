using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryActionsController : MonoBehaviour
{
    [Header("World")]
    [SerializeField] private Transform playerRoot;
    [SerializeField] private Camera playerCamera;

    [Header("Interaction")]
    [SerializeField] private float interactRayDistance = 3f;
    [SerializeField] private LayerMask interactLayerMask;

    [Header("Controllers")]
    [SerializeField] private InventoryController inventory;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private ItemDetailsController details;
    [SerializeField] private InventoryScreenController screen;
    [SerializeField] private InventoryFeedbackController feedback;

    private ItemUseHandlerBase currentItemUseHandler => details.CurrentItemUseHandler;

    private DoorLock _currentTargetDoor;

    private void Awake()
    {
        if (playerRoot == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: player root");
        }

        if (playerCamera == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: player camera");
        }

        if (inventory == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: inventory controller");
        }

        if (equipment == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: equipment controller");
        }

        if (details == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: item details controller");
        }

        if (screen == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: screen controller");
        }

        if (feedback == null)
        {
            Debug.LogWarning("[InventoryActionsController] Missing reference: feedback controller");
        }
    }
    private void Update()
    {
        UpdateTarget();
        if (currentItemUseHandler != null)
        {
            var label = currentItemUseHandler.GetLabel(BuildUseContext());
            details.SetActionButtonLabel(label);
        }
    }

    private void UpdateTarget()
    {
        _currentTargetDoor = null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRayDistance, interactLayerMask))
        {
            _currentTargetDoor = hit.collider.GetComponentInParent<DoorLock>();
        }
    }

    public void HandleItemAction(ItemData item, int index)
    {
        if (item.IsUsable)
        {
            HandleUse(item, index);
        }
        else
        {
            HandleSummon(item, index);
        }
    }

    private void HandleSummon(ItemData item, int index)
    {
        equipment.DropItem(item);
        inventory.RemoveItem(index);
        feedback?.PlayItemEquippedFeedback();
        screen.HideAll();
    }

    private void HandleUse(ItemData item, int index)
    {
        var ctx = BuildUseContext();

        if (!item.UseHandler.CanUse(ctx))
        {
            feedback?.PlayInvalidActionFeedback();
            return;
        }

        item.UseHandler.Use(ctx);

        if (item.RemoveFromInventoryOnUse)
        {
            inventory.RemoveItem(index);
        }

        feedback?.PlayItemEquippedFeedback();
        screen.HideAll();
    }

    private ItemUseContext BuildUseContext()
    {
        return new ItemUseContext
        {
            playerRoot = playerRoot,
            playerCamera = playerCamera,
            equipment = equipment,
            screen = screen,
            targetedDoorLock = _currentTargetDoor
        };
    }
}
