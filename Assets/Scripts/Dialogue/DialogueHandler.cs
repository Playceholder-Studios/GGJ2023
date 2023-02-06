using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogType
{
    Customer,
    Player
}

public class DialogueHandler : MonoBehaviour
{
    public bool isWaitingForPlayerResponse = false;
    public GameObject playerDialogueBox;
    public GameObject customerDialogueBox;
    public GameObject dialogueInfoText;
    public Transform dialogContentTransform;
    public ScrollRect scrollRect;
    private List<Dialogue> currentDialogueList;
    private int currentDialogueIndex;
    //For Debugging Purpose
    //private bool _addType = true;
    private bool _hasAddedDialog = false;

    private void Update()
    {
        //For Debugging Purpose
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (_addType)
        //    {
        //        AddDialog(Time.time.ToString(), DialogType.Customer);
        //    } else {
        //        AddDialog(Time.time.ToString(), DialogType.Player);
        //    }
        //    _addType = !_addType;
        //}

        if (_hasAddedDialog )
        {
            StartCoroutine (ScrollToBottom());
        }
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.transform);
        _hasAddedDialog = false;
    }

    public void AddDialog(string dialogText, DialogType type)
    {
        if (type == DialogType.Customer)
        {
            GameObject c = Instantiate(customerDialogueBox, dialogContentTransform);
            c.GetComponent<DialogueBoxHandler>().SetDialogueText(dialogText);
        }
        else if (type == DialogType.Player)
        {
            GameObject p = Instantiate(playerDialogueBox, dialogContentTransform);
            p.GetComponent<DialogueBoxHandler>().SetDialogueText(dialogText);
        }

        AudioManager.Instance.PlayEffect("POP");
        _hasAddedDialog = true;
    }

    public void StartDialogue(List<Dialogue> dialogueList)
    {
        currentDialogueList = dialogueList;
        currentDialogueIndex = (int)DialogueLocation.Start;
        NextDialogue();
    }

    public void NextDialogue()
    {
        int remainingDialogueCount = currentDialogueList.Count - currentDialogueIndex;
        int startingDialogueIndex = currentDialogueIndex;
        foreach (Dialogue d in currentDialogueList.GetRange(startingDialogueIndex, remainingDialogueCount))
        {
            if (d.isPlayer && !isWaitingForPlayerResponse)
            {
                isWaitingForPlayerResponse = true;
                break;
            } else
            {
                isWaitingForPlayerResponse = false;
            }

            AddDialog(d.content, d.isPlayer ? DialogType.Player : DialogType.Customer);
            currentDialogueIndex++;

            if (currentDialogueIndex >= currentDialogueList.Count - 1)
            {
                SetInfoDialogueActive(false);
            }
            else
            {
                SetInfoDialogueActive(true);
            }
        }
    }

    public void ClearDialogue()
    {
        foreach (Transform t in dialogContentTransform)
        {
            Destroy(t.gameObject);
        }
    }

    public void SetInfoDialogueActive(bool isActive)
    {
        dialogueInfoText.SetActive(isActive);
    }
}
