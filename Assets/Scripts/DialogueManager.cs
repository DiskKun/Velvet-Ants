using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI characterName;

    public TextAsset dialogueTSV;
    // Dialogue Format:
    // Col 0: Scene number
    // Col 1: Character number
    // Col 2: Dialogue ID
    // Col 3: Dialogue type
    // Col 4: Character
    // Col 5: Content
    // Col 6: Go to

    string dialogueString;
    string[] TSVLines;
    List<string[]> dialogueTable;

    int currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogueString = dialogueTSV.text;
        TSVLines = dialogueString.Split("\n");

        dialogueTable = new List<string[]>(TSVLines.Length);
        for (int i = 0; i < TSVLines.Length; i++)
        {
            dialogueTable.Add(TSVLines[i].Split('\t'));
        }
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            PrintDialogue(currentDialogue);
        }
    }

    public void CommenceDialogue(int index)
    {
        dialogueBox.SetActive(true);
        currentDialogue = index;
    }

    public void ContinueDialogue()
    {
        if (GetDialogueRow(currentDialogue)[6] == "0")
        {
            dialogueBox.SetActive(false);
        } else
        {
            int newID;
            int.TryParse(GetDialogueRow(currentDialogue)[6], out newID);
            Debug.Log(newID);
            currentDialogue = newID;
        }
    }

    public void PrintDialogue(int dialogueID)
    {
        string[] currDialogueRow = GetDialogueRow(currentDialogue);
        
        dialogueTextBox.text = currDialogueRow[5];
        characterName.text = currDialogueRow[4];
      
    }

    string[] GetDialogueRow(int dialogueID)
    {
        foreach (string[] s in dialogueTable)
        {
            if (s[2] == dialogueID.ToString())
            {
                return(s);
            }
        }
        return null;
    }
}
