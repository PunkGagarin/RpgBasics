using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    private static EquipmentManager instance;

    public static EquipmentManager GetInstance { get { return instance; } }


    // Start is called before the first frame update
    void Awake() {
        if (instance == null) {
            instance = this;
        } else
            Debug.Log("EquipmentManager already has an instance!!!");
    }
    #endregion

    public Equipment[] currentEquipment;

    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start() {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        inventory = Inventory.GetInstance;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null) {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip (int slotItem) {
        Equipment oldItem = currentEquipment[slotItem];
        if(oldItem != null) {
            inventory.Add(oldItem);
            currentEquipment[slotItem] = null;

            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }
    }
}
