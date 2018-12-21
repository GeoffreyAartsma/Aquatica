using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    float interval;

    [SerializeField]
    public WaterCounter watercount; 

    float timer;

    public bool IsProducing;

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        if (IsProducing && timer + interval <= Time.time)
        {
            watercount.AddWater();
                
            timer = Time.time;
        }
    }

    public void SetTransparency(int percentage)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, percentage / 100f);
    }
}
