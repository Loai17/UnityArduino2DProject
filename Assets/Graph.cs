using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    List<GraphNode> locations;
    List<GraphEdge> edges;
    public Graph()
    {
        locations = new List<GraphNode>();
        edges = new List<GraphEdge>();
    }

    public void addLocation(GraphNode location)
    {
        locations.Add(location);
    }

    public GraphEdge addEdge(GraphNode locationA, GraphNode locationB, int price)
    {
        GraphEdge edge = new GraphEdge(locationA, locationB, price);
        edges.Add(edge);
       /* locationA.AddEdge(edge);
        locationB.AddEdge(edge);*/
        return edge;
    }


    public void ToString() {
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
