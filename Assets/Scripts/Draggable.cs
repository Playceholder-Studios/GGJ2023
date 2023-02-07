using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// Game Object can only be dropped on Droppable areas.
    /// </summary>
    public bool isDroppableOnly = true;
    public UnityEvent PointerDown;
    /// <summary>
    /// Unity Event invoked when a drag event starts. The original game object
    /// is passed through the callback as parameter.
    /// </summary>
    public UnityEvent DragBegin;
    /// <summary>
    /// Unity Event invoked when a drag event ends. The original game object
    /// is passed through the callback as parameter.
    /// </summary>
    public UnityEvent DragEnd;
    public UnityEvent PointerUp;
    private SpriteRenderer spriteRenderer;
    private GameObject _draggedIcon;
    private Color _defaultColor;

    public int ingredientId;
    public Ingredient ingredient;

    private void Start()
    {
        DragBegin ??= new UnityEvent();
        DragEnd ??= new UnityEvent();
        PointerDown ??= new UnityEvent();
        PointerUp ??= new UnityEvent();
        spriteRenderer = GetSpriteRenderer();
        _defaultColor = spriteRenderer.color;

        ingredient = GameManager.Instance.GetIngredient(ingredientId);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartDragEffect();
        _draggedIcon = new GameObject("Dragged Icon");
        SpriteRenderer addedSR = _draggedIcon.AddComponent<SpriteRenderer>();
        addedSR.sprite = spriteRenderer.sprite;
        addedSR.sortingOrder = 1;
        _draggedIcon.transform.position = transform.position;
        _draggedIcon.transform.localScale = transform.localScale;

        DndHandler.Instance.previouslyDraggedObject = gameObject;

        DragBegin?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDragPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        spriteRenderer.color = _defaultColor;
        if (isDroppableOnly)
        {
            GameObject droppedOn = eventData.pointerCurrentRaycast.gameObject;
            // if (droppedOn == null || droppedOn.GetComponent<DropTarget>() == null)
            // {
            Destroy(_draggedIcon);
            // }
        }

        DragEnd?.Invoke();
    }

    private void SetDragPosition(PointerEventData eventData)
    {
        if (_draggedIcon != null)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            _draggedIcon.transform.position = new Vector3(worldPoint.x, worldPoint.y, _draggedIcon.transform.position.z);
        }
    }

    private void StartDragEffect()
    {
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    /// <summary>
    /// Get the first Sprite Renderer in the game object or its direct children.
    /// </summary>
    private SpriteRenderer GetSpriteRenderer()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            foreach (Transform t in transform)
            {
                sr = t.GetComponent<SpriteRenderer>();
                if (sr != null) break;
            }
        }

        return sr;
    }
}
