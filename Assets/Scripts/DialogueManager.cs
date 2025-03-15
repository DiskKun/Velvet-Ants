using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    public InputManager inputManager;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI characterName;

    public TextAsset dialogueTSV;

    public GameObject optionBox;
    List<GameObject> optionBoxes = new List<GameObject>();
    public List<Button> optionButtons = new List<Button>();
    List<TextMeshProUGUI> optionTexts = new List<TextMeshProUGUI>();

    // Dialogue Format:
    // Col 0: Scene number
    // Col 1: Character number
    // Col 2: Dialogue ID
    // Col 3: Dialogue type
    // Col 4: Character
    // Col 5: Content
    // Col 6-9: Go to

    string dialogueString;
    public string[] TSVLines;
    public List<string[]> dialogueTable;

    int currentDialogue;

    public CameraZoom cameraZoomScript;

    [System.Serializable]
    public struct DialogueData
    {
        public int dialogueID;
        public string characterName;
    }

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in optionBox.transform.GetChild(0))
        {
            optionBoxes.Add(child.gameObject);
        }
        foreach (GameObject oBox in optionBoxes)
        {
            optionButtons.Add(oBox.GetComponent<Button>());
            optionTexts.Add(oBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }


        

        dialogueString = dialogueTSV.text;
        TSVLines = dialogueString.Split("\n");

        dialogueTable = new List<string[]>(TSVLines.Length);
        for (int i = 0; i < TSVLines.Length; i++)
        {
            dialogueTable.Add(TSVLines[i].Split('\t'));
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CommenceDialogue(int index)
    {
        //dialogueBox.SetActive(true);
        currentDialogue = index;
        PrintDialogue(currentDialogue);
        
        cameraZoomScript.ZoomIn();


        var data = new DialogueData()
        {
            dialogueID = index,
            characterName = GetDialogueRow(currentDialogue)[4]
        };
        TelemetryLogger.Log(this, "Dialogue Commenced", data);

    }

    public void ContinueDialogue(int gotoRow)
    {

        //if (currentDialogue == 0) { return; }


        Debug.Log(gotoRow);
        int newID;
        int.TryParse(GetDialogueRow(currentDialogue)[gotoRow], out newID);
        currentDialogue = newID;
        PrintDialogue(currentDialogue);
        
    }

    public void PrintDialogue(int dialogueID)
    {

        if (currentDialogue == 0) { inputManager.ChangeGameMode("move"); cameraZoomScript.ZoomOut(); return; }



        string[] currDialogueRow = GetDialogueRow(currentDialogue);

        

        if (currDialogueRow[3] == "option")
        {
            optionBox.SetActive(true);
            dialogueBox.SetActive(false);
            string[] options = currDialogueRow[5].Split("*");
            foreach (TextMeshProUGUI t in optionTexts)
            {
                t.text = "";
            }
            foreach (Button b in optionButtons)
            {
                b.onClick.RemoveAllListeners();
            }

            optionTexts[0].text = options[0];
            optionButtons[0].onClick.AddListener(delegate { ContinueDialogue(6); });
            if (options.Length == 1) { return; }
            optionTexts[1].text = options[1];
            optionButtons[1].onClick.AddListener(delegate { ContinueDialogue(7); });
            if (options.Length == 2) { return; }
            optionTexts[2].text = options[2];
            optionButtons[2].onClick.AddListener(delegate { ContinueDialogue(8); });
            if (options.Length == 3) { return; }
            optionTexts[3].text = options[3];
            optionButtons[3].onClick.AddListener(delegate { ContinueDialogue(9); });

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
