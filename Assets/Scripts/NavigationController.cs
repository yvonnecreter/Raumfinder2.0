using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    // Dictionary to store room names and their corresponding GameObjects
    private Dictionary<string, GameObject> roomTags = new Dictionary<string, GameObject>();
    public GameObject originPrefab;
    private GameObject originGameObject;
    public GameObject camera;
    public GameObject roomsParent;

    void Start()
    {
        // populate dictionary with rooms
        for (int i = 0; i < roomsParent.transform.childCount; i++) //iterate through children
        {
            Transform child = roomsParent.transform.GetChild(i);
            // filter for room tags
            if (child.CompareTag("Room"))
            {
                roomTags[child.name] = child.gameObject; // add to dictionary
            }
        }
    }


    // spawn origin at camera
    public void SpawnTargetAtRoom(string roomName)
    {
        if (roomTags.ContainsKey(roomName))
        {
            // set origin to camera
            GameObject origin = Instantiate(originPrefab, camera.transform.position, Quaternion.identity);
            NavLineRenderer lineRenderer = origin.GetComponent<NavLineRenderer>();
            if (lineRenderer && this.roomTags[roomName])
            {
                lineRenderer.target = this.roomTags[roomName].transform;
                lineRenderer.startNavMesh();
            }
            else
            {
                Debug.LogWarning("LineRenderer or target is null.");
            }
            originGameObject = origin;
            // snap to navmesh
            /* UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(origin.transform.position, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                origin.transform.position = hit.position;
            } */

            // wait for one frame, then set target
            /* StartCoroutine(InitializePath(origin, roomTags[roomName].transform)); */
        }
        else
        {
            Debug.LogWarning($"Room with name {roomName} not found.");
        }
    }

    public void RecalibratePosition()
    {
        if (!string.IsNullOrEmpty(SceneController.SelectedRoomName))
        {
            Destroy(originGameObject);
            SpawnTargetAtRoom(SceneController.SelectedRoomName);
        }
        else
        {
            Debug.LogWarning("No room was selected before opening this scene.");
        }
    }
}