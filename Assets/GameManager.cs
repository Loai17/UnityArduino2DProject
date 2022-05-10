using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Graph map;
    public GraphNode currentLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        map = new Graph();

        GraphNode locationA = new GraphNode("Forest");
        GraphNode locationB = new GraphNode("Firestation");
        GraphNode locationC = new GraphNode("House");

        map.addLocation(locationA);
        map.addLocation(locationB);
        map.addLocation(locationC);

        map.addEdge(locationA, locationB, 0);
        map.addEdge(locationB, locationC, 0);
        map.addEdge(locationC, locationA, 0);

        map.ToString();
        currentLocation = locationC;
        Debug.Log(currentLocation.getNeighbors());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
