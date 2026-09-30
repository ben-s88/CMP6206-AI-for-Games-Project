using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent NVM;
    Vector3 dest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NVM.SetDestination(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!NVM.hasPath)
        {
            NVM.SetDestination(new Vector3(Random.Range(-1.0f, 1.0f) * 5, Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f) * 5));
        }
        //NVM.Move(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * Time.deltaTime);
        
    }
}
