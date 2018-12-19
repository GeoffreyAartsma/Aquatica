using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    Text countWater;

    [SerializeField]
    int watercount;

    [SerializeField]
    Camera cam;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                if (hit.transform.tag == "WaterResource")
                {
                    watercount = watercount + 1;
                    Debug.Log("Water +1");

                    SetCountWater();
                }
        }
    }

    // Update Wood UI
    void SetCountWater()
    {
        countWater.text = "Water: " + watercount.ToString();
        Debug.Log("Water Updated");
    }
}
