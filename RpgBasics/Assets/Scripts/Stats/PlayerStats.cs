using UnityEngine;

public class PlayerStats : CharacterStats {
    void Start() {
        EquipmentManager.GetInstance.onEquipmentChanged += OnEquipmentChange;
    }

    private void OnEquipmentChange(Equipment newItem, Equipment oldItem) {
        if (newItem != null) {
            armor.AddModifier(newItem.armorModifier);
            attack.AddModifier(newItem.attackModifier);
        }
        if (oldItem != null) {
            armor.AddModifier(oldItem.armorModifier);
            attack.AddModifier(oldItem.attackModifier);
        }
    }

    public override void Die() {
        base.Die();
        //Kill the player
        PlayerManager.GetInstance.KillPlayer();
    }
}
