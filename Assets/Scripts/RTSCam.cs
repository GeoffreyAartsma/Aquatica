using UnityEngine;

public class RTSCam : MonoBehaviour {
    [SerializeField]
    float climbSpeed = 4;
    [SerializeField]
    float normalMoveSpeed = 10;
    [SerializeField]
    float slowMoveFactor = 0.25f;
    [SerializeField]
    float fastMoveFactor = 3;

    void Start()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
    }

    void Update()
    {
        Vector3 forwardLocked = new Vector3(transform.forward.x, 0f, transform.forward.z);

        float speed = normalMoveSpeed;

        // Faster flying on shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            speed *= fastMoveFactor;
        } else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            speed *= slowMoveFactor;
        }

        Vector3 velocity = forwardLocked * speed * Input.GetAxis("Vertical") * Time.deltaTime +
                         transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime;

        // Elevation
        if (Input.GetKey(KeyCode.Q)) { velocity.y = -climbSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.E)) { velocity.y = climbSpeed * Time.deltaTime; }

        transform.position += velocity;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30, 27), 
                                         Mathf.Clamp(transform.position.y, 0f, 25f), 
                                         Mathf.Clamp(transform.position.z, -64, 20));
    }
}
