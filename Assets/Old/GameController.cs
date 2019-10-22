using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        InvokeRepeating("spawnBot", 0f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnBot()
    {
        Instantiate(robot, new Vector3(0, 4f, -6.2f), Quaternion.identity);
    }
}
