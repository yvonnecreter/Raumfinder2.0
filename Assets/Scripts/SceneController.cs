using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static string SelectedRoomName; // Static variable to store the selected room name

    // Switch to a specified scene
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Switch to OpenARScreen with the selected room name
    public void SwitchToNavigationScreen()
    {
        SelectedRoomName = this.GetComponentInChildren<TMP_Text>().text; // Store the room name
        SceneManager.LoadScene("CameraScreen_v2"); // Load the OpenARScreen scene
    }
}