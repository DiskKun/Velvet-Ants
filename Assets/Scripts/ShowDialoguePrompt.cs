using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialoguePrompt : MonoBehaviour
{
    //variables for dialogue prompt
    public GameObject dialoguePrompt;

    public InputManager inputManager;

    void Start()
    {
        dialoguePrompt.SetActive(false); //don't show dialogue prompt
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialoguePrompt.SetActive(true);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inputManager.currentGameMode == "dialogue")
        {
            dialoguePrompt.SetActive(false);
        }
        else
        {
            dialoguePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dialoguePrompt != null)
        {
            dialoguePrompt.SetActive(false);

        }

    }
}
