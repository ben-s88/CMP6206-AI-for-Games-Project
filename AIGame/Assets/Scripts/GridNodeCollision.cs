using UnityEngine;

public class GridNodeCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0.17f);
    }
}
