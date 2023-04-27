using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // Reference to the spawn point object
    private bool hasTeleported = false; // Flag to prevent multiple teleports

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a "Player" tag and hasn't been teleported yet
        if (other.CompareTag("Player") && !hasTeleported)
        {
            // Reset the player position to the spawn point
            other.transform.position = spawnPoint.position;

            // Set flag to true to prevent further teleports
            hasTeleported = true;

            // Disable collider to prevent repeated calls to OnTriggerEnter
            GetComponent<Collider>().enabled = false;

            // Enable collider after a short delay to prevent immediate re-entry
            Invoke(nameof(EnableCollider), 2f);
        }
    }

    // Enable collider after a delay
    private void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
        hasTeleported = false;
    }
}
