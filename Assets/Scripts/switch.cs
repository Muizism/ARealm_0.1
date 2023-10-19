using UnityEngine;
using UnityEngine.UI;

public class CanvasNavigation : MonoBehaviour
{
    public GameObject canvasToHide; // Reference to the canvas to hide (Canvas1).
    public GameObject canvasToShow; // Reference to the canvas to show (Canvas2).

    // Reference to the UI Button that triggers the navigation.
    public Button button;

    private void Start()
    {
        // Attach the button click event to the NavigateToCanvas method.
        button.onClick.AddListener(NavigateToCanvas);
    }

    public void NavigateToCanvas()
    {
        // Deactivate the initial canvas.
        canvasToHide.SetActive(false);

        // Activate the destination canvas.
        canvasToShow.SetActive(true);
    }
}
