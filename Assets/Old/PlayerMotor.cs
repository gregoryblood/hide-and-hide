using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    public Text pausePanel;

    private Camera camm;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
    public void Jump()
    {
        rb.AddForce(0f, 200f, 0f);
    }
    void FixedUpdate()
    {
        PerformMovement();
        PerfromRotation();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Robot")
        {
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
    void PerfromRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (camm != null)
        {
            camm.transform.Rotate(-cameraRotation);
        }
    }
    
    
}
