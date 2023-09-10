using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SendUserCoordinates(double latitude, double longitude)
    {
        if (databaseReference != null)
        {
            // Create a new entry in the database with the user's coordinates
            DatabaseReference userLocationRef = databaseReference.Child("user_locations");
            userLocationRef.Child("user_id").SetValueAsync(new { latitude, longitude });
        }
    }
}
