using Unity.Mathematics;
using UnityEngine;

public class VisionChecker : MonoBehaviour
{
    public float viewAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.green);

        Debug.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward), Color.green);
        Debug.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward), Color.green);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, other.transform.position, Color.red, Time.fixedDeltaTime);
            float angle = Vector3.Angle(transform.forward, Vector3.Normalize(other.transform.position - transform.position));
            if (angle < viewAngle / 2.0f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Player in view radius " + angle.ToString());
                        //Destroy(hit.rigidbody.gameObject);
                    }
                }
            }
        }
    }
}
