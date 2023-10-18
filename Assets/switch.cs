using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public Canvas canvas1; // Reference to the initial canvas.
    public Canvas canvas2; // Reference to the canvas to switch to.

    private bool isCanvas1Active = true;

    private void Start()
    {
        // Ensure the initial canvas is active, and the other is not.
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }

    public void SwitchCanvas()
    {
        // Toggle the active state of the canvases.
        isCanvas1Active = !isCanvas1Active;
        canvas1.gameObject.SetActive(isCanvas1Active);
        canvas2.gameObject.SetActive(!isCanvas1Active);
    }
}
