using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<GraphNode> locations; // List of nodes.
    List<GraphEdge> edges; // List of edges.
    public Graph()
    {
        locations = new List<GraphNode>();
        edges = new List<GraphEdge>();
    }

    public void addLocation(GraphNode location) // Adding a Node
    {
        locations.Add(location);
    }

    public GraphEdge addEdge(GraphNode locationA, GraphNode locationB, int price) // We interpreted the "weight" as "price" here.
    {
        GraphEdge edge = new GraphEdge(locationA, locationB, price);
        edges.Add(edge);
        return edge;
    }

    public GraphNode getNodeByName(string name) // Get a node by knowing its locationName attribute.
    {
        foreach (GraphNode location in locations) 
        {
            if (location.LocationName == name) return location;
        }
        return null;
    }

    public void ToString() { // Printing all nodes and then all edges.
        Debug.Log("Graph:");
        Debug.Log("Locations - ");
        foreach(GraphNode location in locations) {
            Debug.Log(location.ToString());
        }

        Debug.Log("Edges -");
        foreach(GraphEdge edge in edges)
        {
            Debug.Log(edge.ToString());
        }
    }
}
