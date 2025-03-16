using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public GameObject cluePanel;
    public GameObject clues;

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

        ClueActive("Clue 1", true);
    }

    public void ClueActive (string name, bool active)
    {
        foreach (GameObject g in clueGameObjects)
        {
            if (g.name == name)
            {
                g.SetActive(active);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
