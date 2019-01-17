using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    float dps;

    [SerializeField]
    float attackRange;

    void Update()
    {
        GameObject[] buildingList = FindGameObjectsWithBuildingLayer();

        foreach (GameObject building in buildingList)
        {
            if (Vector3.Distance(transform.position, building.transform.position) < attackRange)
            {

                building.GetComponent<BuildingHealth>().TakeDamage(dps * Time.deltaTime);
            }
        }
    }

    GameObject[] FindGameObjectsWithBuildingLayer()
    {
        List<GameObject> buildingList = new List<GameObject>();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            // Building layer
            if (go.activeInHierarchy && go.layer == 12)
                buildingList.Add(go);
        }

        if (buildingList.Count == 0)
        {
            return null;
        }

        return buildingList.ToArray();
    }
}
