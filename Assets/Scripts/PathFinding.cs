using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple breadth first pathfinder
/// </summary>
public class PathFinding : MonoBehaviour
{
    GridManager grid;

    private void Start()
    {
        // Grid en pathfinder moeten op hetzelfde game object zitten
        grid = GetComponent<GridManager>();
    }

    /// <summary>
    /// Create a path of nodes to the destination coordinate using BFS to avoid obstructed coordinates.
    /// </summary>
    /// <param name="start_position"></param>
    /// <param name="target_position"></param>
    /// <returns>List of coordinates from the start position to the destination. Excluding the start coordinate itself.</returns>
    public IList<Vector2Int> FindPath(Vector3 start_position, Vector3 target_position)
    {
        // Note: node en coordinate zijn hetzelfde. Ik moet mijn benaming nog even fixen maar ik ben er nog niet over uit

        // Zet de world positie om in locale grid coordinaten
        Vector2Int start_node = grid.GetCoordinateFromPosition(start_position);
        Vector2Int target_node = grid.GetCoordinateFromPosition(target_position);

        // Queue die nodes afgaat die gechecked moeten worden
        Queue<Vector2Int> nodes_to_check = new Queue<Vector2Int>();
        // Hashset (een datatype geoptimaliseerd om snel te zoeken) die bijhoud wat we al hebben bezocht
        HashSet<Vector2Int> explored_nodes = new HashSet<Vector2Int>();
        // Deze dictionary houdt bij welke node wat als parent heeft. Het eerste coordinaat(Vector2Int) is een key.
        // Een key is een object dat gebruikt wordt om te zoeken naar een ander object(de value). In dit geval is dat 
        // het coordinaat van zijn parent (Vector2Int)
        Dictionary<Vector2Int, Vector2Int> parent_nodes = new Dictionary<Vector2Int, Vector2Int>();

        // Voeg de start node toe aan de queue. Anders is de queue leeg
        nodes_to_check.Enqueue(start_node);
        // Zolang we nog nodes hebben om te zoeken
        while (nodes_to_check.Count > 0)
        {
            // Krijg de node vooraan de queue en delete hem uit de queue
            Vector2Int current_node = nodes_to_check.Dequeue();
            if (current_node == target_node)
            {
                // Als de target coordinaat gevonden is dan moet het pad worden gereconstructueerd.
                // Dit wordt gedaan door alle nodes af te gaan naar hun parent en ze dan toe te voegen 
                // aan een nieuwe lijst. Deze lijst wordt dan gereturned.
                List<Vector2Int> path = new List<Vector2Int>();

                while (current_node != start_node)
                {
                    // Voeg de node die we nu behandelen toe aan het pad
                    path.Add(current_node);
                    // En zoek met de current_node als key de parent in de dictionary
                    current_node = parent_nodes[current_node];
                }

                // Het pad is eerst van eind punt naar begin. Moet even worden omgedraaid.
                path.Reverse();
                return path;
            }

            IList<Vector2Int> nodes = GetWalkableNodes(current_node);
            foreach (Vector2Int child_node in nodes)
            {
                if (!explored_nodes.Contains(child_node))
                {
                    // Mark the node as explored
                    explored_nodes.Add(child_node);
                    // Store a reference to the previous node
                    parent_nodes.Add(child_node, current_node);
                    // Add this to the queue of nodes to examine
                    nodes_to_check.Enqueue(child_node);
                }
            }
        }

        // Return null als geen pad gevonden is (dus het pad compleet geblokeerd is)
        return null;
    }

    /// <summary>
    /// Get all the nodes around the given node that are walkable.
    /// </summary>
    /// <param name="source_node"></param>
    /// <returns>List of all possible nodes to walk on.</returns>
    IList<Vector2Int> GetWalkableNodes(Vector2Int source_node)
    {
        // Maak eerst een lijst met alle mogelijke coordinaten
        IList<Vector2Int> possible_nodes = new List<Vector2Int>() {
            new Vector2Int (source_node.x + 1, source_node.y),
            new Vector2Int (source_node.x - 1, source_node.y),
            new Vector2Int (source_node.x, source_node.y + 1),
            new Vector2Int (source_node.x, source_node.y - 1)
            // Voor nu geen diagonaal (Anders zou hij tussen twee diagonaal geplaatste muren heen kunnen)
            //,
            //new Vector2Int (source.x + 1, source.y + 1),
            //new Vector2Int (source.x + 1, source.y - 1),
            //new Vector2Int (source.x - 1, source.y + 1),
            //new Vector2Int (source.x - 1, source.y - 1)
        };

        // Check of de coordinaten vallen binnen het grid en of de positie niet al is bezet
        IList<Vector2Int> walkable_nodes = new List<Vector2Int>();
        for (int i = 0; i < possible_nodes.Count; i++)
        {
            if (possible_nodes[i].x >= 0 && possible_nodes[i].y >= 0 &&
                possible_nodes[i].x < grid.GridSize && possible_nodes[i].y < grid.GridSize &&
                !grid.IsOccupied[possible_nodes[i].x, possible_nodes[i].y])
            {
                // Voeg de node toe als hij nog vrij is
                walkable_nodes.Add(possible_nodes[i]);
            }
        }

        return walkable_nodes;
    }
}
