using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{

    public int Boundary = 50;
    public int speed = 5;

    private int screenWidth;
    private int screenHeight;


    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }


    void Update()
    {

    }
}

