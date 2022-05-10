using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameManager gameManager;
    ArduinoMechanics playerArduino;
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerArduino = GameObject.FindGameObjectWithTag("Player").GetComponent<ArduinoMechanics>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && playerArduino.cardInserted) {
            Debug.Log("You may choose one of those next locations:");
            foreach (GraphNode location in gameManager.currentLocation.getNeighbors())
                Debug.Log(location.ToString());
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player") {
            playerInRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
