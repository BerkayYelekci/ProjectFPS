using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    const string player = "Player";

    [SerializeField] int ammoAmount=5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == player)
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}
