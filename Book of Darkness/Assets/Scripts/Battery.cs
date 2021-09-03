using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private InventoryManager playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D col) // Player collect
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInventory.AddBattery();
            Destroy(gameObject);
        }
    }
}
