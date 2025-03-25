using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public GameObject cluePanel;
    public GameObject clues;
    public GameObject alert;

    Image[] clueTransforms;

    List<GameObject> clueGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        clueTransforms = clues.GetComponentsInChildren<Image>(true);
        Debug.Log(clueTransforms[1].ToString());

        foreach (Image t in clueTransforms)
        {
            clueGameObjects.Add(t.gameObject);
        }


        // TEMPORARY
        ClueActive("Clue 1", true);
    }

    public void ClueActive (string name, bool active)
    {
        foreach (GameObject g in clueGameObjects)
        {
            if (g.name == name)
            {
                g.SetActive(active);
                if (active)
                {
                    alert.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
