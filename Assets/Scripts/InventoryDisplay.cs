using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    public InventoryList invL;
    public float spacing = 0.15f;
    float space = 0.35f;
    ArduinoMechanics arM;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InventoryDisplay");

        if (objs.Length > 1)
        {
            foreach (GameObject o in objs)
            {
                if (o.transform.childCount == 0) 
                {
                    Destroy(o);
                }
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        invL = GameObject.Find("GameManager").GetComponent<InventoryList>();
        arM = GameObject.Find("Player").GetComponent<ArduinoMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arM.rotateToggle)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            foreach (GameObject item in invL.inventory)
            {
                item.SetActive(false);
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            foreach (GameObject item in invL.inventory)
            {
                item.SetActive(true);
            }
        }
        int i = 0;
        foreach (GameObject item in invL.inventory)
        {
            item.transform.position = new Vector3(this.transform.position.x + space + spacing * i, this.transform.position.y, this.transform.position.z);
            i++;
        }
    }
}
