using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchController : MonoBehaviour
{
    public RoomDatabase roomDatabase;
    public TMP_InputField searchInputField; // Use TMP_InputField for TextMeshPro    
    public GameObject suggestionParent; // Parent object to hold suggestion buttons
    public GameObject suggestionPrefab; // Prefab for suggestion buttons

    private List<GameObject> activeSuggestions = new List<GameObject>(); // Track active suggestion objects

    void Start()
    {
        if (searchInputField != null)
        {
            searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
        }
    }

    // Method to handle value changes in the search input field
    public void OnSearchValueChanged(string searchQuery)
    {
        ClearSuggestions();

        if (string.IsNullOrEmpty(searchQuery)) return;

        // Get the top 4 matching room names
        List<string> suggestions = GetTopSuggestions(searchQuery);

        // Display the suggestions
        foreach (string suggestion in suggestions)
        {
            AddSuggestionButton(suggestion);
        }
    }

    // Get the top 4 room names that match the input
    private List<string> GetTopSuggestions(string searchQuery)
    {
        List<string> suggestions = new List<string>();

        foreach (string roomName in roomDatabase.roomNames)
        {
            if (roomName.ToLower().Contains(searchQuery.ToLower()))
            {
                suggestions.Add(roomName);

                if (suggestions.Count >= 4)
                {
                    break; // Limit to top 4 results
                }
            }
        }

        return suggestions;
    }

    // Create and add a suggestion button
    private void AddSuggestionButton(string suggestion)
    {
        GameObject suggestionButton = Instantiate(suggestionPrefab, suggestionParent.transform);
        suggestionButton.GetComponentInChildren<TMP_Text>().text = suggestion;
        suggestionButton.GetComponent<Button>().onClick.AddListener(() => OnSuggestionSelected(suggestion));
        activeSuggestions.Add(suggestionButton);
    }

    // Handle when a suggestion is selected
    private void OnSuggestionSelected(string roomName)
    {
        searchInputField.text = roomName; // Update the input field
        ClearSuggestions(); // Clear the suggestions
        Debug.Log($"Room '{roomName}' selected.");
        // Additional logic to focus or highlight the room in your scene can go here
    }

    // Clear all active suggestions
    private void ClearSuggestions()
    {
        foreach (GameObject suggestion in activeSuggestions)
        {
            Destroy(suggestion);
        }
        activeSuggestions.Clear();
    }
}