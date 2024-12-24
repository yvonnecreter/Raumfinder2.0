using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDatabase", menuName = "Room Management/Room Database", order = 1)]
public class RoomDatabase : ScriptableObject
{
    public List<string> roomNames; // Store the list of room names
}