using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int space = 10;

    public List<Item> items = new List<Item>();


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private static Inventory instance;

    public static Inventory GetInstance { get { return instance; } }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Debug.Log("Inventory already have instance!!!");
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
