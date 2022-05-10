using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    GameObject gM;
    GameObject iV;
    private void Start()
    {
        gM = GameObject.Find("GameManager");
        iV = GameObject.Find("InventoryDisplay");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") 
        {
            GameObject player = col.gameObject;
            Debug.Log("Adding item to inventory");
            gM.GetComponent<InventoryList>().Add(this.gameObject);
            //this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.transform.parent = iV.transform;
        }
    }
}
