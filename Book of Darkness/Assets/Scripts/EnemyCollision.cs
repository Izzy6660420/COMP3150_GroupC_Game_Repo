using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemies") && !Player.instance.invincible)
        {
            Player.instance.TakeHit(1);
        }
    }
}
