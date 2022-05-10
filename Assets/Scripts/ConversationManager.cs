using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{

    Node start;
    Node root;
    public Tree tutorialTree;
    public Tree currentTree;
    public Text text;

    public List<ButtonInfo> buttons = new List<ButtonInfo>();
    public List<Button> buttonsL = new List<Button>();
    public Text staticTextField;
    


    // Start is called before the first frame update
    void Start()
    {
        Node root = new Node("", "Already home Honey? Shouldnt you be at work?");
        tutorialTree = new Tree(root);
        start = new Node("I forgot something. I'll have to leave immediatly!", "What did you forget?");
        tutorialTree.addNode(start, root);
            Node N2_1 = new Node("My Keys", "Wait let me get them for you");
            tutorialTree.addNode(N2_1, start);
                Node N3_1 = new Node("Thanks! See you later :)");
                tutorialTree.addNode(N3_1, N2_1);

            Node N2_2 = new Node("To kiss you <3", ":*");
            tutorialTree.addNode(N2_2, start);
                Node N3_2 = new Node(":* Bye");
                tutorialTree.addNode(N3_2, N2_2);
       

        
        foreach (Button b in buttonsL)
        {
            ButtonInfo bI = new ButtonInfo();
            bI.b = b;
            buttons.Add(bI);
        }

        currentTree = tutorialTree;
        initButtons(currentTree);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneChange()
    {
        SceneManager.LoadScene("NextLocation");
    }

    public void ButtonClick(Button b)
    {

        if (currentTree.Step(currentTree.getNextDisplayOptions()[buttonsL.IndexOf(b)]))
        {
            initButtons(currentTree);
        }
        else
        {
            Debug.Log("End of Tree reached!");
            currentTree.current = currentTree.nodes[0];
            initButtons(currentTree);
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
