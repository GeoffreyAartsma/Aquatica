using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GridManager grid;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    PathFinding pathfinder;

    public void SpawnEnemy(bool retry = false)
    {
        Vector2Int coord = Vector2Int.zero;

        switch (Random.Range(0, 4))
        {
            case 0: // North
                coord = new Vector2Int(Random.Range(0, grid.GridSize - 1), grid.GridSize - 1);
                break;
            case 1: // East
                coord = new Vector2Int(grid.GridSize - 1, Random.Range(0, grid.GridSize - 1));
                break;
            case 2: // South
                coord = new Vector2Int(Random.Range(0, grid.GridSize - 1), 0);
                break;
            case 3: // West
                coord = new Vector2Int(0, Random.Range(0, grid.GridSize - 1));
                break;

            default:
                break;
        }

        if (!grid.IsOccupied[coord.x, coord.y])
        {
            Vector3 position = grid.GetPositionFromCoordinate(coord);
            // Instantieer een nieuw kopie van de soldier prefab op de gegeven positie zonder rotatie
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            // Een reference naar het script van de net gespawnde soldier
            SoldierWalk enemyWalk = enemy.GetComponent<SoldierWalk>();
            // Zorg ervoor dat de grid en pathfinder gelinked zijn aan het terrein
            enemyWalk.grid = grid;
            enemyWalk.pathfinder = pathfinder;

            Vector3 closestBuildingPos = Vector3.zero;
            float closestDistance = float.MaxValue;
            GameObject[] buildings = FindGameObjectsWithBuildingLayer();
            
            foreach (GameObject building in buildings) {
                float distance = Vector3.Distance(position, building.transform.position);
                if (distance < closestDistance) {
                    closestBuildingPos = building.transform.position;
                    closestDistance = distance;
                }
            }
            
            StartCoroutine(enemyWalk.MoveTo(closestBuildingPos));
        }
        else
        {
            if (retry)
            {
                Debug.LogError("All spaces are occupied!");
                return;
            }
            SpawnEnemy(true);
        }
    }

    GameObject[] FindGameObjectsWithBuildingLayer()
    {
        List<GameObject> buildingList = new List<GameObject>();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects) {
            // Building layer
            if (go.activeInHierarchy && go.layer == 12)
                buildingList.Add(go);
        }

        if (buildingList.Count == 0) {
            return null;
        }

        return buildingList.ToArray();
    }
}

