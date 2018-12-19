using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodScript : MonoBehaviour
{
    [SerializeField]
    Text countWood;

    [SerializeField]
    int woodcount;

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
                if (hit.transform.tag == "WoodResource")
                {
                    woodcount = woodcount + 1;
                    Debug.Log("Wood +1");

                    SetCountWood();
                }
        }
    }

    // Update Wood UI
    void SetCountWood()
    {
        countWood.text = "Wood: " + woodcount.ToString();
        Debug.Log("Wood Updated");
    }
}