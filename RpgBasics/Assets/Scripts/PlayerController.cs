using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    Camera cam;
    public LayerMask movementMask;
    public Interactable focus;
    PlayerMotor motor;


    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                //Move to what we hit
                motor.MoveToPoint(hit.point);

                //Stop focusing any objects
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.collider.TryGetComponent(out Interactable interactable)) {
                    SetFocus(interactable);
                }
                //Check if interactable object
                //Focus ont it if it is
            }
        }
    }
    private void SetFocus(Interactable newFocus) {
        if (focus != newFocus) {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);

    }

    private void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
