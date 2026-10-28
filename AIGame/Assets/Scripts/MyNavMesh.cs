using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MyNavMesh : MonoBehaviour
{
    GameObject[,] GOs = new GameObject[40,40];

    public GameObject enemy;
    public GameObject Player;

    public GameObject cube;
    public int gridResolution = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        //GOs[(int)enemy.transform.position.x, (int)enemy.transform.position.z].GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0, 0.17f);
        foreach (GameObject go in getAdjacentNodes((int)enemy.transform.position.x, (int)enemy.transform.position.z))
        {
            //go.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0, 0.17f);
        }

        Astar(enemy.transform.position.x, enemy.transform.position.z, Player.transform.position.x, Player.transform.position.z);
    }

    void createNavMesh()
    {
        for (int x = 0; x < 40; x += gridResolution)
        {
            for (int y = 0; y < 40; y += gridResolution)
            {
                GameObject a = Instantiate(cube, new Vector3(x + 0.5f, 1, y + 0.5f), Quaternion.identity);
                a.transform.localScale = Vector3.one * gridResolution * 0.9f;
                GOs[x, y] = a;
            }
        }
    }

    GameObject[] getAdjacentNodes(int x, int z)
    {
        return new GameObject[] { GOs[x+ 1, z + 1], GOs[x+ 1, z- 1], GOs[x+ 1, z],
                                  GOs[x, z+ 1], GOs[x, z- 1],
                                  GOs[x- 1, z+ 1], GOs[x- 1, z- 1], GOs[x- 1, z],};
    }

    void Astar(float xOrigin, float zOrigin, float xDest, float zDest)
    {
        List<GameObject> path = new List<GameObject>();
        GameObject currentNode;
        float currentPathLength = 0.0f;

        currentNode = GOs[(int)xOrigin, (int)zOrigin];
        GameObject bestNode = null;

        float lowestH = 10000000.0f;
        foreach(GameObject GO in getAdjacentNodes((int)currentNode.transform.position.x, (int)currentNode.transform.position.z))
        {
            if (path.Count != 0)
            {
                //calculate g from currentpathlength
            }
            float testH = Vector3.Distance(new Vector3(GO.transform.position.x, 0, GO.transform.position.z), new Vector3(xDest, 0, zDest));
            if (testH < lowestH)
            {
                lowestH = testH;
                bestNode = GO;
            }
        }

        bestNode.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
    }
}
