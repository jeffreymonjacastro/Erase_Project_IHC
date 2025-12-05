using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class KeyUseHandler : ItemUseHandlerBase
{
    public override string GetLabel(ItemUseContext ctx)
    {
        ItemData item = ctx.itemData;
        DoorLock door = ctx.targetedDoorLock;

        if (door == null)
        {
            return string.Empty;
        }

        //Debug.Log($"!! keyId = {item.keyId}, requiredKeyId = {door.RequiredKeyId}");

        if (door.RequiredKeyId == item.keyId)
        {
            return door.IsLocked ? "Unlock" : "Lock";
        }

        return string.Empty;
    }

    public override bool CanUse(ItemUseContext ctx)
    {
        ItemData item = ctx.itemData;
        DoorLock door = ctx.targetedDoorLock;
        if (door == null)
            return false;

        return door.RequiredKeyId == item.keyId;
    }

    public override void Use(ItemUseContext ctx)
    {
        if (!CanUse(ctx))
            return;

        DoorLock door = ctx.targetedDoorLock;

        if (door.IsLocked)
        {
            door.Unlock();
        }
        else
        {
            door.Lock();
        }
    }
}
