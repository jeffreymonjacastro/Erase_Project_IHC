using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MaskUseHandler : ItemUseHandlerBase
{
    [Header("Dependencies")]
    public EquipmentController equipment;

    protected override void Awake()
    {
        base.Awake();

        if (equipment == null)
        {
            Debug.LogError("[MaskUseHandler] Missing reference: equipment controller");
        }
    }

    public override string GetLabel(ItemUseContext ctx)
    {
        return equipment.HasGasProtection ? "Take off" : "Wear";
    }

    public override bool CanUse(ItemUseContext ctx)
    {
        return true;
    }

    public override void Use(ItemUseContext ctx)
    {
        if (equipment.HasGasProtection)
        {
            equipment.UnequipItem(item);
        }
        else
        {
            equipment.EquipItem(item);
        }
    }
}
