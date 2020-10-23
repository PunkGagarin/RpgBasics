using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interract() {
        base.Interract();

        PickUp();
    }

    private void PickUp() {
        Debug.Log("Picking up " + item.name);
        bool wasAdded = Inventory.GetInstance.Add(item);

        if (wasAdded)
            Destroy(gameObject, .3f);
    }
}
