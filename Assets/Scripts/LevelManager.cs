using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<int> currentIngredients;
    public List<GameObject> ingredientSlots;
    public bool iceAdded;
    // Used to advance to the next customer so it doesn't skip the 
    // success/failure messages
    public bool readyToServeCustomer = false;

    public int currentDrink;

    public Level currentLevel;
    public DialogueHandler dialogueHandler;
    public CustomerHandler customerHandler;

    public GameObject activeIce;
    public GameObject emptyCup;
    public GameObject successDrink;
    public GameObject failureDrink;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueHandler.isWaitingForPlayerResponse)
            {
                dialogueHandler.NextDialogue();
            }

            if (readyToServeCustomer)
            {
                GameManager.Instance.PlayNextLevel();
            }
        }
    }

    public void StartLevel(Level level)
    {
        Debug.Log("Starting level " + level.id);
        ClearIngredients();
        currentLevel = level;
        readyToServeCustomer = false;
        dialogueHandler.ClearDialogue();
        customerHandler.LoadCustomer(currentLevel.characterID);
        dialogueHandler.StartDialogue(currentLevel.ordering);
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
            dialogueHandler.StartDialogue(currentLevel.success);
            GameManager.Instance.AddTip(currentLevel.tipSuccess);
            AudioManager.Instance.PlayEffect("SUCCESS");
        }
        else 
        {
            Debug.Log("You suck");
            dialogueHandler.StartDialogue(currentLevel.failure);
            GameManager.Instance.AddTip(currentLevel.tipFailure);
            AudioManager.Instance.PlayEffect("FAILURE");
        }

        readyToServeCustomer = true;
        dialogueHandler.SetInfoDialogueActive(true);
    }

    public void ToggleIce()
    {
        Debug.Log("Toggling ice to " + !iceAdded);
        if (!iceAdded) {
            AudioManager.Instance.PlayEffect("ICE");
        }
        iceAdded = !iceAdded;
        activeIce.SetActive(iceAdded);
    }

    public void ClearIngredients()
    {
        currentIngredients = new List<int>();

        foreach (GameObject slot in ingredientSlots)
        {
            SetSlot(slot, -1);
        }
        iceAdded = false;
        activeIce.SetActive(false);
        currentDrink = -1;

        emptyCup.SetActive(true);
        successDrink.SetActive(false);
        failureDrink.SetActive(false);
    }

    public bool AddIngredient(int id)
    {
        if (currentIngredients.Count < 3 && !currentIngredients.Contains(id))
        {
            Debug.Log("Added ingredient " + id);
            SetSlot(ingredientSlots[currentIngredients.Count], id);
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
        emptyCup.SetActive(false);
        AudioManager.Instance.PlayEffect("MIX");
        foreach (Recipe r in GameManager.Instance.Recipes.Values) {
            bool isMatch = r.TryRecipe(currentIngredients, iceAdded);
            Debug.Log("Trying Recipe " + r.recipeName + ": " + isMatch);
            if (isMatch)
            {
                successDrink.SetActive(true);
                currentDrink = r.id;
                Debug.Log("Recipe found! " + r.id);
                return;
            }
        }

        failureDrink.SetActive(true);
        currentDrink = 0;
        Debug.Log("Not a recipe!");
        return;
    }

    private void SetSlot(GameObject ingSlot, int ingredientId) {
        foreach(Transform child in ingSlot.transform)
        {
            if (child.gameObject.name == ingredientId.ToString())
            {
                child.gameObject.SetActive(true);
            }
            else 
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
