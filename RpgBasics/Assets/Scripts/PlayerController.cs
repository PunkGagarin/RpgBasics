using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor playerMotor;
    void Start() {
        cam = Camera.main;
        playerMotor = GetComponent<PlayerMotor>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                //Move to what we hit
                playerMotor.MoveToPoint(hit.point);

                //Stop focusing any objects (?)
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                //Check if interactable object
                //Focus ont it if it is
            }
        }
    }
}
