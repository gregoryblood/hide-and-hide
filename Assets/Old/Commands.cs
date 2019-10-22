using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Commands : MonoBehaviour
{
    public Text timer;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f0");
        timer.text = minutes + ":" + seconds;
    }
}
