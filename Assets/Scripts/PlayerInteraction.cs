using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f; // how close u can be to interact

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // creates a ray where camera is facing
        
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("NPC"); //ray only detects npc layer (prevents u from touching the floor by accident)

    if (Physics.Raycast(ray, out hit, 100f, layerMask)) //ray travels for 100 units in game
    {
    Debug.Log("Hit: " + hit.collider.name); // checking if it hit the npc collider. this didnt work for ages lowk

    NPCRequest npc = hit.collider.GetComponentInParent<NPCRequest>();
    if (npc != null)
    {
        // if the player has an item, it tries to deliver it and clears the inventory
        PlayerInventory inv = GetComponent<PlayerInventory>();
        npc.TryDeliver(inv.heldItem);
        inv.ClearItem();
    }
}
            }
        }
    }

