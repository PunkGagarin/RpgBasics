using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public float rotationSpeed = 5f;

    Transform target;
    NavMeshAgent navMeshAgent;

    void Start() {
        target = PlayerManager.GetInstance.player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        Vector3 offset = target.position - transform.position;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen <= lookRadius * lookRadius) {
            navMeshAgent.SetDestination(target.position);
            if (sqrLen <= navMeshAgent.stoppingDistance * navMeshAgent.stoppingDistance) {
                //Attack the target
                FaceTarget();
            }
        }
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
