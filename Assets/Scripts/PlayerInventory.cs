using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData heldItem;

    public void PickUp(ItemData item)
    {
        // youll never guess what this does
        heldItem = item;
        Debug.Log("Picked up " + item.itemName);
    }

    public void ClearItem()
    {
        // youll also NEVER guess what this does
        heldItem = null;
    }
}
