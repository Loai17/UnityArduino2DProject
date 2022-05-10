using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEdge
{
    private GraphNode _locationA;

    public GraphNode LocationA
    {
        get
        {
            return _locationA;
        }
    }

    private GraphNode _locationB;

    public GraphNode LocationB
    {
        get
        {
            return _locationB;
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
        this._locationA = locationA;
        this._locationB = locationB;
        this._weight = weight;
        _locationA.AddEdge(this);
        if (_locationA != _locationB)
        {
            _locationB.AddEdge(this);
        }
    }


    public bool isIncidentTo(GraphNode node)
    {
        return (_locationA == node || _locationB == node);
    }


    public override string ToString()
    {
        return _locationA.ToString() + ", " + _locationB.ToString();
    }

    public string ToString2()
    {
        return _locationA.ToString() + ", " + _locationB.ToString() + ", " + Weight;
    }
}
