using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // List of other nodes that will be the children of this node
        List<Node> children = new List<Node>();
        // parent node (only gets set when node is added to a tree)
        public Node parent;
        // 2 string fields for the ingame displaying of text
        string textToDisplay;
        public string staticText;

        // Setting both text fields
        public Node(string text, string _staticText = "")
        {
            textToDisplay = text;
            staticText = _staticText;
        }

        // Adding and returning a child node
        public Node addChild(Node newChild)
        {
            children.Add(newChild);
            return newChild;
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


