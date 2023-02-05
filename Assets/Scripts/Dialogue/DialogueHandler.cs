using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum DialogType
{
    Customer,
    Player
}

public class DialogueHandler : MonoBehaviour
{
    public GameObject playerDialogueBox;
    public GameObject customerDialogueBox;
    public Transform dialogContentTransform;
    public ScrollRect scrollRect;
    private bool _addType = true;
    private bool _hasAddedDialog = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_addType)
            {
                AddDialog(Time.time.ToString(), DialogType.Customer);
            } else {
                AddDialog(Time.time.ToString(), DialogType.Player);
            }
            _addType = !_addType;
        }

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

        _hasAddedDialog = true;
    }
}
