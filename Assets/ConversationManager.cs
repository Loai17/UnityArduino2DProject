using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{

    Node start;
    Tree tutorialTree;
    public Text text;

    public List<ButtonInfo> buttons = new List<ButtonInfo>();
    public List<Button> buttonsL = new List<Button>();
    


    // Start is called before the first frame update
    void Start()
    {
        start = new Node("Hello and Welcome to this Game!");
        Node start1_1 = new Node("First Option");
        Node start1_2 = new Node("Second Option");
        Node start1_3 = new Node("Third Option");
        // Added "Node" here on the line above me - was giving me an error and preventing the game from playing. -L

        tutorialTree = new Tree(start);
        tutorialTree.addNode(start1_1, start);
        tutorialTree.addNode(start1_2, start);
        tutorialTree.addNode(start1_3, start1_2);

        
        foreach (Button b in buttonsL)
        {
            ButtonInfo bI = new ButtonInfo();
            bI.b = b;
            buttons.Add(bI);
        }

        initButtons(tutorialTree);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            tutorialTree.Step(tutorialTree.getNextOptions()[0]);
        }
        if (Input.GetKeyDown(KeyCode.H) && tutorialTree.current.getParent() != null)
        {
            tutorialTree.Step(tutorialTree.current.getParent());
        }
    }

    public void ButtonClick(Button b)
    {
        //Debug.Log(getInfoFromButton(b).n);
        if (getInfoFromButton(b).b != null) tutorialTree.Step(getInfoFromButton(b).n);
        initButtons(tutorialTree);
    }

    public ButtonInfo getInfoFromButton(Button b)
    {
        foreach (ButtonInfo bI in buttons)
        {
            //Debug.Log(b);
            //Debug.Log(bI.b);
            //Debug.Log("===");
            if (b == bI.b)
            {
                return bI;
            }
        }
        return new ButtonInfo();
    }

    void initButtons(Tree t)
    {
        Debug.Log(t.getNextDisplayOptions());
        List<Node> nodes = t.getNextDisplayOptions();
        int i = 0;
        foreach (Node n in nodes)
        {
            buttons[i].b.transform.GetChild(0).GetComponent<Text>().text = n.getText();
            ButtonInfo h = buttons[i];
            h.n = n;
            buttons[i].b.gameObject.SetActive(true);
            i++;
        }
        for (int j = i; j < buttons.Count; j++)
        {
            buttons[j].b.gameObject.SetActive(false);
        }
    }

    public struct ButtonInfo
    {
        public Button b;
        public Node n;
    }

    public class Tree
    {
        Node root;
        List<Node> nodes = new List<Node>();
        public Node current;

        public Tree(Node r)
        {
            root = r;
            current = root;
        }

        public void addNode(Node n, Node parent)
        {
            parent.addChild(n);
            n.setParent(parent);
            nodes.Add(n);

        }

        public string getCurrentText()
        {
            return current.getText();
        }

        public List<Node> getNextOptions()
        {
            Debug.Log(current);
            Debug.Log(current.getChildren());
            List<Node> h = current.getChildren();
            h.Add(current.getParent());
            return h;
        }

        public List<Node> getNextDisplayOptions()
        {
            return current.getChildren();
        }

        public bool Step(Node n)
        {
            if (getNextOptions().Contains(n))
            {
                current = n;
                return true;
            }
            return false;
        }

    }

    public class Node
    {

        List<Node> children = new List<Node>();
        Node parent;
        string textToDisplay;
        

        public Node(string text)
        {
            textToDisplay = text;
        }

        public Node addChild(Node newChild)
        {
            children.Add(newChild);
            return newChild;
        }

        public void Print()
        {
            Debug.Log('a');
        }

        public string getText()
        {
            return textToDisplay;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public Node getParent()
        {
            return parent;
        }  
        public void setParent(Node p)
        {
            this.parent = p;
        }

    }
}
