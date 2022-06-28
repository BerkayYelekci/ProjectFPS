using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    const string player = "Player";

    [SerializeField] float healAmount = 50f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == player)
        {
            FindObjectOfType<PlayerHealth>().IncreaseCurrentHP(healAmount);
            Destroy(gameObject);
        }
    }

}
