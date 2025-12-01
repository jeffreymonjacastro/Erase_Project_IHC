using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryActionsController : MonoBehaviour
{
    [Header("World")]
    [SerializeField] private Transform playerRoot;
    [SerializeField] private Transform rightHand;

    [Header("Controllers")]
    [SerializeField] private InventoryController inventory;
    [SerializeField] private EquipmentController equipment;
    [SerializeField] private InventoryScreenController screen;
    [SerializeField] private InventoryFeedbackController feedback;

    private void Awake()
    {
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

        if (feedback == null)
        {
            Debug.LogError("[InventoryActionsController] Missing reference: feedback controller");
        }
    }

    public void HandleItemAction(ItemData item, int index)
    {
        if (item.IsConsumable && item.UseHandler != null)
        {
            HandleUse(item, index);
        }
        else
        {
            HandleEquip(item, index);
        }
    }

    private void HandleEquip(ItemData item, int index)
    {
        equipment.Equip(item, index);
        inventory.RemoveItem(index);
        feedback?.PlayItemEquippedFeedback();
        screen.HideAll();
    }

    private void HandleUse(ItemData item, int index)
    {
        var ctx = new ItemUseContext
        {
            player = playerRoot,
            rightHand = rightHand,
            item = item,
            inventory = inventory,
            equipment = equipment,
            screen = screen
        };

        if (!item.UseHandler.CanUse(ctx))
        {
            // error feedback if you want
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
}
