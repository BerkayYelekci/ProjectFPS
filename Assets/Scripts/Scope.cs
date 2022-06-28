﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] GameObject weaponCam;
    [SerializeField] Camera mainCamera;

    public float scopedFOV = 15f;
    private float normalFOV;


    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnscoped();
        }
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCam.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
        
    }
    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCam.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }
}
