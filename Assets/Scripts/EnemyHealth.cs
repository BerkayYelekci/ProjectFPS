using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    EnemyAI enemyAI;
    EnemyAttack enemyAttack;
    //AudioSource audioSource;
    public AudioClip deathSound;

    bool isDead = false;
    
    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        enemyAI = GetComponent<EnemyAI>();
        enemyAttack = GetComponent<EnemyAttack>();

    }

    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage,bool isHeadshot) 
    {       
        BroadcastMessage("DamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            if (!isHeadshot)
            {
                EnemyDead();
            }
            else
            {
                TakeHeadshot();
            }
           
        }
    }
    public void TakeHeadshot()
    {      
        if (isDead) return;
        isDead = true;
        enemyAI.EnemyHeadshotAnimation();
        enemyAI.navMeshAgent.enabled = false;
        Destroy(gameObject, 15);
        GetComponent<EnemyAI>().enabled = false;
    }
    void EnemyDead()
    {
        //EnemyDeathSound();
        if (isDead) return;
        isDead = true;
        enemyAI.EnemyDeathAnimation();
        enemyAI.navMeshAgent.enabled = false;
        Destroy(gameObject,15);
        GetComponent<EnemyAI>().enabled = false;
    }
    //public void EnemyDeathSound()
   // {
     //   audioSource.PlayOneShot(deathSound);
    //}
}
