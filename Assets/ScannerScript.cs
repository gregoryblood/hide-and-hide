using UnityEngine;

public class ScannerScript : MonoBehaviour
{
    public float fov = 90f;

    private SphereCollider col;
    RobotControl robotControl;
    private bool patrolling;
    // Start is called before the first frame update
    void Awake()
    {
        robotControl = this.transform.parent.GetComponent<RobotControl>();
        col = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle < fov * 0.5f || Vector3.Distance(other.transform.position, transform.position) < 10f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    robotControl.patrol();
                    if (hit.collider.CompareTag("Player"))
                    {
                        robotControl.chase();
                    }
                }
            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            robotControl.patrol();
        }
    }
}
