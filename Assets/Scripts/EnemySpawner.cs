using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GridManager grid;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("grid width: " + grid.GridSize);
            SpawnEnemy();
        }
    }

    void SpawnEnemy(bool retry = false)
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
            // Cube wordt gespawnd
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // Positie van cube wordt bepaald door de coordinate die hierboven wordt aangegeven
            cube.transform.position = grid.GetPositionFromCoordinate(coord);
            // Cube wordt blauw gemaakt
            cube.GetComponent<MeshRenderer>().material.color = Color.blue;
            // Coordinate wordt bezet
            grid.IsOccupied[coord.x, coord.y] = true;
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

