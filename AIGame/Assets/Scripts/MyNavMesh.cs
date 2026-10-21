using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MyNavMesh : MonoBehaviour
{
    GameObject[,] GOs = new GameObject[40,40];

    public GameObject enemy;

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
            go.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 0, 0.17f);
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
}
