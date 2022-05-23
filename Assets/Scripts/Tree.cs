using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
// root node of the tree
        Node root;
        // all nodes that have been added to the tree
        public List<Node> nodes = new List<Node>();
        // current node that the tree is at 
        public Node current;


        public Tree(Node r)
        {
            // Set the first node given when creating the tree as the root and adding it to the nodes list
            root = r;
            current = root;
            nodes.Add(root);
        }

        // Adding a node to the tree and handling the setting of parentship
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

        // Get next options including the parent of the current node
        public List<Node> getNextOptions()
        {
            List<Node> h = current.getChildren();
            h.Add(current.getParent());
            return h;
        }

        // Only get the children of the current node for displaying the text in UI
        public List<Node> getNextDisplayOptions()
        {
            return current.getChildren();
        }

        // Try to step toHandling the A a given next node return true if possible and false if not
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
