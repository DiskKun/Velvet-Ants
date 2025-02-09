using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI characterName;

    public TextAsset dialogueTSV;
    public GameObject optionBox;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
    public GameObject option4;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;
    public TextMeshProUGUI option4Text;

    Button option1Button;
    Button option2Button;
    Button option3Button;
    Button option4Button;



    // Dialogue Format:
    // Col 0: Scene number
    // Col 1: Character number
    // Col 2: Dialogue ID
    // Col 3: Dialogue type
    // Col 4: Character
    // Col 5: Content
    // Col 6: Go to

    string dialogueString;
    public string[] TSVLines;
    public List<string[]> dialogueTable;

    int currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        option1Button = option1.GetComponent<Button>();
        option2Button = option2.GetComponent<Button>();
        option3Button = option3.GetComponent<Button>();
        option4Button = option4.GetComponent<Button>();



        dialogueString = dialogueTSV.text;
        TSVLines = dialogueString.Split("\n");

        dialogueTable = new List<string[]>(TSVLines.Length);
        for (int i = 0; i < TSVLines.Length; i++)
        {
            dialogueTable.Add(TSVLines[i].Split('\t'));
        }
        dialogueBox.SetActive(false);
        optionBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDialogue != 0)
        {
            PrintDialogue(currentDialogue);
        } else
        {
            optionBox.SetActive(false);
            dialogueBox.SetActive(false);
        }
    }

    public void CommenceDialogue(int index)
    {
        //dialogueBox.SetActive(true);
        currentDialogue = index;
    }

    public void ContinueDialogue(int gotoRow)
    {
        //Debug.Log(GetDialogueRow(currentDialogue)[gotoRow]);
        if (GetDialogueRow(currentDialogue)[gotoRow] == "0")
        {
            currentDialogue = 0;
        } else
        {
            int newID;
            int.TryParse(GetDialogueRow(currentDialogue)[gotoRow], out newID);
            //Debug.Log(newID);
            currentDialogue = newID;
        }
    }

    public void PrintDialogue(int dialogueID)
    {
        string[] currDialogueRow = GetDialogueRow(currentDialogue);

        if (currDialogueRow[3] == "option")
        {
            optionBox.SetActive(true);
            dialogueBox.SetActive(false);
            string[] options = currDialogueRow[5].Split("*");
            option1Text.text = "";
            option2Text.text = "";
            option3Text.text = "";
            option4Text.text = "";
            option1Button.onClick.RemoveAllListeners();
            option2Button.onClick.RemoveAllListeners();
            option3Button.onClick.RemoveAllListeners();
            option4Button.onClick.RemoveAllListeners();

            if (options.Length == 1)
            {
                option1Text.text = options[0];
                option1Button.onClick.AddListener(delegate { ContinueDialogue(6); });
            }
            else if (options.Length == 2)
            {
                option1Text.text = options[0];
                option2Text.text = options[1];
                option1Button.onClick.AddListener(delegate { ContinueDialogue(6); });
                option2Button.onClick.AddListener(delegate { ContinueDialogue(7); });
            } else if (options.Length == 3)
            {
                option1Text.text = options[0];
                option2Text.text = options[1];
                option3Text.text = options[2];
                option1Button.onClick.AddListener(delegate { ContinueDialogue(6); });
                option2Button.onClick.AddListener(delegate { ContinueDialogue(7); });
                option3Button.onClick.AddListener(delegate { ContinueDialogue(8); });
            } else if (options.Length == 4)
            {
                option1Text.text = options[0];
                option2Text.text = options[1];
                option3Text.text = options[2];
                option4Text.text = options[3];
                option1Button.onClick.AddListener(delegate { ContinueDialogue(6); });
                option2Button.onClick.AddListener(delegate { ContinueDialogue(7); });
                option3Button.onClick.AddListener(delegate { ContinueDialogue(8); });
                option4Button.onClick.AddListener(delegate { ContinueDialogue(9); });

            }


        }
        else if (currDialogueRow[3] == "text")
        {
            optionBox.SetActive(false);
            dialogueBox.SetActive(true);
            dialogueTextBox.text = currDialogueRow[5];
            characterName.text = currDialogueRow[4];
        }
        
      
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
