using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
//itemdata stuffs, just adds the items name and icon for the request ui
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}
