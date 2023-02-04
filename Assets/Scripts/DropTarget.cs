using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log("Dropping " + DndHandler.Instance.previouslyDraggedObject);

        Draggable ingredient = DndHandler.Instance.previouslyDraggedObject.GetComponent<Draggable>();

        LevelManager.Instance.AddIngredient(ingredient.ingredient.id);
    }
}
