using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.Android;
using System;

public class LocationDisplay : MonoBehaviour
{
    public TextMeshPro coordinatesText; // 3D Text Mesh component
    public TextMeshPro hardcodedCoordinatesText; // Text for hardcoded coordinates
    public TextMeshPro distanceText; // Text for displaying the distance
    public TextMeshPro betaTestingText;

    // Reference to the 3D character GameObject
    public GameObject characterGameObject;

    // Hardcoded coordinates
    private double hardcodedLatitude = 33.6562176;
    private double hardcodedLongitude = 73.0161097;

    // Start is called before the first frame update
    void Start()
    {
        // Request location permissions from the user (Android specific)
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // Start location service after requesting permission
        Input.location.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Get the user's current coordinates
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;

            // Display the user's coordinates
            coordinatesText.text = $"User Coordinates: Lat: {latitude:F5}, Lon: {longitude:F5}";

            // Calculate distance between user and hardcoded coordinates
            double distance = CalculateDistance(latitude, longitude, hardcodedLatitude, hardcodedLongitude);

            // Display the distance
            distanceText.text = $"Distance: {distance:F2} meters";

            // Display the hardcoded coordinates
            hardcodedCoordinatesText.text = $"Hardcoded Coordinates: Lat: {hardcodedLatitude:F5}, Lon: {hardcodedLongitude:F5}";

            betaTestingText.text = "Ab text show ho raha ha ye?";

            if (distance <= 100.0)
            {
                // Show the 3D character by enabling its GameObject
                characterGameObject.SetActive(true);
            }
            else
            {
                // Hide the 3D character by disabling its GameObject
                characterGameObject.SetActive(false);
            }
        }
    }

    // Calculate distance in meters between two sets of coordinates
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Convert latitude and longitude values from degrees to radians
        lat1 *= Math.PI / 180.0;
        lon1 *= Math.PI / 180.0;
        lat2 *= Math.PI / 180.0;
        lon2 *= Math.PI / 180.0;

        // Earth's radius in meters (approximate)
        double radius = 6371000.0; // Earth's mean radius in meters

        // Haversine formula for calculating distance
        double dLat = lat2 - lat1;
        double dLon = lon2 - lon1;

        double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

        return radius * c;
    }
}
