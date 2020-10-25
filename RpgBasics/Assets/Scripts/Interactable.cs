using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;               // How close do we need to be to interact?
    public Transform interractionTransform;  // The transform from where we interact in case you want to offset it

    bool isFocused = false;   // Is this interactable currently being focused?
    Transform player;       // Reference to the player transform

    bool isInterracted = false;	// Have we already interacted with the object?

    private void Update() {
        // If we are currently being focused
        // and we haven't already interacted with the object
        if (isFocused && !isInterracted) {
            // If we are close enough
            float distance = Vector3.Distance(player.position, interractionTransform.position);
            if (distance <= radius) {
                Interract();
                isInterracted = true;
            }
        }
    }

    public virtual void Interract() {
        //This method meant to be overriden
        //Debug.Log("Interracted with " + interractionTransform.name);
    }

    // Called when the object starts being focused
    public void OnFocused(Transform playerTransform) {
        isFocused = true;
        player = playerTransform;
        isInterracted = false;
    }

    // Called when the object is no longer focused
    public void OnDefocused() {
        isFocused = false;
        player = null;
        isInterracted = false;
    }

    private void OnDrawGizmosSelected() {
        if (interractionTransform == null)
            interractionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interractionTransform.position, radius);
    }
}
