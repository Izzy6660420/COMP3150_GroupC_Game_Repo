using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    List<GameObject> objects = new List<GameObject>();
    private int batteries = 0;

    public void AddBattery()
    {
        batteries++;
    }

    public void AddItem(GameObject obj)
    {
        objects.Add(obj);
    }
}
