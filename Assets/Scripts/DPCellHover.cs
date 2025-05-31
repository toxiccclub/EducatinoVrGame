using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DPCellHover : MonoBehaviour
{
    private TextMeshProUGUI cellText;
    private Color originalColor;
    public Color hoverColor = Color.cyan;
    private Renderer rend;

    private void Start()
    {
        cellText = GetComponent<TextMeshProUGUI>();
        rend = GetComponentInChildren<Renderer>();
        if (cellText != null)
            originalColor = cellText.color;
    }

    public void OnHoverEntered()
    {
        if (cellText != null)
            cellText.color = hoverColor;
    }

    public void OnHoverExited()
    {
        if (cellText != null)
            cellText.color = originalColor;
    }
}
