using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{

    [SerializeField]
    float interval;

    public ResourceManager resourceManager;

    float timeSinceLastUpdate;

    [HideInInspector]
    public bool IsProducing;

    [HideInInspector]
    public enum ResourceType
    {
        Water,
        Wood
    }

    public ResourceType resourceType;

    void Start()
    {
        timeSinceLastUpdate = Time.time;
    }

    void Update()
    {
        if (IsProducing && timeSinceLastUpdate + interval <= Time.time)
        {
            switch (resourceType)
            {
                case ResourceType.Water:
                    resourceManager.AddWater();
                    break;
                case ResourceType.Wood:
                    resourceManager.AddWood();
                    break;
                default:
                    break;
            }

            timeSinceLastUpdate = Time.time;
        }
    }

    public void SetTransparency(int percentage)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, percentage / 100f);
    }
}
