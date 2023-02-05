using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltippable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public float testLength;
    public float toolTipTimeShow = 0.5f;
    public bool isCustomTooltip = false;
    public string customTooltipTitle;
    public string customTooltipDescription;
    public Draggable draggableIngredient;
    private float _hoverTimer;
    private bool _isHovering;
    private Ingredient _draggableIngredient;

    private void Start()
    {
        if (draggableIngredient != null)
        {
            _draggableIngredient = draggableIngredient.ingredient;
        }
    }

    private void Update()
    {
        if (_isHovering)
        {
            _hoverTimer += Time.deltaTime;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering over " + eventData.hovered);
        _isHovering = true;
        SetIngredientToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DndHandler.Instance.toolTipObject.SetActive(false);
        ClearIngredientToolTip();
        _hoverTimer = 0;
        _isHovering = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_hoverTimer >= toolTipTimeShow)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            DndHandler.Instance.toolTipObject.transform.position = new Vector3(worldPoint.x + testLength, worldPoint.y, DndHandler.Instance.toolTipObject.transform.position.z);
            DndHandler.Instance.toolTipObject.SetActive(true);
        }
    }

    private void SetIngredientToolTip()
    {
        if (isCustomTooltip)
        {
            DndHandler.Instance.SetToolTipTitle(customTooltipTitle);
            DndHandler.Instance.SetTooltipText(customTooltipDescription);
        } else if (draggableIngredient != null)
        {
            DndHandler.Instance.SetToolTipTitle(_draggableIngredient.ingredientName);
            DndHandler.Instance.SetTooltipText(_draggableIngredient.description);
        }
    }

    private void ClearIngredientToolTip()
    {
        if (draggableIngredient != null)
        {
            DndHandler.Instance.SetToolTipTitle("Title");
            DndHandler.Instance.SetTooltipText("");
        }
    }
}
