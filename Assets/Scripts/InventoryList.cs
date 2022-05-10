using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public List<GameObject> inventory;

    public InventoryList()
    {
        inventory = new List<GameObject>();
    }

    public void Add(GameObject item)
    {
        inventory.Add(item);
    }

    public void Insert(int pos, GameObject item)
    {
        inventory.Insert(pos, item);
    }

    public int Size()
    {
        return inventory.Count;
    }

    public GameObject Get(int pos)
    {
        return inventory[pos];
    }

    public GameObject Remove(int pos)
    {
        GameObject item = inventory[pos];
        inventory.RemoveAt(pos);
        return item;
    }
}
