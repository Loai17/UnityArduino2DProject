using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{

    Node start;
    Tree tutorialConversationTree;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        start = new Node(null, "Hello and Welcome to this Game!");
        start1_1 = new Node(start, "First Option");

        
        tutorialConversationTree = new Tree(start);
        

    }

    // Update is called once per frame
    void Update()
    {
        text.text = tutorialConversationTree.getCurrentText();
    }



    class Tree
    {
        Node root;
        List<Node> nodes;
        Node current;

        public Tree(Node r)
        {
            root = r;
            current = root;
        }

        public void addNode(Node n)
        {
            nodes.Add(n);
        }

        public string getCurrentText()
        {
            return current.getText();
        }

        public List<Node> getNextOptions()
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

    class Node
    {

        List<Node> children;
        Node parent;
        string textToDisplay;

        public Node(Node p, string text)
        {
            parent = p;
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

    }
}
