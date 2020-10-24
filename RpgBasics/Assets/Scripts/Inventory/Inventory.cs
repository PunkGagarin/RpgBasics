using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int space = 10;

    public List<Item> items = new List<Item>();

    private static Inventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public static Inventory GetInstance { get { return instance; } }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Debug.Log("Inventory already has an instance!!!");
    }

    public bool Add(Item item) {
        if (!item.isDefault) {
            if (items.Count < space) {
                items.Add(item);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

                return true;
            }
            Debug.Log("No more space!");
        }
        return false;
    }

    public void Remove(Item item) {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
