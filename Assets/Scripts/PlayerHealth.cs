using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject bloodOverlay;

    private void Start()
    {
        bloodOverlay.SetActive(false);
    }

    public IEnumerator TakeDamage(float damage)
    {
        Debug.Log("Corutine started");
        hitPoints -= damage;
        bloodOverlay.SetActive(true);
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().DeathHandle();
        }
        yield return new WaitForSeconds(2);
        bloodOverlay.SetActive(false);
        Debug.Log("Corutine finished");
    }
    public void SplatterVoid(float damage)
    {
        StartCoroutine(TakeDamage(damage));
    }
    public void IncreaseCurrentHP(float healS)
    {
        hitPoints += healS;
    }

}
