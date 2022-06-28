using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    [SerializeField] float headShotDamage = 100f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] TextMeshProUGUI ammoText;

    AudioSource gunAS;
    public AudioClip shootAC;

    EnemyAI enemyAI;

    Animator anim;
    bool canShoot = true;

    private void Start()
    {
        gunAS = GetComponent<AudioSource>();
        enemyAI = GetComponent<EnemyAI>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0)&&canShoot==true)
        {
           StartCoroutine(Shoot());         
        }
    }
    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo();
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayGunSound();
            PlayMuzzleFlash();
            ProcessRaycast();         
            ammoSlot.ReduceCurrentAmmo();
                 
        }
      yield  return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                HitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                target.TakeDamage(damage,false);
            }
            else if (hit.transform.tag == "Head")
            {
              
                HitImpact(hit);
                EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
                
                if (target == null) return;
                target.TakeDamage(headShotDamage,true);
               
            }    
        }
        else
        {
            return;
        }
    }
    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
    private void PlayGunSound()
    {
        if (gunAS != null)
        {
            gunAS.PlayOneShot(shootAC);
        }
    }

    private void HitImpact(RaycastHit hit)
    {
       GameObject impact= Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
