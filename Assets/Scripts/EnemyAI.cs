using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turningSpeed=2f;
    EnemyHealth enemyHealth;

    Animator anim;
    public NavMeshAgent navMeshAgent;
    float distanceToTarget=Mathf.Infinity;
    bool isProvoked = false;
    
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if
        (distanceToTarget <= chaseRange)
        {
            isProvoked = true;       
        }  
    }

    public void DamageTaken()
    {
        isProvoked = true;
    }


    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    private void ChaseTarget()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        navMeshAgent.SetDestination(target.position);
        if (enemyHealth.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
    }
    private void AttackTarget()
    {
        anim.SetBool("isAttacking", true);
        FindObjectOfType<EnemyAttack>().Trigger();
    }

    private void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void EnemyDeathAnimation()
    {
        anim.SetTrigger("isDead");
    }
    public void EnemyHeadshotAnimation()
    {
        anim.SetTrigger("isHeadshot");
    }
}
