using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Where equipped items are attached")]
    [SerializeField] private Transform rightHandAnchor;   // e.g. ControllerTouchHandGrabInteractor transform or a child of it

    private GameObject currentEquipped;

    public void Equip(ItemData item)
    {
        Unequip();

        if (item == null || item.prefab == null)
        {
            Debug.LogError("Equip called with null item or prefab");
            return;
        }

        // Instantiate as child of the right hand
        currentEquipped = Instantiate(item.prefab, rightHandAnchor);

        // Reset local transform so it sits at the anchor
        currentEquipped.transform.localPosition = Vector3.zero;
        currentEquipped.transform.localRotation = Quaternion.identity;

        // Optional: tweak localPosition/localRotation per item type if needed
    }

    public void Unequip()
    {
        if (currentEquipped != null)
        {
            Destroy(currentEquipped);
            currentEquipped = null;
        }
    }
}
