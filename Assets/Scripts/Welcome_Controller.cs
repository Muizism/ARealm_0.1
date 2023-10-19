using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android; // Required for Android permissions

public class WelcomePageController : MonoBehaviour
{
    public GameObject welcomePageCanvas;
    public GameObject secondPageCanvas;
    public float delayInSeconds = 2.0f;

    private void Start()
    {
        // Hide the second page canvas initially
        welcomePageCanvas.SetActive(true);
        secondPageCanvas.SetActive(false);

        // Request location permissions from the user (Android specific)
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // Start location service after requesting permission (you may want to handle other platforms)
        Input.location.Start();

        // Use Invoke to call a method to transition to the second page after the specified delay.
        Invoke("ShowSecondPage", delayInSeconds);
    }

    private void ShowSecondPage()
    {
        // Disable the welcome page canvas and enable the second page canvas.
        welcomePageCanvas.SetActive(false);
        secondPageCanvas.SetActive(true);
    }
}
