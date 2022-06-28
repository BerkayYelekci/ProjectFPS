using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    const string Player= "Player";

    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 250f;
    Rigidbody rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        Transform target = GameObject.FindGameObjectWithTag(Player).transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == Player)
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.SplatterVoid(damage);
            //StartCoroutine(playerHealth.TakeDamage(damage));
            Destroy(gameObject);
        }
        else
        {
            this.enabled = false;
        }
    }
}
