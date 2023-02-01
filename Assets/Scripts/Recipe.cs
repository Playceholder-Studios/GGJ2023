using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe
{
    public int id;
    public string recipeName;
    public string description;
    public List<int> ingredients;
    public bool iced;

    private bool unlocked = false;
    private string hash = "";

    public bool TryRecipe(List<int> _ingredients, bool _iced) {
        bool isMatch = matchesRecipe(_ingredients, _iced);
        if (!unlocked && isMatch) {
            unlockRecipe();
        }
        return isMatch;
    }

    private bool matchesRecipe(List<int> _ingredients, bool _iced)
    {
        if (hash == "")
        {
            ingredients.Sort();
            hash = String.Join("-", ingredients);

            if (iced) {
                hash += "-iced";
            }
        }

        _ingredients.Sort();
        string _ingredientHash = String.Join("-", _ingredients);

        if (_iced) {
            _ingredientHash += "-iced";
        }

        return _ingredientHash == hash;
    }

    private void unlockRecipe()
    {
        unlocked = true;
    }
}