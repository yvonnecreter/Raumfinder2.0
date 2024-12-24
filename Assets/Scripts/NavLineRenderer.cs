using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Include this namespace for IEnumerator

public class NavLineRenderer : MonoBehaviour
{
    public LineRenderer line; // Holds the LineRenderer component
    public Transform target; // Holds the target's transform
    public NavMeshAgent agent; // Holds the NavMeshAgent

    /*  private void Start()
     {
         startNavMesh();
     } */

    public void startNavMesh()
    {
        // Get the LineRenderer and NavMeshAgent components
        line = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();

        // Ensure the LineRenderer is set up properly
        line.positionCount = 0;

        // Start generating the path
        StartCoroutine(GetPath());

        // Prevent agent from pushed to floor
        agent.updatePosition = false; // Prevents NavMeshAgent from overriding the object's position
        agent.updateUpAxis = false;   // Prevents NavMeshAgent from adjusting the object's height
    }

    private Vector3 GetValidNavMeshPosition(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.LogWarning("Target position is not on the NavMesh.");
            return position; // Fallback to the original position
        }
    }

    private IEnumerator GetPath()
    {
        Vector3 validTargetPosition = GetValidNavMeshPosition(target.position);
        agent.SetDestination(validTargetPosition);

        // Wait until the path is calculated
        while (agent.pathPending)
        {
            yield return null;
        }

        if (agent.hasPath)
        {
            DrawPath(agent.path);
        }
        else
        {
            Debug.LogWarning("Path could not be generated.");
        }

        agent.isStopped = true;
    }

    private void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) // If the path has 1 or no corners, return
            return;

        // Set the number of positions in the LineRenderer
        line.positionCount = path.corners.Length;

        // Loop through corners and set the positions on the LineRenderer
        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
    }
}