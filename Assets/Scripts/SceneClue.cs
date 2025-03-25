using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneClue : MonoBehaviour
{
    public Button pickupButton;
    TextMeshProUGUI buttonText;
    public ClueManager cm;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = pickupButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickupButton.gameObject.SetActive(true);
        pickupButton.onClick.RemoveAllListeners();
        pickupButton.onClick.AddListener(delegate { cm.ClueActive(gameObject.name, true); });
        pickupButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        buttonText.text = "Pick up " + gameObject.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupButton.gameObject.SetActive(false);
    }
}
