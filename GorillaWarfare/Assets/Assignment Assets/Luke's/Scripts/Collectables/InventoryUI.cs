using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI orbsText;
 
    void Start()
    {
        orbsText = GetComponent<TextMeshProUGUI>();
    }

   public void UpdateOrbText(PlayerInventory playerInventory)
    {
        orbsText.text = playerInventory.NumberOfOrbs.ToString();
    }
}
