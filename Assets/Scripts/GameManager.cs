using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject startGameScreen;
    public GameObject endGameScreen;
    public TextMeshProUGUI tipDisplayText;
    public TextMeshProUGUI endGameTotalTipDisplayText;
    private int totalTip;

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
       totalTip = 0;
       endGameScreen.SetActive(false);
       //startGameScreen.SetActive(true);
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

    public void AddTip(int amount)
    {
        totalTip += amount;
        tipDisplayText.text = totalTip.ToString();
    }

    public void EndGame()
    {
        endGameTotalTipDisplayText.text = "$" + totalTip.ToString();
        endGameScreen.SetActive(true);
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

    public void StartGame()
    {
        startGameScreen.SetActive(false);
    }

    public Ingredient GetIngredient(int id) {
        return Ingredients[id];
    }
}
