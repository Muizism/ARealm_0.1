using UnityEngine;
using UnityEngine.UI;

public class CloseButtonController : MonoBehaviour
{
    public Image closeButton; // Reference to the Image component
    public GameObject secondPageCanvas; // Reference to the canvas or object of the second page

    private void Start()
    {
        // Attach a click event listener to the "Close" image
        closeButton.GetComponent<Button>().onClick.AddListener(CloseSecondPage);
    }

    private void CloseSecondPage()
    {
        // Deactivate the second page canvas or object
        secondPageCanvas.SetActive(false);
    }
}
