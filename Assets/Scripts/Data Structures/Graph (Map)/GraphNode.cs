using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{

    // In our project, we call a "Graph Node" as a "Location"

    List<GraphEdge> edges; // List of connected edges.
    public string LocationName; // Only attribute of a node is a location name which is equivalent the unity 'scene' name.

    public GraphNode(string name)
    {
        LocationName = name;
        edges = new List<GraphEdge>();
    }

    public void AddEdge(GraphEdge edge)
    {
        edges.Add(edge);
    }

    public GraphNode[] getNeighbors() // Function that returns the neighboring nodes/locations to the current node/location, by checking the connected edges.
    {
        GraphNode[] neighbours = new GraphNode[edges.Count];
        int i = 0;
        foreach (GraphEdge edge in edges)
        {
            if (edge.LocationA == this)
            {
                neighbours[i] = edge.LocationB;
            }
            else
            {
                neighbours[i] = edge.LocationA;
            }
            i++;
        }
        return neighbours;
    }

    public override string ToString() // Returns the location name of node.
    {
        return LocationName;
    }

}

