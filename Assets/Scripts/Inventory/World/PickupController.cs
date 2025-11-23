using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class HandPickupController : MonoBehaviour
{
    [SerializeField] private InventoryController inventory;
    [SerializeField] private HandGrabInteractor grabInteractor;

    private void Awake()
    {
        if (inventory == null)
        {
            Debug.LogError("[PickupController] Missing reference: inventory");
        }

        if (grabInteractor == null)
        {
            Debug.LogError("[PickupController] Missing reference: grab interactor");
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            TryStoreCurrentItem();
        }
    }

    private void TryStoreCurrentItem()
    {
        Debug.Log("[PickupController] Trying to store item");
        GameObject grabbed = GetCurrentlyGrabbedObject();
        if (grabbed == null) return;

        Debug.Log("[PickupController] Got object");
        PickupItem pickup = grabbed.GetComponentInParent<PickupItem>();
        if (pickup == null) return;

        Debug.Log("[PickupController] Got pickup item");
        inventory.AddItem(pickup.ItemData);
        Debug.Log("[PickupController] Added to inventory");
        Destroy(pickup.gameObject);
        Debug.Log("[PickupController] Destroyed object");
    }

    private GameObject GetCurrentlyGrabbedObject()
    {
        // Depending on SDK version, this might be:
        // grabInteractor.SelectedInteractable
        // or grabInteractor.Interactable
        var interactable = grabInteractor.Interactable;
        return interactable != null ? interactable.transform.gameObject : null;
    }
}
