using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StaticInventoryController : MonoBehaviour
{
    [SerializeField] private List<ItemData> items;

    public IReadOnlyList<ItemData> Items => items;
    private void Awake()
    {
        if (items.Count == 0)
        {
            Debug.LogWarning("[StaticInventoryController] Static inventory initialized with empty list");
        }
    }
}
