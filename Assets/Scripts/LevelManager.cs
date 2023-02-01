using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<int> currentIngredients;
    public bool iceAdded;

    void Start()
    {
        ClearIngredients();

        // Testing
        MixDrink();
        AddIngredient(1);
        MixDrink();
        AddIngredient(2);
        MixDrink(); // Success!
        ToggleIce();
        MixDrink();
        ClearIngredients();
        MixDrink();
    }

    public void ToggleIce()
    {
        iceAdded = !iceAdded;
    }

    public void ClearIngredients()
    {
        currentIngredients = new List<int>();
        iceAdded = false;
    }

    public void AddIngredient(int id)
    {
        currentIngredients.Add(id);
    }

    public int MixDrink()
    {
        foreach (Recipe r in GameManager.Instance.Recipes.Values) {
            bool isMatch = r.TryRecipe(currentIngredients, iceAdded);
            Debug.Log("Trying Recipe " + r.recipeName + ": " + isMatch);
            if (isMatch)
            {
                return r.id;
            }
        }

        return -1;
    }
}
