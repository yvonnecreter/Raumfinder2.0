using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    // Dictionary to store room names and their corresponding GameObjects
    private Dictionary<string, GameObject> roomTags = new Dictionary<string, GameObject>();
    public GameObject originPrefab;
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

        // get selected room
        string selectedRoom = SceneController.SelectedRoomName;

        // spawn origin OR no selected room found
        if (!string.IsNullOrEmpty(selectedRoom))
        {
            SpawnTargetAtRoom(selectedRoom);
        }
        else
        {
            Debug.LogWarning("No room was selected before opening this scene.");
        }
    }

    // spawn origin at camera
    public void SpawnTargetAtRoom(string roomName)
    {
        if (roomTags.ContainsKey(roomName))
        {
            // set origin to camera
            GameObject origin = Instantiate(originPrefab, camera.transform);

            // create path from origin to target
            NavLineRenderer lineRenderer = origin.GetComponent<NavLineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.target = roomTags[roomName].transform;
            }
        }
        else
        {
            Debug.LogWarning($"Room with name {roomName} not found.");
        }
    }
}