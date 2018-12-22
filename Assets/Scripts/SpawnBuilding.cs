using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{

    [SerializeField]
    GameObject waterPrefab;

    [SerializeField]
    GameObject woodPrefab;

    [SerializeField]
    GameObject ballistaPrefab;

    [SerializeField]
    GameObject defensePrefab;

    GameObject prefabclone;

    [SerializeField]
    public ResourceManager resourceManager;

    private int BuildIndex;


    private int grid_layer = 1 << 9 | 1 << 11;

    void Update()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        if (GUIUtility.hotControl != 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && prefabclone != null)
        {
            prefabclone.GetComponent<ResourceScript>().SetTransparency(100);
            prefabclone.GetComponent<ResourceScript>().IsProducing = true;

            resourceManager.RemoveWood(3);
            resourceManager.RemoveWater(1);

            prefabclone = null;
            BuildIndex = 0;
        }

        if (BuildIndex == 0)
        {
            return;
        }

        Vector3 spawnPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, grid_layer))
        {
            if (hit.transform.tag == "WaterResource" || hit.transform.tag == "WoodResource")
            {
                return;
            }
            spawnPos = hit.point;
        }

        spawnPos.y += 0.2f;
        prefabclone.transform.position = spawnPos;
    }

    public void SetBuildIndex(int index)
    {
        Destroy(prefabclone);
        BuildIndex = index;

        switch (BuildIndex)
        {
            case 0:
                return;
            case 1:
                prefabclone = Instantiate(waterPrefab) as GameObject;
                prefabclone.GetComponent<ResourceScript>().resourceType = ResourceScript.ResourceType.Water;
                break;
            case 2:
                prefabclone = Instantiate(woodPrefab) as GameObject;
                prefabclone.GetComponent<ResourceScript>().resourceType = ResourceScript.ResourceType.Wood;
                break;
            case 3:
                prefabclone = Instantiate(ballistaPrefab) as GameObject;
                break;
            case 4:
                prefabclone = Instantiate(defensePrefab) as GameObject;
                break;
            default:
                Debug.LogError("Unexpected BuildIndex");
                break;
        }

        prefabclone.GetComponent<ResourceScript>().SetTransparency(50);
        prefabclone.GetComponent<ResourceScript>().resourceManager = resourceManager;
    }
}
