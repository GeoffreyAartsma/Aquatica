using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{

    public int Boundary = 50;
    public int speed = 5;


    void Start()
    {
    }


    void Update()
    {
        if (Input.mousePosition.x > Screen.width - Boundary)
        {
            transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if (Input.mousePosition.x < 0 + Boundary)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if (Input.mousePosition.y > Screen.height - Boundary)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);
        }
        if (Input.mousePosition.y < 0 + Boundary)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }
}

