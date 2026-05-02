using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public float respawnTime = 5f;

    private Collider col;
    private Renderer rend;


    // gets the collider and renderer for the items
    private void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv != null) //if theres nothing in the inventory, pick up a new item and then respawn it 
            {
                inv.PickUp(item);
                StartCoroutine(Respawn());
            }
        }
    }

    // respawns the stuff after pickup. i tried disabling item but decided to just hide the colliders and renders for ease cuz it broke before
    private IEnumerator Respawn()
    {
        // hide da item
        col.enabled = false;
        rend.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        // show da item
        col.enabled = true;
        rend.enabled = true;
    }
}
    
