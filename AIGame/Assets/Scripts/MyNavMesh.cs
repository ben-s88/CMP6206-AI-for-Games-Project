using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

struct Node
{
    public Node(bool passable_, GameObject obj_)
    {
        passable = passable_;
        obj = obj_;
        passed = false;
    }
    public bool passable;
    public GameObject obj;
    public bool passed;
}

public class MyNavMesh : MonoBehaviour
{
    Node[,] GOs = new Node[40,40];
    List<Node> currentPath = null;

    public GameObject enemy;
    public GameObject Player;

    public GameObject cube;
    public int gridResolution = 1;

    bool pathfind = true;
    bool nodesCollision = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createNavMesh();
    }

    

    // Update is called once per frame
    void Update()
    {
        //GOs[(int)enemy.transform.position.x, (int)enemy.transform.position.z].GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0, 0.17f);

        //Astar(enemy.transform.position.x, enemy.transform.position.z, Player.transform.position.x, Player.transform.position.z);

        if (pathfind)
        {
            Astar(enemy.transform.position.x, enemy.transform.position.z, Player.transform.position.x, Player.transform.position.z);

            
            pathfind = false;
        }

        if (currentPath != null)
        {
            StartCoroutine(nameof(traversePath), currentPath);
        }
    }

    private void LateUpdate()
    {
        if (nodesCollision)
        {
            for (int x = 0; x < 40; x++)
            for (int z = 0;  z < 40;  z++)
            {
                    GOs[x, z].passable = GOs[x, z].obj.GetComponent<GridNodeCollision>().passable;
            }

            nodesCollision = false;
        }
    }

    void createNavMesh()
    {
        for (int x = 0; x < 40; x += gridResolution)
        {
            for (int y = 0; y < 40; y += gridResolution)
            {
                GameObject a = Instantiate(cube, new Vector3(x + 0.5f, 1, y + 0.5f), Quaternion.identity);
                a.transform.localScale = Vector3.one * gridResolution * 0.9f;
                Node node = new(false, a);
                GOs[x, y] = node;
            }
        }
    }

    Node[] getAdjacentNodes(int x, int z)
    {
        return new Node[] { GOs[x+ 1, z + 1], GOs[x+ 1, z- 1], GOs[x+ 1, z],
                                  GOs[x, z+ 1], GOs[x, z- 1],
                                  GOs[x- 1, z+ 1], GOs[x- 1, z- 1], GOs[x- 1, z],};
    }

    void Astar(float xOrigin, float zOrigin, float xDest, float zDest)
    {
        List<Node> path = new List<Node>();
        path.Add(GOs[(int)xOrigin, (int)zOrigin]);

        evaluateAdjacentNodes(path, GOs[(int)xDest, (int)zDest]);

        foreach (Node node in path)
        {
            try { node.obj.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1); }
            catch
            {
                Debug.Log(path.FindIndex(a => a.obj.GetEntityId() == node.obj.GetEntityId()));
            }
            
        }

        currentPath = path;
    }

    List<Node> evaluateAdjacentNodes(List<Node> pathSoFar, Node dest) // return node with lowest heuristic
    {
        if (pathSoFar.Count == 0)
        {
            pathSoFar.Add(GOs[(int)transform.position.x, (int)transform.position.z]);
        }
        Node startNode = pathSoFar[pathSoFar.Count - 1];
        Node chosenNode = new();

        float lowestH = 10000000.0f;
        foreach (Node node in getAdjacentNodes((int)startNode.obj.transform.position.x, (int)startNode.obj.transform.position.z))
        {
            
            if (!node.passable) { continue; }
            if (pathSoFar.Any(x => x.obj.GetEntityId() == node.obj.GetEntityId())) {  continue; }
            if (pathSoFar.Count > 0)
            {
                //calculate g from currentpathlength
            }
            float testH = Vector3.Distance(new Vector3(node.obj.transform.position.x, 0, node.obj.transform.position.z), dest.obj.transform.position);
            if (testH < lowestH)
            {
                lowestH = testH;
                chosenNode = node;
            }
        }

        pathSoFar.Add(chosenNode);
        if (chosenNode.obj == dest.obj)
        {  
            return pathSoFar; 
        }
        else
        {
            return evaluateAdjacentNodes(pathSoFar, dest); // return the 
        }
    }

    float moveTime = 1.0f;
    IEnumerable traversePath(List<Node> path)
    {
        Node current, next;

        for (int x = 0; x < path.Count; x++)
        {
            current = path[x];
            next = path[x + 1];
            float timeMoving = 0.0f;
            while (next.obj.transform.position != transform.position)
            {
                transform.position = Vector3.Lerp(current.obj.transform.position, next.obj.transform.position, Mathf.Min( timeMoving / moveTime, 1.0f));
                yield return null;
                timeMoving += Time.deltaTime;
            }
        }
    }
}
