using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltippable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public float testLength;
    public float toolTipTimeShow = 0.5f;
    private float _hoverTimer;
    private bool _isHovering;

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DndHandler.Instance.toolTipObject.SetActive(false);
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
}
