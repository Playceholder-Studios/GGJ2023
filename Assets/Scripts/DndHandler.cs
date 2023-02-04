using UnityEngine;

/// <summary>
/// Drag and drop handler
/// </summary>
public class DndHandler : MonoBehaviour
{
    public static DndHandler Instance { get; private set; }
    public GameObject previouslyDraggedObject;

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
}
