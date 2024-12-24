using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Include this namespace for IEnumerator

public class NavLineRenderer : MonoBehaviour
{
    public LineRenderer line; // Holds the LineRenderer component
    public Transform target; // Holds the target's transform
    public NavMeshAgent agent; // Holds the NavMeshAgent

    void Start()
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

    private IEnumerator GetPath()
    {
        // Set the destination for the agent
        agent.SetDestination(target.position);

        // Wait for the path calculation to complete
        yield return new WaitForEndOfFrame();

        // Draw the path
        DrawPath(agent.path);

        // Stop the agent if you don't want it to move
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