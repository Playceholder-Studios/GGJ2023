using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<int> currentIngredients;
    public bool iceAdded;

    public int currentDrink;

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

    private void Start()
    {
        GameManager.Instance.PlayNextLevel();
    }

    public void StartLevel(Level level)
    {
        Debug.Log("Starting level " + level.id);
        ClearIngredients();
        currentLevel = level;

        PlayDialogue(currentLevel.ordering);
    }

    public void Submit() 
    {
        if (currentDrink < 1)
        {
            Debug.Log("No drink to submit!");
            return;
        }
        if (currentDrink == currentLevel.correctDrinkId)
        {
            Debug.Log("We did it!");
            PlayDialogue(currentLevel.success);
        }
        else 
        {
            Debug.Log("You suck");
            PlayDialogue(currentLevel.failure);
        }
        GameManager.Instance.PlayNextLevel();
    }

    public void ToggleIce()
    {
        Debug.Log("Toggling ice to " + !iceAdded);
        iceAdded = !iceAdded;
    }

    public void ClearIngredients()
    {
        currentIngredients = new List<int>();
        iceAdded = false;
        currentDrink = -1;
    }

    public bool AddIngredient(int id)
    {
        if (currentIngredients.Count < 3 && !currentIngredients.Contains(id))
        {
            Debug.Log("Added ingredient " + id);
            currentIngredients.Add(id);
            return true;
        }

        Debug.Log("ingredients full " + id);
        return false;
    }

    public void MixDrink()
    {
        if (currentDrink != -1)
        {
            Debug.Log("Drink already mixed");
            return;
        }
        foreach (Recipe r in GameManager.Instance.Recipes.Values) {
            bool isMatch = r.TryRecipe(currentIngredients, iceAdded);
            Debug.Log("Trying Recipe " + r.recipeName + ": " + isMatch);
            if (isMatch)
            {
                currentDrink = r.id;
                Debug.Log("Recipe found! " + r.id);
                return;
            }
        }

        currentDrink = 0;
        Debug.Log("Not a recipe!");
        return;
    }

    public void PlayDialogue(List<Dialogue> dialogue)
    {
        // TODO
    }
}
