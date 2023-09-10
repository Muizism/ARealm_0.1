using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlacement : MonoBehaviour
{
    public Transform userLocation; // Reference to the user's AR camera or a GPS object
    public Transform hardcodedLocation; // Reference to the AR object representing the hardcoded location
    public GameObject capsule; // Reference to the capsule object

    private ARRaycastManager raycastManager;
    private Vector3 capsuleOffset = new Vector3(0, 0.5f, 0); // Adjust this to align the capsule with the ground

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        capsule.SetActive(false); // Initially, hide the capsule
    }

    void Update()
    {
        // Calculate the distance between user and hardcoded locations
        float distance = Vector3.Distance(userLocation.position, hardcodedLocation.position);

        // Check if the user is within a 100-meter radius
        if (distance <= 100f)
        {
            // Display the capsule at the hardcoded location
            PlaceCapsuleAtLocation(hardcodedLocation.position);
        }
        else
        {
            // Hide the capsule if outside the radius
            capsule.SetActive(false);
        }
    }

    private void PlaceCapsuleAtLocation(Vector3 position)
    {
        // Create a ray from the user's camera
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

        List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List to store raycast hits

        // Perform an AR raycast to find a suitable placement for the capsule
        if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the hit pose from the first hit in the list
            Pose hitPose = hits[0].pose;

            // Place the capsule at the hit pose with an offset
            capsule.transform.position = hitPose.position + capsuleOffset;
            capsule.SetActive(true);
        }
    }
}
