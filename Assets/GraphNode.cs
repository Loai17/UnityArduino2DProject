using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    List<GraphEdge> edges;
    string LocationName;

    public GraphNode(string name)
    {
        LocationName = name;
        edges = new List<GraphEdge>();
    }

    int getDeg()
    {
        return edges.Count;
    }

    public void AddEdge(GraphEdge edge)
    {
        edges.Add(edge);
    }

    public bool IsAdjacent(GraphNode otherLocation)
    {
        foreach (GraphEdge edge in edges)
        {
            if (edge.isIncidentTo(otherLocation))
            {
                return true;
            }
        }
        return false;
    }

    public float GetAdjacenceWeight(GraphNode otherLocation)
    {
        foreach (GraphEdge edge in edges)
        {
            if (otherLocation == this) // checking loops
            {
                if (edge.LocationA == edge.LocationB)
                {
                    return edge.Weight;
                }
            } // checking incidence for different Locations
            else if (edge.isIncidentTo(otherLocation))
            {
                return edge.Weight;
            }
        }
        return 0f;
    }

    public GraphNode[] getNeighbours()
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

    void addEdge(GraphEdge edge)
    {
        edges.Add(edge);
    }

    public override string ToString()
    {
        return LocationName;
    }

}

