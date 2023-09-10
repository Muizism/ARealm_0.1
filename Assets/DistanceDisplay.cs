using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceDisplay : MonoBehaviour
{
    public Transform userLocation; // Reference to the user's AR camera or a GPS object
    public Transform hardcodedLocation; // Reference to the AR object representing the hardcoded location
    public TextMeshPro distanceText; // Reference to the TextMeshPro component

    private LocationManager locationManager; // Reference to the LocationManager script

    private void Start()
    {
        locationManager = FindObjectOfType<LocationManager>(); // Find the LocationManager script in the scene
    }

    private void Update()
    {
        if (locationManager == null)
        {
            Debug.LogError("LocationManager not found. Make sure it exists in the scene.");
            return;
        }

        Vector2 userPos = locationManager.GetUserLocation(); // Get the user's real-time location
        Vector2 hardcodedPos = new Vector2(32.5925325f, 74.0735243f);

        // Calculate the distance between user and hardcoded locations
        float distance = locationManager.CalculateDistance(userPos, hardcodedPos);

        if (float.IsNaN(distance))
        {
            Debug.LogWarning("Distance calculation failed.");
            // Handle the case where distance is not a valid number (e.g., due to calculation errors)
            distanceText.text = "Distance not available";
        }
        else
        {
            // Display user's and hardcoded coordinates on the screen in real-time
            distanceText.text = $"User Coordinates: {userPos}\nHardcoded Coordinates: {hardcodedPos}\nDistance: {distance} km";
        }
    }
}
