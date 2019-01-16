using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GridManager grid;

    [SerializeField]
    private PathFinding pathfinder;

    [SerializeField]
    private GameObject soldier_prefab;



    /// <summary>
    /// List of spawnable objects. Logic for object spawning is defined in PlaceObject().
    /// </summary>
    private enum Spawnable
    {
        Wall,
        Soldier
    }

    // grid layer is een layer die ik heb gedefined in Unity en is het negende layer.
    // Door 1 negen plaatsen naar links te shiften krijg ik de negende layer.
    private readonly int grid_layer = 1 << 9;
    Vector3 destination;

    private void Start()
    {
        destination = grid.GetPositionFromCoordinate(new Vector2Int(0, 0));
    }

    private void Update()
    {
        RaycastHit hit;

        // Bouw een muur met linker muisknop
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, 100f, grid_layer))
            {
                PlaceObject(hit.point, Spawnable.Wall);
            }
        }

        // Bouw een soldier met rechter muisknop
        if (Input.GetMouseButtonDown(1))
        {
            // De ray moet in dit geval ook kunnen colliden met de soldier layer ( | is een or bitwise operator) 
            // Soldier layer is layer 10
            int layer_mask = (1 << 10) | grid_layer;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, 100f, layer_mask))
            {
                if (hit.transform.tag == "Soldier")
                {
                    // Delete de soldier als je er met rmb op klikt
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    PlaceObject(hit.point, Spawnable.Soldier);
                }
            }
        }

        // Zet de destination voor nieuwe gespawnde soldiers naar hit.point
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, 100f, grid_layer))
            {
                // Hoeft niet afgerond te worden, word al gedaan in Pathfinding class
                destination = hit.point;
            }
        }
    }

    /// <summary>
    /// Place a spawnable object in the world.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="spawnable"></param>
    private void PlaceObject(Vector3 position, Spawnable spawnable)
    {
        // Rond de muispositie af naar het centrum van de cell
        Vector3 new_position = grid.GetNearestPointOnGrid(position);
        // Converteer de positie naar een coordinate
        Vector2Int coordinate = grid.GetCoordinateFromPosition(new_position);
        // Als het coordinaat bezet is, stop dan met de functie (return)
        if (grid.IsOccupied[coordinate.x, coordinate.y]) { return; }

        // Als de cell nog vrij is
        switch (spawnable)
        {
            case Spawnable.Wall:
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new_position;
                cube.GetComponent<MeshRenderer>().material.color = Color.blue;
                grid.IsOccupied[coordinate.x, coordinate.y] = true;
                break;
            case Spawnable.Soldier:
                // Instantieer een nieuw kopie van de soldier prefab op de gegeven positie zonder rotatie
                GameObject new_soldier = Instantiate(soldier_prefab, new_position, Quaternion.identity);
                // Een reference naar het script van de net gespawnde soldier
                SoldierWalk soldier_walk = new_soldier.GetComponent<SoldierWalk>();
                // Zorg ervoor dat de grid en pathfinder gelinked zijn aan het terrein
                soldier_walk.grid = grid;
                soldier_walk.pathfinder = pathfinder;

                // Begin met lopen (Dit moet eruit maar voor het testen is het makkelijk dat hij direct begint)
                StartCoroutine(soldier_walk.MoveTo(destination));
                break;
            default:
                Debug.Log("Spawn type not yet implemented.");
                break;
        }
    }
}
