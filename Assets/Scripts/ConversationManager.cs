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
    Tree fireStation1Tree;
    Tree fireStation2Tree;
    public Tree currentTree;
    public Text text;

    public List<ButtonInfo> buttons = new List<ButtonInfo>();
    public List<Button> buttonsL = new List<Button>();
    public Text staticTextField;

    public int TreeNum;
    public List<Tree> trees = new List<Tree>();
    public ArduinoMechanics aM;

    public float delayTime = 0.5f;
    public float timer;
    public bool choosingAllowed;

    InventoryList iL;

    // Start is called before the first frame update
    void Start()
    {
        Node rootT = new Node("", "Already home Honey? Shouldnt you be at work?");
        tutorialTree = new Tree(rootT);
        start = new Node("I forgot something. I'll have to leave immediatly!", "What did you forget?");
        tutorialTree.addNode(start, rootT);
            Node N2_1 = new Node("My Keys", "Wait let me get them for you");
            tutorialTree.addNode(N2_1, start);
                Node N3_1 = new Node("Thanks! See you later :)");
                tutorialTree.addNode(N3_1, N2_1);

            Node N2_2 = new Node("To kiss you <3", ":*");
            tutorialTree.addNode(N2_2, start);
                Node N3_2 = new Node(":* Bye");
                tutorialTree.addNode(N3_2, N2_2);

        Node rootF = new Node("", "Where is your Key? I can't let you in without it!");
        fireStation1Tree = new Tree(rootF);
        Node F1_1 = new Node("I don't know... Can't you just let me in?", "NO! Go get them!");
        fireStation1Tree.addNode(F1_1, rootF);
            Node F2_1 = new Node("Okay okay... I'm already on my way ...");
            fireStation1Tree.addNode(F2_1, F1_1);
        Node F1_2 = new Node("I think I maybe left them in the Forst let me go and have a look...", "Good luck!");
        fireStation1Tree.addNode(F1_2, rootF);
            Node F2_2 = new Node("Thanks...");
            fireStation1Tree.addNode(F2_2, F1_2);

        Node rootF2 = new Node("", "I see you got your Key! Anything else to report?");
        fireStation2Tree = new Tree(rootF2);
        Node FF1_1 = new Node("Yes there is a fire in the Forest!!", "Then go and put it out1!11!!!1!1");
        fireStation2Tree.addNode(FF1_1, rootF2);
            Node FF2_1 = new Node("Okay okay... I'm already on my way ...");
            fireStation2Tree.addNode(FF2_1, FF1_1);
        Node FF1_2 = new Node("No nothing unusual! The forest is doing great ;)", "Good to hear");
        fireStation2Tree.addNode(FF1_2, rootF2);
            Node FF2_2 = new Node("Yessir. See you later!");
            fireStation2Tree.addNode(FF2_2, FF1_2);


        trees.Add(tutorialTree);
        trees.Add(fireStation1Tree);
        trees.Add(fireStation2Tree);
        


        foreach (Button b in buttonsL)
        {
            ButtonInfo bI = new ButtonInfo();
            bI.b = b;
            buttons.Add(bI);
        }

        currentTree = trees[TreeNum];
        initButtons(currentTree);

        aM = GameObject.Find("Player").GetComponent<ArduinoMechanics>();

        iL = GameObject.Find("GameManager").GetComponent<InventoryList>();

        if (this.TreeNum == 1)
        {
            foreach(GameObject g in iL.inventory)
            {
                if (g.GetComponent<Interactable>().itemName == "KeyCard")
                {
                    currentTree = trees[2];
                    initButtons(currentTree);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < delayTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            choosingAllowed = true;
        }
        if (aM.joystickToggle && choosingAllowed)
        {
            choosingAllowed = false;
            timer = 0;
            ButtonClickArduino(1);
        }
        if (aM.rotateToggle && choosingAllowed)
        {
            choosingAllowed = false;
            timer = 0;
            ButtonClickArduino(0);
        }
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

    public void ButtonClickArduino(int i)
    {

        if (currentTree.Step(currentTree.getNextDisplayOptions()[i]))
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
