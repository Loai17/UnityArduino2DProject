using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEdge : MonoBehaviour
{
    private GraphNode locationA;

    public GraphNode LocationA
    {
        get
        {
            return locationA;
        }
    }

    private GraphNode locationB;

    public GraphNode LocationB
    {
        get
        {
            return locationB;
        }
    }

    private float _weight;

    public float Weight
    {
        get
        {
            return _weight;
        }
    }

    public GraphEdge(GraphNode locationA, GraphNode locationB, float weight)
    {
        this.locationA = locationA;
        this.locationB = locationB;
        this._weight = weight;
        locationA.AddEdge(this);
        if (locationA != locationB)
        {
            locationB.AddEdge(this);
        }
    }


    public bool isIncidentTo(GraphNode node)
    {
        return (locationA == node || locationB == node);
    }


    public override string ToString()
    {
        return LocationA.ToString() + ", " + LocationB.ToString();
    }

    public string ToString2()
    {
        return LocationA.ToString() + ", " + LocationB.ToString() + ", " + Weight;
    }
}
