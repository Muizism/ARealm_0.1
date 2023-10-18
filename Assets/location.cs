using UnityEngine;
using UnityEngine.UI;

public class LocationDistanceCalculator : MonoBehaviour
{
    public TMPro.TextMeshProUGUI distanceText;

    private Vector2 targetCoordinates = new Vector2(33.6558874f, 73.0165409f);

    private void Update()
    {
        if (Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running)
        {
            float userLatitude = Input.location.lastData.latitude;
            float userLongitude = Input.location.lastData.longitude;

            float distance = CalculateDistance(userLatitude, userLongitude, targetCoordinates);

            distanceText.text = "Distance: " + distance.ToString("F2") + " meters";
        }
    }

    private float CalculateDistance(float userLat, float userLong, Vector2 target)
    {
        float latDistance = Mathf.Deg2Rad * (userLat - target.x);
        float lonDistance = Mathf.Deg2Rad * (userLong - target.y);

        float a = Mathf.Sin(latDistance / 2) * Mathf.Sin(latDistance / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * userLat) * Mathf.Cos(Mathf.Deg2Rad * target.x) *
                  Mathf.Sin(lonDistance / 2) * Mathf.Sin(lonDistance / 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return c * 6371000; // Earth radius in meters
    }
}
