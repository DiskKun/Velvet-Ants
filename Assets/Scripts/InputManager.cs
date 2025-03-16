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
    //public CameraZoom cameraZoomScript;
    // Start is called before the first frame update
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
            playerMovement.AllowMovement(true);
            dialogue.SetActive(false);
            clues.SetActive(false);
            evidenceButton.SetActive(true);

        } else if (mode == "dialogue")
        {
            playerMovement.AllowMovement(false);
            dialogue.SetActive(true);
            clues.SetActive(false);
            evidenceButton.SetActive(false);
        }
        else if (mode == "clues")
        {
            playerMovement.AllowMovement(false);
            dialogue.SetActive(false);
            clues.SetActive(true);
            evidenceButton.SetActive(false);

        }
    }
}
