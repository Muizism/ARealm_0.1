

using UnityEngine;
using UnityEngine.Android; // For Android permissions

public class LocationManager : MonoBehaviour
{
    public Vector2 hardcodedLocation = new Vector2(32.5925325f, 74.0735243f); // Example coordinates
    private Vector2 userLocation; // User's real device location

    public Vector2 GetUserLocation()
    {
        return userLocation;
    }

    public float CalculateDistance(Vector2 userLocation, Vector2 hardcodedLocation)
    {
        float earthRadius = 6371; // Earth's radius in kilometers
        float lat1 = userLocation.x;
        float lon1 = userLocation.y;
        float lat2 = hardcodedLocation.x;
        float lon2 = hardcodedLocation.y;

        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float distance = earthRadius * c;

        return distance;
    }


    private void Start()
    {
        // Request location permissions from the user (Android specific)
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // Start location service after requesting permission
        Input.location.Start();
    }

    private void Update()
    {
        // Check if location service is running and has valid data
        if (Input.location.status == LocationServiceStatus.Running && Input.location.lastData.timestamp > 0)
        {
            // Get the user's real device location
            userLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
        else
        {
            // Handle the case where the location service is not running or has no valid data
            Debug.LogError("Location service is not providing valid data.");
        }
    }

    private void OnDestroy()
    {
        // Stop the location service when the script is destroyed
        Input.location.Stop();
    }
}

