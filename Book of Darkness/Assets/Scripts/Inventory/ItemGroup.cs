using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroup : MonoBehaviour
{
    public Food[] items;

    void Start()
    {
        foreach (var item in items)
        {
            item.itemChosenEvent += ReturnItem;
        }
    }

    void ReturnItem(Food curr)
    {
        foreach (var item in items)
        {
            if (item != curr) item.Drop();
        }
    }
}
