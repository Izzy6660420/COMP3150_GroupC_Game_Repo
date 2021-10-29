using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of Inventory detected!");
        }
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int space = 5;
    public int batteries = 0;

    public event Action BookObtainedEvent;

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }

        if (item.name == "Book")
            BookObtainedEvent?.Invoke();

        items.Add(item);
        onItemChangedCallback?.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.name == itemName)
                return true;
        }
        return false;
    }
}
