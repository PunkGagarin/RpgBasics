public class EnemyStats : CharacterStats {

    public override void Die() {
        base.Die();

        //Add ragdoll effect / death animation

        Destroy(gameObject);

        //Add Loot here if needed
    }
}
