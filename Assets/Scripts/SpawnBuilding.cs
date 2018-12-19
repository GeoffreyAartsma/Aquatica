using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour {

    [SerializeField]
    GameObject prefab;
    
    GameObject prefabclone;

    public bool spawnPrefab;

    private int grid_layer = 1 << 9 | 1 << 11;

    void Update()
    {
        SpawnPrefab();   
    }

    public void SpawnPrefab()
    {
        if (Input.GetMouseButton(0))
        {
            if (spawnPrefab == false)
            {
                return;
            }

            if (GUIUtility.hotControl != 0)   
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
            

            if (prefabclone == null)
            {
                prefabclone = Instantiate(prefab, spawnPos, Quaternion.identity) as GameObject;
            }
            
            prefabclone.transform.position = spawnPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            prefabclone = null;
        }
    }

    public void ToggleBuildMode()
    {
        spawnPrefab = !spawnPrefab;
    } 
}
