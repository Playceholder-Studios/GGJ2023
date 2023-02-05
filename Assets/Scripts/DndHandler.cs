using TMPro;
using UnityEngine;

/// <summary>
/// Drag and drop handler
/// </summary>
public class DndHandler : MonoBehaviour
{
    public static DndHandler Instance { get; private set; }
    public GameObject previouslyDraggedObject;
    public GameObject toolTipObject;
    public TextMeshProUGUI toolTipTitle;
    public TextMeshProUGUI toolTipDescription;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetToolTipTitle(string text)
    {
        toolTipTitle.text = text;
    }

    public void SetTooltipText(string text)
    {
        toolTipDescription.text = text;
    }
}
