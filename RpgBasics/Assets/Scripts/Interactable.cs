using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;

    bool isFocused = false;
    private bool isInterracted = false;


    Transform player;
    public Transform interractionTransform;

    private void Update() {
        if (isFocused && !isInterracted) {
            float distance = Vector3.Distance(player.position, interractionTransform.position);
            if (distance <= radius) {
                Interract();
                isInterracted = true;
            }
        }
    }

    public virtual void Interract() {
        //This method meant to be overriden
        Debug.Log("Interracted with " + interractionTransform.name);
    }

    public void OnFocused(Transform playerTransform) {
        isFocused = true;
        player = playerTransform;
        isInterracted = false;
    }

    public void OnDefocused() {
        isFocused = false;
        player = null;
        isInterracted = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interractionTransform.position, radius);
    }
}
