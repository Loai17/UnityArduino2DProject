using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Graph map;
    public GraphNode currentLocation;
    public int treesDestroyed;
    public string currentLocationName;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

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
        currentLocation = locationA;
        Debug.Log(currentLocation.getNeighbors());
    }


    // Update is called once per frame
    void Update()
    {
        if (currentLocationName != currentLocation.LocationName) currentLocationName = currentLocation.LocationName;

        if (SceneManager.GetActiveScene().name == "NextLocation")
        {
            GameObject location1 = GameObject.FindGameObjectWithTag("Location1");
            GameObject location2 = GameObject.FindGameObjectWithTag("Location2");

            List<GameObject> locations = new List<GameObject>();
            locations.Add(location1); locations.Add(location2);

            GraphNode[] nextLocations = currentLocation.getNeighbors();
            for (int i = 0; i < nextLocations.Length; i++)
            {
                //Debug.Log(nextLocations.Length);
                //Debug.Log(nextLocations[i].ToString());
                locations[i].GetComponent<GoToLocation>().nextLocation = nextLocations[i].ToString();
                locations[i].GetComponent<GoToLocation>().textUI.text = nextLocations[i].ToString();
            }
        }
    }
}
