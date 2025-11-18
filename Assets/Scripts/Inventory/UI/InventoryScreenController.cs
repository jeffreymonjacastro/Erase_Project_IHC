using UnityEngine;

public class InventoryScreenController : MonoBehaviour
{
    public InventoryUIController inventory;
    public ItemDetailsController details;


    public void ShowInventory()
    {
        if (inventory != null && details != null)
        {
            details.Hide();
            inventory.Show();
        }
    }
    public void ShowDetails(ItemData item)
    {
        if (inventory != null && details != null)
        {
            inventory.Hide();
            details.ShowItem(item);
        }
    }
}
