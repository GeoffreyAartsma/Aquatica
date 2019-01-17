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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("grid width: " + grid.GridSize);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy(bool retry = false)
    {
        Vector2Int coord = Vector2Int.zero;

        switch (Random.Range(0, 4))
        {
            case 0: // North
                Debug.Log("NORTH");
                coord = new Vector2Int(Random.Range(0, grid.GridSize - 1), grid.GridSize - 1);
                break;
            case 1: // East
                coord = new Vector2Int(grid.GridSize - 1, Random.Range(0, grid.GridSize - 1));
                Debug.Log("EAST");
                break;
            case 2: // South
                coord = new Vector2Int(Random.Range(0, grid.GridSize - 1), 0);
                Debug.Log("SOUTH");
                break;
            case 3: // West
                coord = new Vector2Int(0, Random.Range(0, grid.GridSize - 1));
                Debug.Log("WEST");
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
            Vector3 destination = Vector3.zero;
            StartCoroutine(enemyWalk.MoveTo(destination));
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
}

