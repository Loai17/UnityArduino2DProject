using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScripts : MonoBehaviour
{

    GameObject gM;

    private void Start()
    {
        gM = GameObject.Find("GameManager");
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.gameObject.GetComponent<ArduinoMechanics>().fireToggle)
            {
                TreeDestroyed();
                gM.GetComponent<GameManager>().treesDestroyed += 1;
            }
        }
    }

    public void TreeDestroyed()
    {
        Destroy(this.gameObject);
    }
}
