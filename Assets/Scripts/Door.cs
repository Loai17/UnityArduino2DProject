using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameManager gameManager;
    ArduinoMechanics playerArduino;
    public bool playerInRange = false;
    public bool cardNeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerArduino = GameObject.FindGameObjectWithTag("Player").GetComponent<ArduinoMechanics>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager==null) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(playerArduino==null) GameObject.FindGameObjectWithTag("Player").GetComponent<ArduinoMechanics>();

        if ((cardNeeded && playerInRange && playerArduino.cardInserted) || (!cardNeeded && playerInRange)) {
            if (SceneManager.GetActiveScene().name != "NextLocation") SceneManager.LoadScene("NextLocation");
            else SceneManager.LoadScene(gameManager.currentLocation.ToString());
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
