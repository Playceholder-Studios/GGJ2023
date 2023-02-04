using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Dictionary<int, Ingredient> Ingredients;
    public Dictionary<int, Recipe> Recipes;
    public Dictionary<int, Level> Levels;

    public int currentLevelId;

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
        loadData();


       currentLevelId = 0;
    }   

    public void PlayNextLevel()
    {
        currentLevelId += 1;
        if (currentLevelId <= Levels.Count)
        {
            LevelManager.Instance.StartLevel(Levels[currentLevelId]);
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        // TODO
    }

    private void loadData()
    {
        // Load ingredients
        Ingredients = Util.ImportJson<Ingredient>("Ingredients")
            .ToDictionary(
                i => i.id,
                i => i);
        // Load recipes
        Recipes = Util.ImportJson<Recipe>("Recipes")
           .ToDictionary(
                r => r.id,
                r => r);
        // Load levels
        Levels = Util.ImportJson<Level>("Levels")
           .ToDictionary(
                l => l.id,
                l => l);
    }
}
