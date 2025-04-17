using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Collider swordCollider;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isDead = false;
    [SerializeField] private float attackInterval = 0.5f;

    private float lastAttackTime = 0;

    private void Start()
    {
        swordCollider.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void startAttack()
    {
        swordCollider.enabled = true;
    }
    public void endAttack()
    {
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 1f && !isDead)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("Run", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Run", false);
            if (Time.time - lastAttackTime > attackInterval)
            {
                animator.SetTrigger("Attack");
            }
        }

    }
    
    public void OnDeath()
    {
        Debug.Log("enemy died");
        isDead = true;
        agent.isStopped = false;
    }


}
