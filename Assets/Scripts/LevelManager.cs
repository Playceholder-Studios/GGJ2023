using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<int> currentIngredients;
    public bool iceAdded;

    public Level currentLevel;
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

    public void StartLevel(Level level)
    {
        ClearIngredients();
        currentLevel = level;

        PlayDialogue(currentLevel.ordering);
    }

    public void Submit(int drinkId) 
    {
        if (drinkId == currentLevel.correctDrinkId)
        {
            PlayDialogue(currentLevel.success);
        }
        else 
        {
            PlayDialogue(currentLevel.failure);
        }
        GameManager.Instance.PlayNextLevel();
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

    public bool AddIngredient(int id)
    {
        if (currentIngredients.Count < 3)
        {
            currentIngredients.Add(id);
            return true;
        }

        return false;
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

    public void PlayDialogue(List<Dialogue> dialogue)
    {
        // TODO
    }
}
