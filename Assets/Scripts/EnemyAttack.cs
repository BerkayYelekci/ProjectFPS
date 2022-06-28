using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform target;
    
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] float turningSpeed = 5f;
    float fireRate = 0.2f;

    AudioSource akAS;
    public AudioClip akAC;

    private void Start()
    {
        akAS = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Trigger()
    {
        fireRate -= Time.deltaTime;
        Vector3 direction = target.position-transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turningSpeed);

        if (fireRate <= 0)
        {
            fireRate=0.5f;
            Shoot();
        }
    }

    public void Shoot()
    {
        PlayAkSound();
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
    }

    public void PlayAkSound()
    {
        akAS.PlayOneShot(akAC);
    }
    
}
