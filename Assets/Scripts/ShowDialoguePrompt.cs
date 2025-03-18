using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialoguePrompt : MonoBehaviour
{
    //variables for dialogue prompt
    public GameObject dialoguePrompt;
    Button dialoguePromptButton;

    public InputManager inputManager;

    void Start()
    {
        dialoguePromptButton = dialoguePrompt.GetComponent<Button>();
        dialoguePrompt.SetActive(false); //don't show dialogue prompt
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialoguePrompt.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dialoguePrompt != null)
        {
            dialoguePrompt.SetActive(false);

        }

    }
}
