using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Don't kill me for the naming @terry wen 
// my head is fully empty
public class ButtonHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent PointerEnter;
    public UnityEvent PointerExit;

    private void Awake()
    {
        PointerEnter ??= new UnityEvent();
        PointerExit ??= new UnityEvent();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke();
    }
}
