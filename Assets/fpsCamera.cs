using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.Audio;

public class fpsCamera : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 0.5f;
    public float smoothing = 2.0f;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    public Text loseScreen;
    public Image topBar;
    public Image bottomBar;
    public Image rightBar;
    public Image leftBar;
    public AudioSource _static;

    private float filled;

    Vector3 originalPos;

    GameObject character;
    GameObject robot;
    Component lights;

    // Use this for initialization
    void Start()
    {
        filled = 0.7f;
        character = transform.parent.gameObject;
        lights = gameObject.transform.GetChild(0);
        _static.volume = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        
        transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(-mouseLook.y, -90, 90), Vector3.right);
        
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        robot = FindClosestTarget("Robot");
        float dist = Vector3.Distance(transform.position, robot.transform.position);
        
        if (dist < 10)
        {
            shakeAmount = (10f - dist) / 10f;
            lights.GetComponent<Light>().intensity = 1.5f - (2f * (10f - dist) / 10f);
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            topBar.fillAmount = filled * (10f - dist) / 10f;
            bottomBar.fillAmount = filled * (10f - dist) / 10f;
            leftBar.fillAmount = filled * (10f - dist) / 10f;
            rightBar.fillAmount = filled * (10f - dist) / 10f;
            _static.volume = (10f - dist) / 10f;
        }
        else
        {
            lights.GetComponent<Light>().intensity = 2f;
            transform.localPosition = originalPos;
            _static.volume = 0;
        }
        if (dist <= 4)
        {
            loseScreen.gameObject.SetActive(true);
            filled = 1f;
            Time.timeScale = 0;
        }
        //Shakey Cam

    }

    GameObject FindClosestTarget(string trgt)
    {
        Vector3 position = transform.position;
        return GameObject.FindGameObjectsWithTag(trgt)
            .OrderBy(o => (o.transform.position - position).sqrMagnitude)
            .FirstOrDefault();
    }
}