using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    private ItemPickup lastItem = null;

    private void OnTriggerEnter(Collider other)
    {
        ItemPickup item = other.gameObject.GetComponent<ItemPickup>();
        //print("Collided with " + other.gameObject.name);
        if (item != null && item != lastItem)
        {
            if (item.CanPickUp())
            {
                // then try talking with it!
                item.PickUp();
                lastItem = item;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemPickup item = other.gameObject.GetComponentInChildren<ItemPickup>();
        //print("Left collision with " + other.gameObject.name);
        if (item == lastItem)
        {
            lastItem = null; // reset the node so we can talk to it again
        }
    }
}
