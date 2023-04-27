using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  public int NumberOfOrbs { get; private set; }

    public void OrbsCollected()
    {
        NumberOfOrbs++;
    }
}
