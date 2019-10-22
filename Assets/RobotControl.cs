using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Audio;


public class RobotControl : MonoBehaviour
{
    public Transform target;
    public UnityEngine.AI.NavMeshAgent agent;
    public float wanderTimer;
    public float wanderRadius;

    private bool patrolling = true;
    private float timer;
    private Light light1;
    private Light[] light2;
    private float searchTime;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        light1 = GetComponent<Light>();
        light2 = GetComponentsInChildren<Light>();
    }
    public void chase()
    {
        if (patrolling == true)
        {
            patrolling = false;
            gameObject.GetComponent<AudioSource>().pitch += 2f * Time.deltaTime;
            Mathf.Clamp(gameObject.GetComponent<AudioSource>().pitch, 0.30f, 0.7f);
            light1.intensity *= 2;
            foreach (Light light in light2)
            {
                light.intensity *= 2;
            }
        }
        
        agent.SetDestination(target.position);

        agent.speed = 5f;
        agent.angularSpeed = 360f;


    }
    public void patrol()
    {
        if(patrolling == false)
        {
            agent.speed = 3.5f;
            agent.angularSpeed = 180f;
            patrolling = true;
            gameObject.GetComponent<AudioSource>().pitch -= 2f * Time.deltaTime;
            Mathf.Clamp(gameObject.GetComponent<AudioSource>().pitch, 0.30f, 0.7f);

            light1.intensity /= 2;
            foreach (Light light in light2)
            {
                light.intensity /= 2;
            }
        } 
    }
    void wander()
    {
        
        if (timer >= wanderTimer)
        {
            /*
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                finalPosition = hit.position;
            }
            agent.destination = finalPosition;
            */
            Vector3 newPos = Vector3.zero;
            /*
            do
            {
                newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);

            } while (Vector3.Distance(agent.transform.position, agent.destination) < 3.1);
            */
            newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);

            timer = 0;


        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (patrolling)
        {
            if (agent.remainingDistance <= 3.1)
            {
                //transform.rotation = Quaternion.Euler(0f, 90 * Mathf.Sin(Time.time * 1f), 0f);
                wander();
            }
                
        }
        //gameObject.GetComponent<AudioSource>().pitch = 0.4f + 0.2f * ((50 - Vector3.Distance(transform.position, target.position)) / 100);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
