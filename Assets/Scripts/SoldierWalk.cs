using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWalk : MonoBehaviour
{
    public GridManager grid;
    public PathFinding pathfinder;

    public IEnumerator MoveTo(Vector3 destination)
    {
        IList<Vector2Int> path = pathfinder.FindPath(transform.position, destination);
        if (path == null) {
            Debug.LogError("Path is obstructed!");
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < path.Count; i++) {
            transform.position = grid.GetPositionFromCoordinate(path[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
