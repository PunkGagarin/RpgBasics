using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

    }

    public void MoveToPoint(Vector3 point) {
        agent.SetDestination(point);
    }
}
