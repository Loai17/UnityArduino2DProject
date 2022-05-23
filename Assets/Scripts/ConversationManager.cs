using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Node.cs;
using Tree.cs;

public class ConversationManager : MonoBehaviour
{
    // Defining Trees needed for story
    Node start;
    Node root;
    public Tree tutorialTree;
    public Tree tutorialTreeA;
    Tree fireStation1Tree;
    Tree fireStation2Tree;
    public int TreeNum;
    public List<Tree> trees = new List<Tree>();
 

    // Tree currently selected and being displayed 
    public Tree currentTree;

    // Unity UI Objects to display the Dialogues
    public Text text;
    public List<ButtonInfo> buttons = new List<ButtonInfo>();
    public List<Button> buttonsL = new List<Button>();
    public Text staticTextField;


    public ArduinoMechanics aM;

    // Help Variables to force a delay between selection via the controller to prevent too many inputs at once
    public float delayTime = 0.5f;
    public float timer;
    public bool choosingAllowed;

    // Access to the object storing the inventory to change conversation tree based on which items are in the inventory
    InventoryList iL;

    void Start()
    {
        // Fetching needed scripts from the scene
        aM = GameObject.Find("Player").GetComponent<ArduinoMechanics>();
        iL = GameObject.Find("GameManager").GetComponent<InventoryList>();

        // Defining the Trees and adding the Story
        Node rootT = new Node("", "Already home Honey? Shouldnt you be at work?");
        tutorialTree = new Tree(rootT);
        start = new Node("I forgot something. I'll have to leave immediatly!", "What did you forget?");
        tutorialTree.addNode(start, rootT);
            Node N2_1 = new Node("My Keys", "They are not here... You must have lost them somewhere!");
            tutorialTree.addNode(N2_1, start);
                Node N3_1 = new Node("Silly me... I'll go and check in the forest");
                tutorialTree.addNode(N3_1, N2_1);

            Node N2_2 = new Node("To kiss you <3", ":*");
            tutorialTree.addNode(N2_2, start);
                Node N3_2 = new Node(":* Bye");
                tutorialTree.addNode(N3_2, N2_2);

        Node rootT2 = new Node("", "Already home Honey? Shouldnt you be at work?");
        tutorialTreeA = new Tree(rootT2);
        Node start2 = new Node("I forgot something. I'll have to leave immediatly!", "What did you forget?");
        tutorialTreeA.addNode(start2, rootT2);
        Node N2_1_A = new Node("My Keys", "But you have them on you, I can see it?");
        tutorialTreeA.addNode(N2_1_A, start2);
        Node N3_1_A = new Node("Silly me... I'll better get back to work!");
        tutorialTreeA.addNode(N3_1_A, N2_1_A);
        tutorialTreeA.addNode(N2_2, start2);
        tutorialTreeA.addNode(N3_2, N2_2);

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

        // Adding Trees to a list to manage them
        trees.Add(tutorialTree);
        trees.Add(fireStation1Tree);
        trees.Add(fireStation2Tree);
        trees.Add(tutorialTreeA);
        
        foreach (Button b in buttonsL)
        {
            ButtonInfo bI = new ButtonInfo();
            bI.b = b;
            buttons.Add(bI);
        }

        // Initiation of the UI from the current Treee
        currentTree = trees[TreeNum];
        initButtons(currentTree);


        // Some game logic to change the tree according to items the player already picked up
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

        if (this.TreeNum == 0)
        {
            foreach (GameObject g in iL.inventory)
            {
                if (g.GetComponent<Interactable>().itemName == "KeyCard")
                {
                    currentTree = trees[3];
                    initButtons(currentTree);
                }
            }
        }
    }

    void Update()
    {
        // Timer to handle too many inputs from the Arduino
        if (timer < delayTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            choosingAllowed = true;
        }

        // Getting Input from the Arduino Manager and calling functions accordingly
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

    // Traversing the tree with the 'Step' function when the player click through the dialogue options (only needed if the button clicks come from the ui and not the arduino)
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

    // Traversing the tree with the 'Step' function when the player click through the dialogue options by using the Arduino
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


    // Setting up the text in the buttons according to the current node found in the tree
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

    // Struct used to handle button inputs via the UI 
    public struct ButtonInfo
    {
        public Button b;
        public Node n;
    }        
}
