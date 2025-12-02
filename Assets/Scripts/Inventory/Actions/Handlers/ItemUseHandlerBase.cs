using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class ItemUseHandlerBase : ScriptableObject
{
    [Header("Item")]
    [SerializeField] protected ItemData item;

    protected virtual void Awake()
    {
        if (item == null)
        {
            Debug.LogError("[ItemUseHandlerBase] Missing reference: item data");
        }
    }

    /// <summary>
    /// Label to display on the Use button ("Wear", "Take off", "Unlock", etc.).
    /// Return null or empty string to hide / disable the button.
    /// </summary>
    public virtual string GetLabel(ItemUseContext ctx)
    {
        return string.Empty;
    }

    /// <summary>
    /// Whether the item can be used in the current context.
    /// </summary>
    public abstract bool CanUse(ItemUseContext context);

    /// <summary>
    /// Actual use behavior.
    /// </summary>
    public abstract void Use(ItemUseContext context);
}