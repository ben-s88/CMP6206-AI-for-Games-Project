using UnityEngine;

public class MyNavMesh : MonoBehaviour
{
    public GameObject cube;
    public int gridResolution = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createNavMesh()
    {
        for (int x = -20; x  < 19; x += gridResolution)
        for (int y = -20; y  < 19; y += gridResolution)
        {
            Instantiate(cube, new Vector3(x + 0.5f, 1, y + 0.5f), Quaternion.identity).transform.localScale = Vector3.one * gridResolution * 0.9f;
        }
    }
}
