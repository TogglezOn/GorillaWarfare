using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PingSystem : MonoBehaviour
{
    public float maxPingDistance = 50f; // Maximum distance the player can ping from
    public float pingDuration = 5f; // How long the ping will last on the minimap
    public GameObject pingPrefab; // Prefab for the ping object
    public LayerMask pingLayerMask; // Layer mask for the ping object
    public KeyCode pingKey = KeyCode.F; // Keycode for the ping key
    private Camera mainCamera; // Main camera for the player
    private bool canPing = true; // Whether or not the player can ping
    private float nextPingTime = 0f; // The time at which the player can ping again

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera for the player
    }

    void Update()
    {
        if (canPing && Time.time >= nextPingTime)
        { // Check if the player can ping
            if (Input.GetKeyDown(pingKey))
            { // Check if the ping key is pressed
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxPingDistance, pingLayerMask))
                { // Check if the player is pinging an object in range
                    StartCoroutine(Ping(hit.point)); // Start the ping coroutine
                    Debug.Log("Pinging");
                    nextPingTime = Time.time + pingDuration; // Set the next ping time
                }
            }
        }
    }

    IEnumerator Ping(Vector3 point)
    {
        canPing = false; // Disable the player's ability to ping
        GameObject pingObject = Instantiate(pingPrefab, point, Quaternion.identity); // Instantiate the ping object
        Destroy(pingObject, pingDuration); // Destroy the ping object after the ping duration
        yield return new WaitForSeconds(pingDuration); // Wait for the ping duration
        canPing = true; // Enable the player's ability to ping
    }
}
