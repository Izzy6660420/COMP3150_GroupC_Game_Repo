using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mPos - (Vector2)transform.position).normalized;

        transform.up = dir;
    }
}
