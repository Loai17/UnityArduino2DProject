using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScripts : MonoBehaviour
{

    GameObject gM;
    public Animator fireAnim; 

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
                StartCoroutine(TreeDestroyed());
            }
        }
    }

    public IEnumerator TreeDestroyed()
    {
        fireAnim.SetBool("OnFire", true);
        yield return new WaitForSeconds(1.1f);
        fireAnim.SetBool("OnFire", false);
        gM.GetComponent<GameManager>().treesDestroyed += 1;
        Destroy(this.gameObject);
    }
}
