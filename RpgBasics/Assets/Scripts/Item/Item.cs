using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    //new public string name = "Example Item";
    public Sprite icon = null;  
    public bool isDefault = false;

    public virtual void UseItem() {
        //UseItem smth might happen
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory() {
        Inventory.GetInstance.Remove(this);
    }
}