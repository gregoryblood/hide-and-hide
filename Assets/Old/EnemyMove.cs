using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxRotation = 30f;
    public Transform target;

    private bool searching = true;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (searching)
        {
            transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * moveSpeed), 0f);
        }
        */
        transform.LookAt(target);
        
    }

}
