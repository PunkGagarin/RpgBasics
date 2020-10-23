using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent navMeshAgent;
    Animator animator;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        float speedPercent = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
