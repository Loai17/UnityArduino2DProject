using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontChooser : MonoBehaviour
{
    ConversationManager cM;

    public Font f1;
    public Font f2;

    public Text StaticTextField;

    // Start is called before the first frame update
    void Start()
    {
        cM = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cM.TreeNum == 1 || cM.TreeNum == 2)
        {
            StaticTextField.font = f2;
        }
        if (cM.TreeNum == 0 )
        {
            StaticTextField.font = f1;
        }

    }
}
