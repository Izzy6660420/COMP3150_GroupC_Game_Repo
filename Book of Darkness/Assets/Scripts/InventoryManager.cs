using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddItem(GameObject obj)
    {
        objects.Add(obj);
    }
}
