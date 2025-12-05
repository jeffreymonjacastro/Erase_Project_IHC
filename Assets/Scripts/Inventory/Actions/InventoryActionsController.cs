using Meta.WitAi.Composer;
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
    [SerializeField] private InventoryScreenController screen;
    [SerializeField] private InventoryFeedbackController inventoryFeedback;
    [SerializeField] private SensorFeedbackController sensorFeedback;

    [Header("Usage")]
    [SerializeField] private ItemUseHandlerRegistry handlerRegistry;

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

        if (screen == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: screen controller");
        }

        if (inventoryFeedback == null)
        {
            Debug.LogWarning("[InventoryActionsController] Missing reference: inventoryFeedback controller");
        }

        if (sensorFeedback == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: sensorFeedback controller");
        }

        if (handlerRegistry == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: use handler registry");
        }
    }

    public string GetActionLabelFor(ItemData item)
    {
        if (item == null) return string.Empty;

        if (item.type == ItemType.Generic)
        {
            return "Summon";
        }

        var handler = handlerRegistry.GetHandlerFor(item);
        if (handler == null)
        {
            Debug.LogWarning($"[InventoryActionsController] Missing handler for {item.id}");
            return string.Empty;
        }

        if (item.type == ItemType.Key)
            UpdateTarget();

        return handler.GetLabel(BuildUseContext());
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
        inventoryFeedback?.PlayItemEquippedFeedback();
        screen.HideAll();
    }

    private void HandleUse(ItemData item, int index)
    {
        var ctx = BuildUseContext();

        var handler = handlerRegistry.GetHandlerFor(item);

        if (!handler.CanUse(ctx))
        {
            inventoryFeedback?.PlayInvalidActionFeedback();
            return;
        }

        handler.Use(ctx);

        if (item.RemoveFromInventoryOnUse)
        {
            inventory.RemoveItem(index);
        }

        inventoryFeedback?.PlayItemEquippedFeedback();
    }

    private ItemUseContext BuildUseContext()
    {
        return new ItemUseContext
        {
            playerRoot = playerRoot,
            playerCamera = playerCamera,
            equipment = equipment,
            screen = screen,
            targetedDoorLock = _currentTargetDoor,
            sensorFeedback = sensorFeedback
        };
    }
}
