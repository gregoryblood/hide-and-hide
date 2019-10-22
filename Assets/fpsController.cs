using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fpsController : MonoBehaviour
{
    public float speed = 10.0f;

    public Text score;
    public Text timer;
    public Text winScreen;
    public Image pauseScreen;
    public GameObject usedKey;

    private float startTime;
    bool crouching = false;
    bool paused = false;
    int scorenum = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        score.text = "0";
        startTime = Time.time;
    }

    void KeyboardMovement()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        transform.Translate(straffe, 0, translation);
        /*
        if (Input.GetKeyDown("c"))
        {
            if (crouching)
            {
                crouching = false;
                speed *= 2.5f;
                gameObject.GetComponent(typeof(BoxCollider)).transform.localScale += new Vector3(-1f, 1.4f, -1f);
            }
            else
            {
                crouching = true;
                speed /= 2.5f;

                gameObject.GetComponent(typeof(BoxCollider)).transform.localScale -= new Vector3(-1f, 1.4f, -1f);

            }
        }
        */

    }

    void Update()
    {
        KeyboardMovement();
        if (Input.GetKeyDown("escape"))
        {
            if(paused)
            {
                paused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                pauseScreen.enabled = false;
            }
            else
            {
                paused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                pauseScreen.enabled = true;
            }
            
        }
        if (Input.GetKeyDown("delete"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        //Wingame
        if (int.Parse(score.text) >= 5)
        {
            winScreen.gameObject.SetActive(true);
        }
        //timer
        float t = Time.time - startTime;
        int minutes = Mathf.FloorToInt(t / 60F);
        int seconds = Mathf.FloorToInt(t - minutes * 60);
        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            Instantiate(usedKey, collision.transform.position, collision.transform.rotation);
            scorenum++;
            score.text = scorenum.ToString();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hide"))
        {
            speed /= 2.5f;
            gameObject.GetComponent(typeof(BoxCollider)).transform.localScale -= new Vector3(0f, 1.4f, 0f);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 
                                transform.position.y - 1, transform.position.z), 100f * Time.deltaTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hide"))
        {
            speed *= 2.5f;
            gameObject.GetComponent(typeof(BoxCollider)).transform.localScale += new Vector3(0f, 1.4f, 0f);
        }
    }
}
