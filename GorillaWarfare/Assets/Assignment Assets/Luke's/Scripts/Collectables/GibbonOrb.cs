using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibbonOrb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInvetory = other.GetComponent<PlayerInventory>();

        if(playerInvetory != null)
        {
            playerInvetory.OrbsCollected();
            gameObject.SetActive(false);
        }
    }
}
