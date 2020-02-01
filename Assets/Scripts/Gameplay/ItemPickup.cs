using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public string itemName;

    [System.Serializable] public class PickUpItemEvent : UnityEvent<ItemPickup> { };
    public PickUpItemEvent onPickedUp;
    private bool pickedUp = false;

    public bool CanPickUp()
    {
        return !pickedUp;
    }

    public void PickUp()
    {
        pickedUp = true;
        FindObjectOfType<CollectedItemTracker>().CollectItem(itemName); // collect the item!
        onPickedUp.Invoke(this);
    }
}
