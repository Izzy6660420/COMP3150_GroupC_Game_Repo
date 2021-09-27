using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEvents : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D col);

    public abstract void OnTriggerExit2D(Collider2D col);

    public abstract void OnTriggerStay2D(Collider2D col);

}
