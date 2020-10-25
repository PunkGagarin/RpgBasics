using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start() {
        playerManager = PlayerManager.GetInstance;
        myStats = GetComponent<CharacterStats>();
    }


    public override void Interract() {
        base.Interract();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if(playerCombat != null) {
            playerCombat.Attack(myStats);
        }
    }
}
