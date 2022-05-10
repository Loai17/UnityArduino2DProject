using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{

    Node start;
    Node root;
    public Tree tutorialTree;
    public Text text;

    public List<ButtonInfo> buttons = new List<ButtonInfo>();
    public List<Button> buttonsL = new List<Button>();
    public Text staticTextField;
    


    // Start is called before the first frame update
    void Start()
    {
        Node root = new Node("", "Hello123");
        start = new Node("Hello and Welcome to this Game!", "How are you today?");
        Node start1_1 = new Node("First Option");
        Node start1_2 = new Node("Second Option");
        Node start2_1 = new Node("Third Option");
        Node start2_2 = new Node("Third Option B");
        // Added "Node" here on the line above me - was giving me an error and preventing the game from playing. -L

        tutorialTree = new Tree(root);
        tutorialTree.addNode(start, root);
        tutorialTree.addNode(start1_1, start);
        tutorialTree.addNode(start1_2, start);
        tutorialTree.addNode(start2_1, start1_2);
        tutorialTree.addNode(start2_2, start1_1);

        
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

    }

    public void ButtonClick(Button b)
    {

        if (tutorialTree.Step(tutorialTree.getNextDisplayOptions()[buttonsL.IndexOf(b)]))
        {
            initButtons(tutorialTree);
        }
        else
        {
            Debug.Log("End of Tree reached!");
            tutorialTree.current = tutorialTree.nodes[0];
            initButtons(tutorialTree);
        }
        
        
    }

    void initButtons(Tree t)
    {
        List<Node> nodes = t.getNextDisplayOptions();
        int i = 0;
        foreach (Node n in nodes)
        {
            buttons[i].b.transform.GetChild(0).GetComponent<Text>().text = n.getText();
            staticTextField.text = t.current.staticText;
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
        public List<Node> nodes = new List<Node>();
        public Node current;

        public Tree(Node r)
        {
            root = r;
            current = root;
            nodes.Add(root);
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
            if (getNextDisplayOptions().Contains(n))
            {
                current = n;
                if (n.getChildren().Count == 0) return false;
                return true;
            }
            return false;
        }

    }

    public class Node
    {

        List<Node> children = new List<Node>();
        public Node parent;
        string textToDisplay;
        public string staticText;



        public Node(string text, string _staticText = "")
        {
            textToDisplay = text;
            staticText = _staticText;
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
