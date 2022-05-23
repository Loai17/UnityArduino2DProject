using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEdge
{
    private GraphNode _locationA; // Node A

    public GraphNode LocationA
    {
        get
        {
            return _locationA;
        }
    }

    private GraphNode _locationB; // Node B

    public GraphNode LocationB
    {
        get
        {
            return _locationB;
        }
    }

    // We have no use for weight in our project. But keeping it for future updates as it becomes easier to link a condition to a specific edge. (Such as: amount of money/points needed to use this edge to head towards the next location/node)
    private float _weight;

    public float Weight
    {
        get
        {
            return _weight;
        }
    }
    /// End of weight section.

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

    public override string ToString()
    {
        return _locationA.ToString() + ", " + _locationB.ToString();
    }

    public string ToString2() // Just a different way of structuring the value returned. For testing purposes. (Addition of weight)
    {
        return _locationA.ToString() + ", " + _locationB.ToString() + ", " + Weight;
    }
}
