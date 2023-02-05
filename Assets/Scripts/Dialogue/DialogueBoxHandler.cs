using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxHandler : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
