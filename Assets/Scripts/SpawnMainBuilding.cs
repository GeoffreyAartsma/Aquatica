using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMainBuilding : MonoBehaviour
{
    GridManager grid;

    [SerializeField]
    GameObject mainbuilding;

    // Use this for initialization
    void Start()
    {
        grid = GetComponent<GridManager>();
        Vector2Int coord = grid.GetCoordinateFromPosition(Vector3.zero);
        grid.IsOccupied[coord.x, coord.y] = true;
        Instantiate(mainbuilding, grid.GetPositionFromCoordinate(coord), Quaternion.identity);
    }
}
