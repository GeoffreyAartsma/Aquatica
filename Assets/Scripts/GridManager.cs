using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int grid_size;

    [SerializeField]
    private float cell_size;

    /// <summary>
    /// Matrix of booleans holding if a coordinate is obstructed or not.
    /// </summary>
    public bool[,] IsOccupied { get; set; }

    /// <summary>
    /// Get the grid size of this instance. With one cell being one unit.
    /// </summary>
    public int GridSize
    {
        get
        {
            return this.grid_size;
        }
    }

    private void Awake()
    {
        // De keer 10 is omdat de plane in unity bij default een grote heeft van 10.
        // Dus een scale van 1 geeft een plane met een grote van 10 x 10
        grid_size = Mathf.RoundToInt(transform.localScale.x * 10f / cell_size);
        IsOccupied = new bool[grid_size, grid_size];

        GetComponent<MeshRenderer>().sharedMaterial.SetInt("_GridSize", grid_size);
        GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_CellSize", cell_size);
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        grid_size = Mathf.RoundToInt(transform.localScale.x * 10f / cell_size);
        GetComponent<MeshRenderer>().sharedMaterial.SetInt("_GridSize", grid_size);
        GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Cellsize", cell_size);
    }

    /// <summary>
    /// Get grid world position of the grid point that is closest to the given position.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>World position of the cell closest to the given position.</returns>
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        // Verwijder enig mogelijke offset
        position -= transform.position;

        // Rond de positie af naar beneden
        float nearest_x = (int)position.x;
        float nearest_z = (int)position.z;

        // Als de positie een negatief getal is moet de helft van de cell size worden
        // afgetrokken in plaats van opgeteld.
        if (position.x < 0) { nearest_x -= cell_size / 2f; }
        else { nearest_x += cell_size / 2f; }

        if (position.z < 0) { nearest_z -= cell_size / 2f; }
        else { nearest_z += cell_size / 2f; }

        // Zet de floats om in een vector3 en voeg een eventuele offset weer toe.
        Vector3 result = new Vector3(nearest_x, transform.position.y, nearest_z);
        return result + transform.position;
    }

    /// <summary>
    /// Convert Vector3 from world space to grid space as a coordinate.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Coordinate of the nearest grid cell.</returns>
    public Vector2Int GetCoordinateFromPosition(Vector3 position)
    {
        // Rond voor de zekerheid nog een keer af. Is niet per se nodig
        // maar mocht het object niet helemaal perfect gealigned zijn kan het voor
        // problemen zorgen.
        Vector3 rounded = GetNearestPointOnGrid(position);
        // Zet de offset weer terug
        rounded -= transform.position;
        // Dank je wel Wolfram voor de versimpeling van mijn functies
        return new Vector2Int((int)((grid_size - cell_size) / 2f + rounded.x),
                              (int)((grid_size - cell_size) / 2f + rounded.z));
    }

    /// <summary>
    /// Convert coordinate from local grid space to a world vector.
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns>Vector of the world position of the grid cell.</returns>
    public Vector3 GetPositionFromCoordinate(Vector2Int coordinate)
    {
        // Dit doet precies het tegen overgestelde van GetCoordinateFromPosition
        // Dank je wel Wolfram voor de versimpeling van mijn functies
        Vector3 position = new Vector3
        {
            x = 0.5f * (-grid_size + cell_size + 2 * coordinate.x),
            y = transform.position.y,
            z = 0.5f * (-grid_size + cell_size + 2 * coordinate.y)
        };

        return position + transform.position;
    }
}
