using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") 
        {
            GameObject player = col.gameObject;
            Debug.Log("Adding item to inventory");
            player.GetComponent<InventoryList>().Add(this.gameObject);
            //this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
