﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackSpeed = 1f;
    public float attackCooldown = 0f;

    public float attackDelay = .6f;

    CharacterStats myStats;

    public event System.Action OnAttack;

    private void Start() {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update() {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack.Invoke();

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.attack.GetValue());
    }
}
