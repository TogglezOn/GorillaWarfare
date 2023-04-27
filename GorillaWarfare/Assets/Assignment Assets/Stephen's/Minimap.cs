using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Camera minimapCamera;
    public RenderTexture renderTexture;
    public RawImage minimapImage;

    private void Start()
    {
        // Set the target texture of the minimap camera to the render texture
        minimapCamera.targetTexture = renderTexture;
        Debug.Log("minimap spawned");
        // Assign the render texture to the minimap image
        minimapImage.texture = renderTexture;
    }

    private void LateUpdate()
    {
        // Update the position of the minimap camera to match the player's position
        minimapCamera.transform.position = new Vector3(
            transform.position.x,
            minimapCamera.transform.position.y,
            transform.position.z
        );
    }
}
