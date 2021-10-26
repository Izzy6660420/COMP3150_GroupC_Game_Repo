using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCollision : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Hideable"))
			Player.instance.canHide = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Hideable"))
			Player.instance.canHide = false;
	}
}
