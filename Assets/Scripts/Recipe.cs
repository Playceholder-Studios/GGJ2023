using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    List<Ingredient> ingredients;
    string ingredientHash;

    void Start()
    {
        ingredients.Sort();
    }
}