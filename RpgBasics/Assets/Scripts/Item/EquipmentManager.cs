using System;
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

    public SkinnedMeshRenderer targetMesh;
    public Equipment[] defaultItems;
    public Equipment[] currentEquipment;
    public SkinnedMeshRenderer[] currentMeshes;

    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start() {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefaultItems();

        inventory = Inventory.GetInstance;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnequipAll();
        }
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        SetupNewMesh(newMesh);
        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotItem) {
        if (currentEquipment[slotItem] != null) {

            if (currentMeshes[slotItem] != null) {
                Destroy(currentMeshes[slotItem].gameObject);
            }
            Equipment oldItem = currentEquipment[slotItem];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            currentEquipment[slotItem] = null;

            //Equipment has been removed so we trigger callback
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    private void SetupNewMesh(SkinnedMeshRenderer newMesh) {
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
    }

    public void UnequipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    private void EquipDefaultItems() {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }

    void SetEquipmentBlendShapes(Equipment item, int weight) {
        foreach (EquipmentMeshRegion meshRegion in item.coveredMeshRegions) {
            targetMesh.SetBlendShapeWeight((int)meshRegion, weight);
        }
    }
}
