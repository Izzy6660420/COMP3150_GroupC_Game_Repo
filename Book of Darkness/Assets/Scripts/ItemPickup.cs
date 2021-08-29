using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			col.GetComponent<InventoryManager>().AddItem(this.gameObject);
			Destroy(this.gameObject);
		}
	}
}
