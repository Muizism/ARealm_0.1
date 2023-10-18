using UnityEngine;
using UnityEngine.UI;

public class RoundedButton : MonoBehaviour
{
    public Image background;
    public Text buttonText;

    [Header("Button Settings")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color clickColor = Color.black;
    public float buttonSizeMultiplier = 1.05f;

    // Rounded corner radius
    public float cornerRadius = 20f;

    private bool isHovered = false;

    private void Start()
    {
        // Set initial button appearance
        SetButtonAppearance(normalColor);

        // Add button click listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        SetButtonAppearance(hoverColor, true);
    }

    private void OnMouseExit()
    {
        isHovered = false;
        SetButtonAppearance(normalColor);
    }

    private void OnButtonClick()
    {
        SetButtonAppearance(clickColor);
        // Handle button click behavior here
    }

    private void SetButtonAppearance(Color color, bool highlight = false)
    {
        background.color = color;
        background.type = highlight ? Image.Type.Sliced : Image.Type.Simple;

        if (highlight)
        {
            // Adjust the button size when hovering
            background.rectTransform.localScale = Vector3.one * buttonSizeMultiplier;
        }
        else
        {
            background.rectTransform.localScale = Vector3.one;
        }

        // Round the button's corners
        background.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, background.rectTransform.rect.width);
        background.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, background.rectTransform.rect.height);

        if (cornerRadius > 0f)
        {
            background.sprite = null;
            background.material = new Material(Shader.Find("UI/Default"));
            background.material.color = color;
            background.material.SetFloat("_CornerRadius", cornerRadius);
        }
    }
}
