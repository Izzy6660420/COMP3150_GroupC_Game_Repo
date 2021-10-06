using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomizedMajorEvent : MonoBehaviour
{

    public abstract IEnumerator customEvent();

    public abstract void triggerEvent();
}
