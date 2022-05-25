using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public List<GameObject> inventory; // List of unity "GameObjects" as the data type for our inventory.

    public InventoryList()
    {
        inventory = new List<GameObject>();
    }

    public void Add(GameObject item) // Adding an item to inventory.
    {
        inventory.Add(item);
    }

    public void Insert(int pos, GameObject item) // Inserting an item to inventory in assigned position (pos).
    {
        inventory.Insert(pos, item);
    }

    public int Size() // Returns an integer of inventory items amount.
    {
        return inventory.Count;
    }

    public GameObject Get(int pos) // Returns inventory item in a specific position (pos).
    {
        return inventory[pos];
    }

    public GameObject Remove(int pos) // Removes and reutrns inventory item in a specific position (pos).
    {
        GameObject item = inventory[pos];
        inventory.RemoveAt(pos);
        return item;
    }
}
