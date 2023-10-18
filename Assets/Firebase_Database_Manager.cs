using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Firebase_Database_Manager : MonoBehaviour
{
    DatabaseReference reference;
    string userId;

    private void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
            }
        });

        // Check if the user is already registered (by checking the user ID)
        userId = GetUserID();
        if (userId == null)
        {
            // User is not registered, so register them with a random user ID.
            userId = GenerateRandomUserID();
            RegisterUser(userId);
        }
    }

    private void Update()
    {
        // Update the user's online status based on user interaction.
        if (userId != null)
        {
            // Example: Set online status to true when the app is active, and false when it's not.
            UpdateOnlineStatus(userId, Application.isPlaying);
        }
    }

    private string GetUserID()
    {
        // Implement logic to retrieve the user's ID (e.g., from local storage).
        // If not found, return null to indicate that the user is not registered.
        return PlayerPrefs.GetString("UserID");
    }

    private string GenerateRandomUserID()
    {
        // Generate a random user ID (for example, a random 6-digit number).
        int randomID = Random.Range(100000, 999999); // Change the range as needed.
        return randomID.ToString();
    }

    private void RegisterUser(string userId)
    {
        // Save the user ID in local storage for future reference.
        PlayerPrefs.SetString("UserID", userId);
        PlayerPrefs.Save();

        // No need to store user name, since we're simplifying the database structure.

        // Create a user entry in the database with online status.
        reference.Child("users").Child(userId).SetValueAsync(true);
    }

    private void UpdateOnlineStatus(string userId, bool isOnline)
    {
        // Update the online status of the user in the database based on user interaction.
        reference.Child("users").Child(userId).SetValueAsync(isOnline);
    }
}
