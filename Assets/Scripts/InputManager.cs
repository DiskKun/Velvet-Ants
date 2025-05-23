using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject dialogue;
    //public GameObject dialoguePrompt;
    public GameObject clues;
    public GameObject evidenceButton;

    public string currentGameMode;

    void Start()
    {
        ChangeGameMode("move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameMode(string mode)
    {
        // "move" - regular player movement
        // "dialogue" - dialogue and option box
        // "clues" - clue manager
        if (mode == "move")
        {
            currentGameMode = mode;
            StartCoroutine(PlayerMovement.AllowMovement(true));
            dialogue.SetActive(false);
            if (!clues.activeInHierarchy)
            {
                evidenceButton.SetActive(true);
                clues.SetActive(false);
            }
        }
        else if (mode == "dialogue")
        {
            currentGameMode = mode;
            StartCoroutine(PlayerMovement.AllowMovement(false));
            dialogue.SetActive(true);
            //clues.SetActive(false);
            evidenceButton.SetActive(false);
        }
        else if (mode == "clues")
        {
            currentGameMode = mode;
            StartCoroutine(PlayerMovement.AllowMovement(false));
            playerMovement.StopMoving();
            dialogue.SetActive(false);
            clues.SetActive(true);
            evidenceButton.SetActive(false);

        }
    }
}
