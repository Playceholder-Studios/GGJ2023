using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropping " + DndHandler.Instance.previouslyDraggedObject);

        Ingredient ingredient = DndHandler.Instance.previouslyDraggedObject.GetComponent<Ingredient>();

        LevelManager.Instance.AddIngredient(ingredient.id);
    }
}
