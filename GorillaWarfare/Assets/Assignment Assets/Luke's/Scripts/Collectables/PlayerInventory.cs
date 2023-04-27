using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
public int NumberOfOrbs { get; private set; }

    public UnityEvent<PlayerInventory> OnOrbCollected;

    public void OrbsCollected()
    {
        NumberOfOrbs++;
        Debug.Log("eh");
        OnOrbCollected.Invoke(this);
    }
}
