using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextBox;

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
    public List<string[]> dialogueTable;

    public int currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogueString = dialogueTSV.text;
        TSVLines = dialogueString.Split("\n");

        dialogueTable = new List<string[]>(TSVLines.Length);
        for (int i = 0; i < TSVLines.Length; i++)
        {
            dialogueTable.Add(TSVLines[i].Split('\t'));
            //Debug.Log(dialogueTable[i][0]);

        }


        // TESTING

       

    }

    // Update is called once per frame
    void Update()
    {
        PrintDialogue(currentDialogue);
    }

    public void CommenceDialogue()
    {

    }

    public void PrintDialogue(int dialogueID)
    {
        foreach (string[] s in dialogueTable)
        {
//            Debug.Log(s);
            if (s[2] == dialogueID.ToString())
            {
                //Debug.Log(s[5].ToString());
                dialogueTextBox.text = s[5];
            }
        }
    }
}
