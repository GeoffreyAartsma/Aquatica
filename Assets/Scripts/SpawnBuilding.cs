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
    GameObject ballistaSlowPrefab;

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
        // Don't place a building if we are trying to press a button on the GUI
        // or ff no building is selected
        if (GUIUtility.hotControl != 0 || prefabclone == null)
        {
            return;
        }

        RaycastHit hit;
        Vector3 spawnPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, grid_layer))
        {
            if (hit.transform.tag == "WaterResource" || hit.transform.tag == "WoodResource")
            {
                return;
            }

            spawnPos = hit.point;
            spawnPos.y += 0.4f;
            prefabclone.transform.position = spawnPos;
        }
        else
        {
            // Not clicking on grid so stop trying to place object
            return;
        }

        // If we are placing a building and press the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Try to buy the building
            if (resourceManager.RemoveWood(3) == false ||
                resourceManager.RemoveWater(1) == false)
            {
                Destroy(prefabclone);
                return;
            }

            if (BuildIndex == 1 || BuildIndex == 2)
            {
                // Set transparency back to 100%
                prefabclone.GetComponent<ResourceScript>().SetTransparency(100);
                // Start producing wood or water
                prefabclone.GetComponent<ResourceScript>().IsProducing = true;
            }

            // Empty the selected prefab
            prefabclone = null;
            BuildIndex = 0;
        }
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

                prefabclone.GetComponent<ResourceScript>().SetTransparency(50);
                prefabclone.GetComponent<ResourceScript>().resourceManager = resourceManager;
                break;
            case 2:
                prefabclone = Instantiate(woodPrefab) as GameObject;
                prefabclone.GetComponent<ResourceScript>().resourceType = ResourceScript.ResourceType.Wood;

                prefabclone.GetComponent<ResourceScript>().SetTransparency(50);
                prefabclone.GetComponent<ResourceScript>().resourceManager = resourceManager;
                break;
            case 3:
                prefabclone = Instantiate(ballistaPrefab) as GameObject;
                break;
            case 4:
                prefabclone = Instantiate(ballistaSlowPrefab) as GameObject;
                break;
            default:
                Debug.LogError("Unexpected BuildIndex");
                break;
        }
    }
}
