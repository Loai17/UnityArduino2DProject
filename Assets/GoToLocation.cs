using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToLocation : MonoBehaviour
{

    public string nextLocation;
    public bool inRange = false;
    public bool cardNeeded = false;

    public Text textUI;
    GameManager gameManager;
    ArduinoMechanics playerArduino;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerArduino = GameObject.FindGameObjectWithTag("Player").GetComponent<ArduinoMechanics>();

        textUI.text = nextLocation;
    }

    void Update()
    {
        if ((cardNeeded && inRange && playerArduino.cardInserted) || (!cardNeeded && inRange))
        {
            SceneManager.LoadScene(nextLocation);
        }

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            inRange = false;
        }
    }
}
