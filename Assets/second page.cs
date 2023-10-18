using UnityEngine;
using UnityEngine.UI;

public class CloseButtonController : MonoBehaviour
{
    public Button closeButton;
    public GameObject secondPageCanvas; // Reference to the canvas or object of the second page

    private void Start()
    {
        // Attach a click event listener to the "Close" button
        closeButton.onClick.AddListener(CloseSecondPage);
    }

    private void CloseSecondPage()
    {
        // Deactivate the second page canvas or object
        secondPageCanvas.SetActive(false);
    }
}
