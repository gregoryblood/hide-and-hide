using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
}

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (Physics.Raycast(transform.position, target.position))
        {
            agent.SetDestination(target.position);
        }


    }
}
